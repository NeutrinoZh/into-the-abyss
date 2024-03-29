using System;

using IntoTheAbyss.UI;

using UnityEngine;

namespace IntoTheAbyss.Game {
    public class SessionManager : MonoBehaviour {
        //==============================================//
        // Singlton
        public static SessionManager Instance { get; private set; }

        private void Awake() {
            if (Instance) {
                Debug.LogWarning("Only one instance of SessionManager can be instantinate");
                Destroy(gameObject);
            } else
                Instance = this;
        }
        // ==============================================//

        public event Action OnChangeScore = null;
        public event Action OnRetry = null;

        private int m_score;
        public int Score {
            get => m_score;
            private set {
                m_score = value;
                OnChangeScore?.Invoke();
            }
        }

        public int HighScore {
            get => PlayerPrefs.GetInt("highScore");
            set => PlayerPrefs.SetInt("highScore", value);
        }

        [SerializeField] private UIPages m_pages;

        private void Start() {
            Player.OnInhale += Scoring;
            Player.OnAfterDie += DieHandle;

            m_pages.Menu.OnRetry += RetryHandle;
        }

        private void OnDestroy() {
            Player.OnInhale -= Scoring;
            Player.OnInhale -= DieHandle;
        }

        private void Scoring() {
            Score += 1;
        }

        private void DieHandle() {
            if (Score > HighScore)
                HighScore = Score;
            LeaderboardAPI.AddScore(HighScore);
        }

        private void RetryHandle() {
            Score = 0;
            OnRetry?.Invoke();
        }
    }
}