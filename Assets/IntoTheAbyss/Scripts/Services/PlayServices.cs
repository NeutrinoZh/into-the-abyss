using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;
using System.Threading.Tasks;

namespace IntoTheAbyss {
    public class PlayServices : MonoBehaviour {
        public string Token;

        private void Awake() {
            PlayGamesPlatform.Activate();
        }

        private async void Start() {
            try {
                await UnityServices.InitializeAsync();
#if UNITY_EDITOR
#else
                await LoginGooglePlayGames();
                await SignInPlayServices();
#endif
            } catch (Exception e) {
                Debug.LogException(e);
            }
        }

        public Task LoginGooglePlayGames() {
            var tcs = new TaskCompletionSource<object>();
            PlayGamesPlatform.Instance.Authenticate((success) => {
                if (success == SignInStatus.Success) {
                    Debug.Log($"{nameof(PlayServices)} Login with Google Play games successful.");

                    PlayGamesPlatform.Instance.RequestServerSideAccess(true, code => {
                        Debug.Log($"{nameof(PlayServices)} Authorization code: " + code);
                        Token = code;
                        tcs.SetResult(null);
                    });
                } else {
                    Debug.Log($"{nameof(PlayServices)} Login Unsuccessful: {success}");
                    tcs.SetException(new Exception("Failed"));
                }
            });
            return tcs.Task;
        }

        public async Task SignInPlayServices() {
            AuthenticationService.Instance.SignedIn += () => {
                Debug.Log($"{nameof(PlayServices)} PlayerID: {AuthenticationService.Instance.PlayerId}");
                Debug.Log($"{nameof(PlayServices)} Access Token: {AuthenticationService.Instance.AccessToken}");

            };

            AuthenticationService.Instance.SignInFailed += err => Debug.LogError($"{nameof(PlayServices)} {err}");
            AuthenticationService.Instance.SignedOut += () => Debug.Log($"{nameof(PlayServices)} Player signed out.");
            AuthenticationService.Instance.Expired += () => Debug.Log($"{nameof(PlayServices)} Player session could not be refreshed and expired.");

            try {
                await AuthenticationService.Instance.SignInWithGooglePlayGamesAsync(Token);
                Debug.Log($"{nameof(PlayServices)} SignIn is successful.");
            } catch (AuthenticationException ex) {
                Debug.LogException(ex);
            } catch (RequestFailedException ex) {
                Debug.LogException(ex);
            }
        }
    }
}