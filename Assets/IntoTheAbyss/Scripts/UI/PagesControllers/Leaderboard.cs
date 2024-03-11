using UnityEngine;
using UnityEngine.UIElements;

namespace IntoTheAbyss.UI {
    public class Leaderboard : MonoBehaviour {
        [SerializeField] private UIPages m_uiPages;
        private UIDocument m_document;

        private Button m_homeBtn;
        private LeaderboardTable m_table;

        private const string c_homeButtonId = "home__button";
        private const string c_leadboardTabaleId = "leaderboard__table";

        private void Awake() {
            m_document = GetComponent<UIDocument>();

            m_table = m_document.rootVisualElement.Query<LeaderboardTable>(c_leadboardTabaleId);
            m_homeBtn = m_document.rootVisualElement.Query<Button>(c_homeButtonId);
            m_homeBtn.clicked += BackToHome;

            PlayServices.OnSignIn += FetchData;
        }

        private void OnDestroy() {
            PlayServices.OnSignIn -= FetchData;
            m_homeBtn.clicked -= BackToHome;
        }

        private void BackToHome() {
            m_uiPages.ShowHome();
        }

        private async void FetchData() {
            var result = await LeaderboardAPI.GetScores();
            m_table.SetData(result);
        }

        public void Show() {
            FetchData();
            m_document.rootVisualElement.style.display = DisplayStyle.Flex;
        }

        public void Hide() {
            m_document.rootVisualElement.style.display = DisplayStyle.None;
        }
    }
}