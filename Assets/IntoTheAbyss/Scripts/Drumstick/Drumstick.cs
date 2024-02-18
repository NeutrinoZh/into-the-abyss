using UnityEngine;


namespace IntoTheAbyss.Game {
    public class Drumstick : MonoBehaviour {
        [SerializeField] private int m_cellCountToDespawn;
        private int m_cellCount = 0;

        private void OnEnable() {
            m_cellCount = 0;

            var fallingController = Player.Instance.GetComponent<FallingController>();
            fallingController.OnEveryCell += IncermentCellCount;
        }

        private void OnDisable() {
            var fallingController = Player.Instance.GetComponent<FallingController>();
            fallingController.OnEveryCell -= IncermentCellCount;
        }

        private void IncermentCellCount() {
            m_cellCount += 1;
            if (m_cellCount >= m_cellCountToDespawn)
                DrumstickManager.Instance.DestroyDrumstick(transform);
        }
    }
}