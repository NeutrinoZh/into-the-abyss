using UnityEngine;

namespace IntoTheAbyss.Game {
    public class SessionManager : MonoBehaviour {
        public int Score { get; private set; }

        private void Start() {
            var fallingController = Player.Instance.GetComponent<FallingController>();
            fallingController.OnPerSection += Scoring;
        }

        private void Scoring() {
            Score += 1;
        }
    }
}