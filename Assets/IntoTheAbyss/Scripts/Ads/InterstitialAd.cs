using UnityEngine;
using UnityEngine.Advertisements;

namespace IntoTheAbyss.Ads {
    public class InterstitialAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener {
        [SerializeField] private string m_androidAdUnitId = "Interstitial_Android";
        [SerializeField] private string m_iosAdUnitId = "Interstitial_iOS";

        private string m_adUnitId = "";
        private bool m_loaded = false;

        private void Awake() {
            m_adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
                ? m_iosAdUnitId
                : m_androidAdUnitId;
        }

        public void LoadAd() {
            Debug.Log("Loading Ad: " + m_adUnitId);
            Advertisement.Load(m_adUnitId, this);
        }

        public void ShowAd() {
            if (!m_loaded)
                return;

            Debug.Log("Showing Ad: " + m_adUnitId);
            Advertisement.Show(m_adUnitId, this);
        }

        public void OnUnityAdsAdLoaded(string adUnitId) {
            m_loaded = true;
        }

        public void OnUnityAdsFailedToLoad(string _adUnitId, UnityAdsLoadError error, string message) {
            Debug.Log($"Error loading Ad Unit: {_adUnitId} - {error} - {message}");
        }

        public void OnUnityAdsShowFailure(string _adUnitId, UnityAdsShowError error, string message) {
            Debug.Log($"Error showing Ad Unit {_adUnitId}: {error} - {message}");
        }

        public void OnUnityAdsShowStart(string _adUnitId) { }
        public void OnUnityAdsShowClick(string _adUnitId) { }
        public void OnUnityAdsShowComplete(string _adUnitId, UnityAdsShowCompletionState showCompletionState) { }
    }
}