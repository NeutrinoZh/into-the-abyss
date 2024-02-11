using IntoTheAbyss.Game;

using UnityEngine;

namespace IntoTheAbyss.UI {
    public class UIPages : MonoBehaviour {
        [SerializeField] private Transform m_menuUI;
        [SerializeField] private HUD m_hudUI;

        private void Start() {
            var dieController = Player.Instance.GetComponent<DieController>();
            dieController.OnDie += DieHandle;
        }

        private void OnDestroy() {
            var dieController = Player.Instance.GetComponent<DieController>();
            dieController.OnDie += DieHandle;
        }

        private void DieHandle() {
            m_hudUI.gameObject.SetActive(false);
            m_menuUI.gameObject.SetActive(true);
        }
    }
}