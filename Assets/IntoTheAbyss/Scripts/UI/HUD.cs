using IntoTheAbyss.Game;

using UnityEngine;
using UnityEngine.UIElements;

namespace IntoTheAbyss.UI {
    public class HUD : MonoBehaviour {
        private UIDocument m_document;

        private const string c_scoreLabelId = "score__label";
        private const string c_pauseButtonId = "pause__button";
        private const string c_hudId = "hud__container";
        private const string c_pausedClass = "paused";

        private VisualElement m_hudContainer;
        private Label m_score;
        private Button m_pauseBtn;

        private string m_score_pattern;

        private void Awake() {
            m_document = GetComponent<UIDocument>();

            m_hudContainer = m_document.rootVisualElement.Query<VisualElement>(c_hudId);
            m_score = m_document.rootVisualElement.Query<Label>(c_scoreLabelId);
            m_pauseBtn = m_document.rootVisualElement.Query<Button>(c_pauseButtonId);

            m_score_pattern = m_score.text;
        }

        private void Start() {
            SessionManager.Instance.OnChangeScore += ChangeScoreHandle;
            m_pauseBtn.clicked += PauseToggle;

            ChangeScoreHandle();
        }

        private void OnDestroy() {
            SessionManager.Instance.OnChangeScore -= ChangeScoreHandle;
            m_pauseBtn.clicked -= PauseToggle;
        }

        private void ChangeScoreHandle() {
            m_score.text = m_score_pattern.Replace("{}", SessionManager.Instance.Score.ToString());
        }

        private void PauseToggle() {
            if (m_hudContainer.ClassListContains(c_pausedClass)) {
                Time.timeScale = 1;
                m_hudContainer.RemoveFromClassList(c_pausedClass);
            } else {
                Time.timeScale = 0;
                m_hudContainer.AddToClassList(c_pausedClass);
            }
        }

        public void Show() {
            m_document.rootVisualElement.style.display = DisplayStyle.Flex;
        }

        public void Hide() {
            m_document.rootVisualElement.style.display = DisplayStyle.None;
        }
    }
}