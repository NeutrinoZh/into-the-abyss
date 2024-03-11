using UnityEngine;

namespace IntoTheAbyss {
    public class CameraController : MonoBehaviour {
        [SerializeField] private Transform m_target;
        [SerializeField] private Vector3 m_offset;

        const float c_cameraSpeed = 180f;

        private void Update() {
            Vector3 direction =
                c_cameraSpeed * Time.deltaTime *
                (m_target.position + m_offset - transform.position).normalized;
            direction.z = direction.x = 0;

            transform.position += direction;
        }
    }
}