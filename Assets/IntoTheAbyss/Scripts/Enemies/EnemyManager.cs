using System.Collections.Generic;

using UnityEngine;

namespace IntoTheAbyss.Game {
    public class EnemyManager : MonoBehaviour {
        //==============================================//
        // Singlton
        public static EnemyManager Instance { get; private set; }

        private void Awake() {
            if (Instance) {
                Debug.LogWarning("Only one instance of EnemyManager can be instantinate");
                Destroy(gameObject);
            } else
                Instance = this;
        }
        // ==============================================//

        private readonly List<Transform> m_enemies = new();

        public void AddEnemy(Transform _enemy) {
            m_enemies.Add(_enemy);
        }
    }
}