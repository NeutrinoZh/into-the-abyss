using System.Collections.Generic;

using UnityEngine;

namespace IntoTheAbyss.Game {
    public class EnemySpawner : MonoBehaviour {
        [SerializeField] private Transform m_spikePrefab;
        [SerializeField] private List<Transform> m_spawnPoints;


        private void Start() {
            var fallingController = Player.Instance.GetComponent<FallingController>();
            fallingController.OnPerSection += SpawnEnemy;
        }

        private void OnDestroy() {
            var fallingController = Player.Instance.GetComponent<FallingController>();
            fallingController.OnPerSection -= SpawnEnemy;
        }

        private void SpawnEnemy() {
            var enemy = Instantiate(m_spikePrefab, EnemyManager.Instance.transform);

            var isRight = Random.Range(0, 2) == 0;
            enemy.transform.position = m_spawnPoints[isRight ? ^1 : 0].position;

            enemy.GetComponent<SpriteRenderer>().flipY = isRight;
        }
    }
}