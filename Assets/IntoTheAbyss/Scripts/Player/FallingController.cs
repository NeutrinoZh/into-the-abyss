using System;

using UnityEngine;

namespace IntoTheAbyss.Game {
    public class FallingController : MonoBehaviour {

        public Action OnPerSection = null;
        private float m_nextSection = 0;

        [SerializeField] private float m_sectionHeight;

        [SerializeField] private float m_speed;
        [SerializeField] private float m_acceleration;

        private void Update() {
            m_speed += m_acceleration * Time.deltaTime;
            transform.position += m_speed * Time.deltaTime * Vector3.down;

            if (transform.position.y <= m_nextSection) {
                OnPerSection?.Invoke();
                m_nextSection -= m_sectionHeight;
            }
        }
    }
}