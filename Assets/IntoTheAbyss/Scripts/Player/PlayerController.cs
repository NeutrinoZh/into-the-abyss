using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

namespace IntoTheAbyss.Game {
    public class PlayerController : MonoBehaviour {

        [SerializeField] Vector3 m_step;
        [SerializeField] private float m_sensitivity;

        [SerializeField] private float m_leftBorder;
        [SerializeField] private float m_rightBorder;

        private PlayerInput m_input;
        private bool m_can_swap = true;

        private void Awake() {
            m_input = GetComponent<PlayerInput>();
            Assert.IsNotNull(m_input);
        }

        private void Start() {
            m_input.Input.Player.Touch.canceled += ProccessTouch;
        }

        private void Update() {
            Vector2 swipeDelta = m_input.Input.Player.Swipe.ReadValue<Vector2>();

            if (swipeDelta.x > m_sensitivity && m_can_swap) {
                if (transform.position.x < m_rightBorder)
                    transform.position += m_step;

                Player.OnSlide?.Invoke();
                m_can_swap = false;
            }

            if (swipeDelta.x < -m_sensitivity && m_can_swap) {
                if (transform.position.x > m_leftBorder)
                    transform.position -= m_step;

                Player.OnSlide?.Invoke();
                m_can_swap = false;
            }
        }

        private void ProccessTouch(InputAction.CallbackContext _) {
            m_can_swap = true;
        }
    }
}