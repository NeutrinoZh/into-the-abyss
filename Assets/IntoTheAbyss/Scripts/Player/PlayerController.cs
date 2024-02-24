using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace IntoTheAbyss.Game {
    public class PlayerController : MonoBehaviour {

        [SerializeField] Vector3 m_step;
        [SerializeField] private float m_sensitivity;

        [SerializeField] private float m_leftBorder;
        [SerializeField] private float m_rightBorder;

        private PlayerInput m_input;
        private bool m_can_slide = true;

        private void Awake() {
            m_input = GetComponent<PlayerInput>();
            Assert.IsNotNull(m_input);
        }

        private void Start() {
            m_input.Input.Player.Touch.performed += ProccessTouch;
        }

        private void OnDestroy() {
            m_input.Input.Player.Touch.performed -= ProccessTouch;
        }

        private void ProccessTouch(InputAction.CallbackContext _context) {
            var touch = _context.ReadValue<TouchState>();
            Vector2 slideDelta = touch.position - touch.startPosition;

            if (slideDelta.x > m_sensitivity && m_can_slide) {
                if (transform.position.x < m_rightBorder)
                    transform.position += m_step;

                Player.OnSlide?.Invoke();
                m_can_slide = false;
            }

            if (slideDelta.x < -m_sensitivity && m_can_slide) {
                if (transform.position.x > m_leftBorder)
                    transform.position -= m_step;

                Player.OnSlide?.Invoke();
                m_can_slide = false;
            }

            if (slideDelta.x == 0)
                m_can_slide = true;
        }
    }
}