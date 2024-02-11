using System;

using IntoTheAbyss.Game;

using UnityEngine;
using UnityEngine.UIElements;

namespace IntoTheAbyss.UI {
    public class Menu : MonoBehaviour {
        public event Action OnRetry;

        private UIDocument m_document;
        private Label m_score;
        private Label m_gameOver;
        private Button m_retry;

        private string m_scorePattern;

        private const string c_scoreId = "Score";
        private const string c_retryId = "Retry";
        private const string c_gameOverId = "GameOver";

        private void Awake() {
            m_document = GetComponent<UIDocument>();
            m_gameOver = m_document.rootVisualElement.Query<Label>(c_gameOverId);
            m_score = m_document.rootVisualElement.Query<Label>(c_scoreId);
            m_retry = m_document.rootVisualElement.Query<Button>(c_retryId);

            // 
            m_scorePattern = m_score.text;
            m_retry.clicked += () => OnRetry?.Invoke();

            // first show (play variant)
            m_retry.text = "Play";
            m_score.visible = false;
            m_gameOver.visible = false;
        }

        public void Show() {
            m_document.rootVisualElement.style.display = DisplayStyle.Flex;

            m_retry.text = "Retry";
            m_score.visible = true;
            m_gameOver.visible = true;

            m_score.text = m_scorePattern.Replace("{}", SessionManager.Instance.Score.ToString());
        }

        public void Hide() {
            m_document.rootVisualElement.style.display = DisplayStyle.None;
        }
    }
}