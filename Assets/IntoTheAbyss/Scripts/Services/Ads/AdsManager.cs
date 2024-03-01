using IntoTheAbyss.Game;

using UnityEngine;

namespace IntoTheAbyss.Ads {
    [RequireComponent(typeof(InterstitialAd), typeof(BannerAd), typeof(AdsInit))]
    public class AdsManager : MonoBehaviour {
        private AdsInit m_adsInit;
        private InterstitialAd m_interstitial;
        private BannerAd m_banner;

        [SerializeField] private int m_bubblesToShowAd;
        private int m_bubbles = 0;

        [SerializeField] private int m_attemptToShowAd;
        private int m_attempt = 0;

        private void Awake() {
            m_adsInit = GetComponent<AdsInit>();
            m_adsInit.OnInitComplete += Load;

            m_interstitial = GetComponent<InterstitialAd>();
            m_banner = GetComponent<BannerAd>();
        }

        private void OnDestroy() {
            m_adsInit.OnInitComplete -= Load;
            Player.OnAfterDie -= ShowInterstitial;
        }

        private void Load() {
            m_banner.Load();
            m_interstitial.Load();

            Player.OnAfterDie += ShowInterstitial;
        }

        private void ShowInterstitial() {
            m_attempt += 1;
            m_bubbles += SessionManager.Instance.Score;
            if (m_bubbles >= m_bubblesToShowAd || m_attempt >= m_attemptToShowAd) {
                m_bubbles = 0;
                m_attempt = 0;

                m_interstitial.ShowAd();
                //m_interstitial.LoadAd();
            }
        }
    }
}