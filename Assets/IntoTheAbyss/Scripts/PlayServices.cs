using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;

namespace IntoTheAbyss {
    public class PlayServices : MonoBehaviour {
        public string Token;

        private async void Awake() {
            try {
                await UnityServices.InitializeAsync();
            } catch (Exception e) {
                Debug.LogException(e);
            }

            PlayGamesPlatform.Activate();
            LoginGooglePlayGames();
        }

        public void LoginGooglePlayGames() {
            PlayGamesPlatform.Instance.Authenticate((success) => {
                if (success == SignInStatus.Success) {
                    Debug.Log($"{nameof(PlayServices)} Login with Google Play games successful.");

                    PlayGamesPlatform.Instance.RequestServerSideAccess(true, code => {
                        Debug.Log($"{nameof(PlayServices)} Authorization code: " + code);
                        Token = code;
                        SignInPlayServices();
                    });
                } else {
                    Debug.Log($"{nameof(PlayServices)} Login Unsuccessful");
                }
            });
        }

        public async void SignInPlayServices() {
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