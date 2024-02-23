using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Pool;

namespace IntoTheAbyss.Game {
    public class BubblesManager : MonoBehaviour {
        public static BubblesManager Instance { get; private set; }

        [SerializeField] private Bubble m_bubble;
        private ObjectPool<GameObject> m_bubblesPool;
        private readonly List<Transform> m_bubbles = new() { };

        // ==============================================//
        // API 

        public Transform SpawnBubble() {
            var drumstick = m_bubblesPool.Get();
            gameObject.SetActive(true);

            m_bubbles.Add(drumstick.transform);
            return drumstick.transform;
        }

        public void DestroyBubble(Transform _drumstick) {
            m_bubblesPool.Release(_drumstick.gameObject);
            m_bubbles.Remove(_drumstick);
        }

        public void Clear() {
            foreach (var drumstick in m_bubbles)
                m_bubblesPool.Release(drumstick.gameObject);
            m_bubbles.Clear();
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
            m_bubblesPool = new(
                () => Instantiate(m_bubble.gameObject, transform),
                obj => obj.SetActive(true),
                obj => obj.SetActive(false),
                obj => Destroy(obj),
                false, 10, 10
            );
        }

        private void SingltonGuard() {
            if (Instance) {
                Debug.LogWarning("Only one instance of BubblesManager can be instantinate");
                Destroy(gameObject);
            } else
                Instance = this;
        }
    }
}