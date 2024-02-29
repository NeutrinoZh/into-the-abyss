using UnityEngine;
using UnityEngine.Advertisements;

namespace IntoTheAbyss.Ads {
    public class BannerAd : MonoBehaviour {
        [SerializeField] private BannerPosition m_bannerPosition = BannerPosition.BOTTOM_CENTER;

        [SerializeField] private string m_androidAdUnitId = "Banner_Android";
        [SerializeField] private string m_iosAdUnitId = "Banner_iOS";
        private string m_adUnitId = null;

        private void Start() {
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
            m_adUnitId = m_androidAdUnitId;
#endif
            Advertisement.Banner.SetPosition(m_bannerPosition);
        }

        public void Load() {
            BannerLoadOptions options = new BannerLoadOptions {
                loadCallback = OnBannerLoaded,
                errorCallback = OnBannerError
            };

            Advertisement.Banner.Load(m_adUnitId, options);
        }

        private void OnBannerLoaded() {
            Debug.Log("BannerAd loaded");
            ShowBannerAd();
        }

        private void OnBannerError(string message) {
            Debug.Log($"BannerAd Error: {message}");
        }

        private void ShowBannerAd() {
            BannerOptions options = new BannerOptions {
                clickCallback = OnBannerClicked,
                hideCallback = OnBannerHidden,
                showCallback = OnBannerShown
            };

            Advertisement.Banner.Show(m_adUnitId, options);
        }

        private void OnBannerClicked() { }
        private void OnBannerShown() { }
        private void OnBannerHidden() { }
    }
}