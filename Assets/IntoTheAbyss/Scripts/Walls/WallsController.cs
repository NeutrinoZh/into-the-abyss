using UnityEngine;

namespace IntoTheAbyss.Game {
    public class WallsController : MonoBehaviour {
        [SerializeField] private Transform m_camera;

        private Transform m_first_section;
        private Transform m_second_section;

        private const string c_firstSection = "FirstSection";
        private const string c_secondSection = "SecondSection";

        private void Start() {
            m_first_section = transform.Find(c_firstSection);
            m_second_section = transform.Find(c_secondSection);
        }

        private void Update() {
            int firstSection = (int)(m_camera.position.y - 15) / 15;
            int secondSection = (int)m_camera.position.y / 15;

            m_first_section.position = 15 * firstSection * Vector3.up;
            m_second_section.position = 15 * secondSection * Vector3.up;
        }
    }
}