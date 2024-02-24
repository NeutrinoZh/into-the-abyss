using System;

using UnityEngine;

namespace IntoTheAbyss.Game {
    public class Player : MonoBehaviour {
        public static Player Instance { get; private set; }

        private void Awake() {
            if (Instance) {
                Debug.LogWarning("Only one instance of Player can be instantinate");
                Destroy(gameObject);
                return;
            } else
                Instance = this;
        }

        public static Action OnInhale = null;
        public static Action OnDie = null;
        public static Action OnAfterDie = null;
        public static Action OnPerSectionFall = null;
        public static Action OnEveryCellFall = null;
        public static Action OnSlide = null;
    }
}