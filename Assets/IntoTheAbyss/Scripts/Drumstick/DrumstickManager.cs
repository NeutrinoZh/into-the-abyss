using UnityEngine;
using UnityEngine.Pool;

namespace IntoTheAbyss {
    public class DrumstickManager : MonoBehaviour {
        //==============================================//
        // Singlton
        public static DrumstickManager Instance { get; private set; }

        private void Awake() {
            if (Instance) {
                Debug.LogWarning("Only one instance of DrumstickManager can be instantinate");
                Destroy(gameObject);
            } else
                Instance = this;

            PoolInit();
        }
        // ==============================================//

        [SerializeField] private Drumstick m_drumstick;
        private ObjectPool<GameObject> m_drumstick_pool;

        private void PoolInit() {
            m_drumstick_pool = new(
                () => Instantiate(m_drumstick.gameObject, transform),
                obj => obj.SetActive(true),
                obj => obj.SetActive(false),
                obj => Destroy(obj),
                false, 10, 10
            );
        }

        public Transform SpawnDrumstick() {
            var drumstick = m_drumstick_pool.Get();
            gameObject.SetActive(true);
            return drumstick.transform;
        }

        public void DestroyEnemy(Transform _enemy) {
            _enemy = _enemy.parent;

            foreach (Transform chlid in _enemy.transform)
                chlid.gameObject.SetActive(false);

            m_drumstick_pool.Release(_enemy.gameObject);
        }
    }
}