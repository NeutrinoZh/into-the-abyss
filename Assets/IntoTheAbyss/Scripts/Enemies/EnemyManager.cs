using UnityEngine;
using UnityEngine.Pool;

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

            PoolInit();
        }
        // ==============================================//


        [SerializeField] private Transform m_enemyPrefab;
        private ObjectPool<GameObject> m_enemies_pool;

        private void PoolInit() {
            m_enemies_pool = new(
                () => Instantiate(m_enemyPrefab.gameObject, transform),
                obj => obj.SetActive(true),
                obj => obj.SetActive(false),
                obj => Destroy(obj),
                false, 10, 10
            );
        }

        public Transform SpawnEnemy(EnemyType _type) {
            var enemy = m_enemies_pool.Get().transform;

            var child = enemy.transform.GetChild((int)_type);
            child.gameObject.SetActive(true);

            return child;
        }

        public void DestroyEnemy(Transform _enemy) {
            _enemy = _enemy.parent;

            foreach (Transform chlid in _enemy.transform)
                chlid.gameObject.SetActive(false);

            m_enemies_pool.Release(_enemy.gameObject);
        }
    }
}