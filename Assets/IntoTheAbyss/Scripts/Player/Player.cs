using UnityEngine;

namespace IntoTheAbyss.Game {
    public class Player : MonoBehaviour {
        public static Player Instance { get; private set; }

        private void Awake() {
            if (Instance) {
                Debug.LogWarning("Only one instance of Player can be instantinate");
                Destroy(gameObject);
            } else
                Instance = this;
        }
    }
}