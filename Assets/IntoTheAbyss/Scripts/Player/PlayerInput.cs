using UnityEngine;

namespace IntoTheAbyss.Game {
    public class PlayerInput : MonoBehaviour {
        public InputActions Input { get; private set; }

        private void Awake() {
            Input = new();
            Input.Enable();
        }

        private void OnDestroy() {
            Input.Disable();
        }
    }
}