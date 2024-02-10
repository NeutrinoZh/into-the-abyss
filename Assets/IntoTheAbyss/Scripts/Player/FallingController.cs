using UnityEngine;

namespace IntoTheAbyss {
    public class FallingController : MonoBehaviour {
        [SerializeField] private float m_speed;
        [SerializeField] private float m_acceleration;

        private void Update() {
            m_speed += m_acceleration * Time.deltaTime;
            transform.position += m_speed * Time.deltaTime * Vector3.down;
        }
    }
}