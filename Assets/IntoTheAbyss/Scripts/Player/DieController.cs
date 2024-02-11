using System;

using UnityEngine;

namespace IntoTheAbyss.Game {
    public class DieController : MonoBehaviour {
        public event Action OnDie;

        private const string c_enemyTag = "Enemy";

        private void OnCollisionEnter2D(Collision2D _other) {
            if (!_other.transform.CompareTag(c_enemyTag))
                return;

            Time.timeScale = 0;
            OnDie?.Invoke();
        }
    }
}