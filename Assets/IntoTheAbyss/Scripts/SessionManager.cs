using System;

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

        private int m_score;
        public int Score {
            get => m_score;
            private set {
                m_score = value;
                OnChangeScore?.Invoke();
            }
        }

        private void Start() {
            var fallingController = Player.Instance.GetComponent<FallingController>();
            fallingController.OnPerSection += Scoring;
        }

        private void Scoring() {
            Score += 1;
        }
    }
}