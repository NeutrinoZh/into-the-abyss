using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Pool;

namespace IntoTheAbyss.Game {
    public class DrumstickManager : MonoBehaviour {
        public static DrumstickManager Instance { get; private set; }

        [SerializeField] private Drumstick m_drumstick;
        private ObjectPool<GameObject> m_drumstick_pool;
        private readonly List<Transform> m_drumsticks = new() { };

        // ==============================================//
        // API 

        public Transform SpawnDrumstick() {
            var drumstick = m_drumstick_pool.Get();
            gameObject.SetActive(true);

            m_drumsticks.Add(drumstick.transform);
            return drumstick.transform;
        }

        public void DestroyDrumstick(Transform _drumstick) {
            m_drumstick_pool.Release(_drumstick.gameObject);
            m_drumsticks.Remove(_drumstick);
        }

        public void Clear() {
            foreach (var drumstick in m_drumsticks)
                m_drumstick_pool.Release(drumstick.gameObject);
            m_drumsticks.Clear();
        }

        // ==============================================//
        // Internal 

        private void Awake() {
            SingltonGuard();
            PoolInit();
        }

        private void Start() {
            SessionManager.Instance.OnRetry += Clear;
        }

        private void OnDestroy() {
            SessionManager.Instance.OnRetry -= Clear;
        }

        private void PoolInit() {
            m_drumstick_pool = new(
                () => Instantiate(m_drumstick.gameObject, transform),
                obj => obj.SetActive(true),
                obj => obj.SetActive(false),
                obj => Destroy(obj),
                false, 10, 10
            );
        }

        private void SingltonGuard() {
            if (Instance) {
                Debug.LogWarning("Only one instance of DrumstickManager can be instantinate");
                Destroy(gameObject);
            } else
                Instance = this;
        }
    }
}