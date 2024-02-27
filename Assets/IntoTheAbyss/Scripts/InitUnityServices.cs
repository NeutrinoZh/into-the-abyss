using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System;

namespace IntoTheAbyss {
    public class InitUnityServices : MonoBehaviour {
        private async void Awake() {
            try {
                await UnityServices.InitializeAsync();
            } catch (Exception e) {
                Debug.LogException(e);
            }

            SetupEvents();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        void SetupEvents() {
            AuthenticationService.Instance.SignedIn += () => {
                Debug.Log($"{nameof(InitUnityServices)} PlayerID: {AuthenticationService.Instance.PlayerId}");
                Debug.Log($"{nameof(InitUnityServices)} Access Token: {AuthenticationService.Instance.AccessToken}");

            };
            AuthenticationService.Instance.SignInFailed += err => Debug.LogError($"{nameof(InitUnityServices)} {err}");
            AuthenticationService.Instance.SignedOut += () => Debug.Log($"{nameof(InitUnityServices)} Player signed out.");
            AuthenticationService.Instance.Expired += () => Debug.Log($"{nameof(InitUnityServices)} Player session could not be refreshed and expired.");
        }
    }
}