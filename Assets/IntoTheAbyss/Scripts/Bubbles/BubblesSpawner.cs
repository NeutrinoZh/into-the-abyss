using System.Collections.Generic;

using UnityEngine;

namespace IntoTheAbyss.Game {
    public class BubblesSpawner : MonoBehaviour {
        [SerializeField] private List<Transform> m_spawnPoints;


        private void Start() {
            Player.OnEveryCellFall += SpawnBubble;
        }

        private void OnDestroy() {
            Player.OnEveryCellFall -= SpawnBubble;
        }

        private void SpawnBubble() {
            var bubble = BubblesManager.Instance.SpawnBubble();

            var ind = Random.Range(0, 3);
            bubble.transform.position = m_spawnPoints[ind].position;
        }
    }
}