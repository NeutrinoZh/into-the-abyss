using IntoTheAbyss.Game;

using UnityEngine;

namespace IntoTheAbyss.Ads {
    [RequireComponent(typeof(InterstitialAd), typeof(AdsInit))]
    public class AdsManager : MonoBehaviour {
        private AdsInit m_adsInit;
        private InterstitialAd m_interstitial;

        [SerializeField] private int m_bubblesToShowAd;
        private int m_bubbles = 0;

        private void Start() {
            m_adsInit = GetComponent<AdsInit>();
            m_adsInit.OnInitComplete += Load;

            m_interstitial = GetComponent<InterstitialAd>();
        }

        private void OnDestroy() {
            m_adsInit.OnInitComplete -= Load;
            Player.OnAfterDie -= ShowInterstitial;
        }

        private void Load() {
            m_interstitial.LoadAd();

            Player.OnAfterDie += ShowInterstitial;
        }

        private void ShowInterstitial() {
            m_bubbles += SessionManager.Instance.Score;
            if (m_bubbles >= m_bubblesToShowAd) {
                m_bubbles = 0;

                m_interstitial.ShowAd();
                //m_interstitial.LoadAd();
            }
        }
    }
}