using System;

using IntoTheAbyss.Game;

using UnityEngine;
using UnityEngine.UIElements;

namespace IntoTheAbyss.UI {
    public class Menu : MonoBehaviour {
        public event Action OnRetry;

        private UIDocument m_document;
        private Label m_score;
        private Label m_highScore;
        private Label m_retryLbl;
        private Button m_retryBtn;
        private VisualElement m_popup;

        private string m_scorePattern;
        private string m_highScorePattern;

        private const string c_mainMenuPopupId = "mainmenu__popup";
        private const string c_scoreId = "score__label";
        private const string c_highScoreId = "high-score__label";
        private const string c_retryButtonId = "retry__button";
        private const string c_retryLabelId = "retry__label";
        private const string c_retryClass = "retry";

        private void Awake() {
            m_document = GetComponent<UIDocument>();
            m_popup = m_document.rootVisualElement.Query<VisualElement>(c_mainMenuPopupId);
            m_score = m_document.rootVisualElement.Query<Label>(c_scoreId);
            m_highScore = m_document.rootVisualElement.Query<Label>(c_highScoreId);
            m_retryLbl = m_document.rootVisualElement.Query<Label>(c_retryLabelId);
            m_retryBtn = m_document.rootVisualElement.Query<Button>(c_retryButtonId);

            // 
            m_scorePattern = m_score.text;
            m_highScorePattern = m_highScore.text;
            m_retryBtn.clicked += () => OnRetry?.Invoke();

            Show(false);
        }

        public void Show(bool _isRetry = true) {
            m_document.rootVisualElement.style.display = DisplayStyle.Flex;

            m_retryLbl.text = _isRetry ? "Retry" : "Play";
            if (_isRetry)
                m_popup.AddToClassList(c_retryClass);
            else if (m_popup.ClassListContains(c_retryClass))
                m_popup.RemoveFromClassList(c_retryClass);

            m_score.text = m_scorePattern.Replace("{}", SessionManager.Instance.Score.ToString());
            m_highScore.text = m_highScorePattern.Replace("{}", SessionManager.Instance.HighScore.ToString());
        }

        public void Hide() {
            m_document.rootVisualElement.style.display = DisplayStyle.None;
        }
    }
}