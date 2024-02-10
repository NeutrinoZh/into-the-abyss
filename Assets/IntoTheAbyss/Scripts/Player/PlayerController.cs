using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

namespace IntoTheAbyss.Game {
    public class PlayerController : MonoBehaviour {
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

            if (swipeDelta.x > 100 && m_can_swap) {
                transform.position += new Vector3(1.5f, 0, 0);
                m_can_swap = false;
            }

            if (swipeDelta.x < -100 && m_can_swap) {
                transform.position += new Vector3(-1.5f, 0, 0);
                m_can_swap = false;
            }
        }

        private void ProccessTouch(InputAction.CallbackContext _) {
            m_can_swap = true;
        }
    }
}