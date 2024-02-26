using IntoTheAbyss.Game;

using UnityEngine;

namespace IntoTheAbyss.UI {
    public class UIPages : MonoBehaviour {
        [SerializeField] private Leaderboard m_leaderboardUI;
        [SerializeField] private Menu m_menuUI;
        [SerializeField] private HUD m_hudUI;

        public Menu Menu => m_menuUI;

        private void Start() {
            Player.OnAfterDie += DieHandle;
            m_menuUI.OnRetry += RetryHandle;

            m_hudUI.Hide();
            m_leaderboardUI.Hide();
            Time.timeScale = 0;
        }

        private void OnDestroy() {
            Player.OnAfterDie -= DieHandle;
            m_menuUI.OnRetry -= RetryHandle;
        }

        private void DieHandle() {
            Time.timeScale = 0;
            ShowHome();
        }

        private void RetryHandle() {
            Time.timeScale = 1;
            ShowHUD();
        }

        //============================================//

        public void ShowHUD() {
            m_menuUI.Hide();
            m_leaderboardUI.Hide();
            m_hudUI.Show();
        }

        public void ShowLeaderboard() {
            m_hudUI.Hide();
            m_menuUI.Hide();
            m_leaderboardUI.Show();
        }

        public void ShowHome() {
            m_hudUI.Hide();
            m_leaderboardUI.Hide();
            m_menuUI.Show();
        }
    }
}