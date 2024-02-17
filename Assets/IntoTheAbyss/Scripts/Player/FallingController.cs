using System;

using UnityEngine;

namespace IntoTheAbyss.Game {
    public class FallingController : MonoBehaviour {

        public Action OnPerSection = null;
        public Action OnEveryCell = null;

        private float m_nextSection = 0;
        private float m_nextCell = 0;

        [SerializeField] private float m_sectionHeight;
        [SerializeField] private float m_cellHeight;

        [SerializeField] private float m_speed;
        [SerializeField] private float m_acceleration;

        private void Start() {
            SessionManager.Instance.OnRetry += () => {
                m_speed = 3f;
                m_nextSection = 0;
                m_nextCell = 0;
            };
        }

        private void Update() {
            m_speed += m_acceleration * Time.deltaTime;
            transform.position += m_speed * Time.deltaTime * Vector3.down;

            if (transform.position.y <= m_nextSection) {
                OnPerSection?.Invoke();
                m_nextSection -= m_sectionHeight;
            } else if (transform.position.y <= m_nextCell) {
                m_nextCell -= m_cellHeight;
            }
        }
    }
}