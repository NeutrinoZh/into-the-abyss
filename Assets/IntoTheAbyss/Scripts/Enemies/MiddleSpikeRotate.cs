using UnityEngine;

namespace IntoTheAbyss.Game {
    public class MiddleSpikeRotate : MonoBehaviour {
        [SerializeField] private Vector3 m_rotateVector;

        public void Update() {
            transform.Rotate(m_rotateVector * Time.deltaTime);
        }
    }
}