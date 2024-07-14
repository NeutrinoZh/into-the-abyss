using System.Collections.Generic;

using UnityEngine;

namespace IntoTheAbyss.Game {
    public class RetrySystem : MonoBehaviour {
        [SerializeField] private List<GameObject> m_returnables;
        private readonly List<Vector3> m_positions = new() { };

        private void Start() {
            SessionManager.Instance.OnRetry += Retry; 
            foreach (var returnable in m_returnables)
                m_positions.Add(returnable.transform.position);
        }

        private void Retry() {
            for (int i = 0; i < m_returnables.Count; ++i) 
                m_returnables[i].transform.position = m_positions[i];
        }
    }
}