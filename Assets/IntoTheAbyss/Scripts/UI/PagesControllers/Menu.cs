using System;

using IntoTheAbyss.Game;

using Unity.Services.Authentication;

using UnityEngine;
using UnityEngine.UIElements;

namespace IntoTheAbyss.UI {
    public class Menu : MonoBehaviour {
        public event Action OnRetry;

        [SerializeField] private UIPages m_uiPages;

        private UIDocument m_document;
        private Label m_score;
        private Label m_highScore;
        private Label m_retryLbl;
        private Button m_retryBtn;
        private Button m_soundBtn;
        private Button m_leaderboardBtn;
        private VisualElement m_popup;

        private string m_scorePattern;
        private string m_highScorePattern;

        private const string c_mainMenuPopupId = "mainmenu__popup";
        private const string c_scoreId = "score__label";
        private const string c_highScoreId = "high-score__label";
        private const string c_soundId = "sound__button";
        private const string c_retryButtonId = "retry__button";
        private const string c_retryLabelId = "retry__label";
        private const string c_retryClass = "retry";
        private const string c_soundOffClass = "sound-off";
        private const string c_leaderboardButtonId = "leaderboard__button";
        private const string c_signinClass = "signin";

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

        private void Awake() {
            m_document = GetComponent<UIDocument>();
            m_popup = m_document.rootVisualElement.Query<VisualElement>(c_mainMenuPopupId);
            m_score = m_document.rootVisualElement.Query<Label>(c_scoreId);
            m_highScore = m_document.rootVisualElement.Query<Label>(c_highScoreId);
            m_retryLbl = m_document.rootVisualElement.Query<Label>(c_retryLabelId);
            m_retryBtn = m_document.rootVisualElement.Query<Button>(c_retryButtonId);
            m_soundBtn = m_document.rootVisualElement.Query<Button>(c_soundId);
            m_leaderboardBtn = m_document.rootVisualElement.Query<Button>(c_leaderboardButtonId);

            // 
            m_scorePattern = m_score.text;
            m_highScorePattern = m_highScore.text;
            m_retryBtn.clicked += Retry;
            m_soundBtn.clicked += TurnSound;
            m_leaderboardBtn.clicked += SwitchToLeaderboard;

            Show(false);

            PlayServices.OnSignIn += TurnOnSignIn;
        }

        private void OnDestroy() {
            m_retryBtn.clicked -= Retry;
            m_soundBtn.clicked -= TurnSound;
            m_leaderboardBtn.clicked -= SwitchToLeaderboard;
            PlayServices.OnSignIn -= TurnOnSignIn;
        }

        private void Retry() {
            OnRetry?.Invoke();
        }

        private void TurnOnSignIn() {
            m_popup.AddToClassList(c_signinClass);
        }

        private void TurnSound() {
            AudioListener.volume = (int)AudioListener.volume == 1 ? 0 : 1;

            if (m_popup.ClassListContains(c_soundOffClass))
                m_popup.RemoveFromClassList(c_soundOffClass);
            else
                m_popup.AddToClassList(c_soundOffClass);
        }

        private void SwitchToLeaderboard() {
            if (AuthenticationService.Instance.IsSignedIn)
                m_uiPages.ShowLeaderboard();
        }
    }
}