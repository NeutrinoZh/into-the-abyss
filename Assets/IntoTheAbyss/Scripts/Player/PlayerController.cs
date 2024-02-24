using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using DG.Tweening.Core;
using DG.Tweening;

namespace IntoTheAbyss.Game {
    public class PlayerController : MonoBehaviour {

        [SerializeField] Vector3 m_step;
        [SerializeField] private float m_sensitivity;

        [SerializeField] private float m_leftBorder;
        [SerializeField] private float m_rightBorder;

        [SerializeField] private float m_slideDuration;

        private PlayerInput m_input;
        private bool m_canSlide = true;

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

        private void Slide(float _shift) {
            DOTween.To(
                () => transform.position.x,
                x => transform.position = new Vector3(
                    x, transform.position.y, transform.position.z
                ),
                transform.position.x + _shift,
                m_slideDuration
            );
        }

        private void ProccessTouch(InputAction.CallbackContext _context) {
            var touch = _context.ReadValue<TouchState>();
            Vector2 slideDelta = touch.position - touch.startPosition;

            if (slideDelta.x > m_sensitivity && m_canSlide) {
                if (transform.position.x < m_rightBorder)
                    Slide(m_step.x);

                Player.OnSlide?.Invoke();
                m_canSlide = false;
            }

            if (slideDelta.x < -m_sensitivity && m_canSlide) {
                if (transform.position.x > m_leftBorder)
                    Slide(-m_step.x);

                Player.OnSlide?.Invoke();
                m_canSlide = false;
            }

            if (slideDelta.x == 0)
                m_canSlide = true;
        }
    }
}