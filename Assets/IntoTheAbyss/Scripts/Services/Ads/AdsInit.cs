using System;

using UnityEngine;
using UnityEngine.Advertisements;

namespace IntoTheAbyss.Ads {
    public class AdsInit : MonoBehaviour, IUnityAdsInitializationListener {
        [SerializeField] private string m_androidGameId;
        [SerializeField] private string m_iOSGameId;
        [SerializeField] private bool m_testMode = true;

        private string m_gameId;

        public event Action OnInitComplete = null;

        void Awake() {
            InitializeAds();
        }

        public void InitializeAds() {
#if UNITY_IOS
            m_gameId = m_iOSGameId;
#elif UNITY_ANDROID
            m_gameId = m_androidGameId;
#elif UNITY_EDITOR
            m_gameId = m_androidGameId;
#endif
            if (!Advertisement.isInitialized && Advertisement.isSupported) {
                Advertisement.Initialize(m_gameId, m_testMode, this);
            }
        }

        public void OnInitializationComplete() {
            Debug.Log("Unity Ads initialization complete.");
            OnInitComplete?.Invoke();
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message) {
            Debug.Log($"Unity Ads Initialization Failed: {error} - {message}");
        }
    }
}