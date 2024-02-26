using UnityEngine;
using UnityEngine.UIElements;

namespace IntoTheAbyss.UI {
    public class Leaderboard : MonoBehaviour {
        [SerializeField] private UIPages m_uiPages;
        private UIDocument m_document;

        private Button m_homeBtn;

        private const string c_homeButtonId = "home__button";

        private void Awake() {
            m_document = GetComponent<UIDocument>();

            m_homeBtn = m_document.rootVisualElement.Query<Button>(c_homeButtonId);
            m_homeBtn.clicked += BackToHome;
        }

        private void OnDestroy() {
            m_homeBtn.clicked -= BackToHome;
        }

        private void BackToHome() {
            m_uiPages.ShowHome();
        }

        public void Show() {
            m_document.rootVisualElement.style.display = DisplayStyle.Flex;
        }

        public void Hide() {
            m_document.rootVisualElement.style.display = DisplayStyle.None;
        }
    }
}