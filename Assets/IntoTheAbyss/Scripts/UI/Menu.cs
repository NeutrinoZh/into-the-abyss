using System;

using IntoTheAbyss.Game;

using UnityEngine;
using UnityEngine.UIElements;

namespace IntoTheAbyss.UI
{
    public class Menu : MonoBehaviour
    {
        public event Action OnRetry;

        private UIDocument m_document;
        private Label m_score;
        private Label m_highScore;
        private Button m_retry;

        private string m_scorePattern;
        private string m_highScorePattern;

        private const string c_scoreId = "score__label";
        private const string c_highScoreId = "high-score__label";
        private const string c_retryId = "retry__button";

        private void Awake()
        {
            m_document = GetComponent<UIDocument>();
            m_score = m_document.rootVisualElement.Query<Label>(c_scoreId);
            m_highScore = m_document.rootVisualElement.Query<Label>(c_highScoreId);
            m_retry = m_document.rootVisualElement.Query<Button>(c_retryId);

            // 
            m_scorePattern = m_score.text;
            m_highScorePattern = m_highScore.text;
            m_retry.clicked += () => OnRetry?.Invoke();

            ShowPlay();
        }

        private void ShowPlay()
        {
            m_retry.text = "Play";
            m_score.visible = false;
            m_highScore.text = m_highScorePattern.Replace("{}", SessionManager.Instance.HighScore.ToString());
        }

        public void Show()
        {
            m_document.rootVisualElement.style.display = DisplayStyle.Flex;

            m_retry.text = "Retry";
            m_score.visible = true;

            m_score.text = m_scorePattern.Replace("{}", SessionManager.Instance.Score.ToString());
            m_highScore.text = m_highScorePattern.Replace("{}", SessionManager.Instance.HighScore.ToString());
        }

        public void Hide()
        {
            m_document.rootVisualElement.style.display = DisplayStyle.None;
        }
    }
}