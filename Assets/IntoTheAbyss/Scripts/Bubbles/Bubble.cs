using UnityEngine;


namespace IntoTheAbyss.Game {
    public class Bubble : MonoBehaviour {
        [SerializeField] private int m_cellCountToDespawn;
        private int m_cellCount = 0;

        private void OnEnable() {
            m_cellCount = 0;
            Player.OnEveryCellFall += IncermentCellCount;
        }

        private void OnDisable() {
            Player.OnEveryCellFall -= IncermentCellCount;
        }

        private void IncermentCellCount() {
            m_cellCount += 1;
            if (m_cellCount >= m_cellCountToDespawn)
                BubblesManager.Instance.DestroyBubble(transform);
        }
    }
}