using IntoTheAbyss.Game;

using UnityEngine;

namespace IntoTheAbyss.UI {
    public class UIPages : MonoBehaviour {
        [SerializeField] private Menu m_menuUI;
        [SerializeField] private HUD m_hudUI;

        public Menu Menu => m_menuUI;

        private void Start() {
            Player.OnDie += DieHandle;
            m_menuUI.OnRetry += RetryHandle;

            m_hudUI.Hide();
            Time.timeScale = 0;
        }

        private void OnDestroy() {
            Player.OnDie -= DieHandle;
            m_menuUI.OnRetry -= RetryHandle;
        }

        private void DieHandle() {
            Time.timeScale = 0;

            m_hudUI.Hide();
            m_menuUI.Show();
        }

        private void RetryHandle() {
            Time.timeScale = 1;

            m_hudUI.Show();
            m_menuUI.Hide();
        }
    }
}