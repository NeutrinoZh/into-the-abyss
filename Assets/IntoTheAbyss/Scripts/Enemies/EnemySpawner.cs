using System.Collections.Generic;

using UnityEngine;

namespace IntoTheAbyss.Game {
    public class EnemySpawner : MonoBehaviour {
        [SerializeField] private List<Transform> m_spawnPoints;
        private readonly List<Transform> m_enemies = new() { };


        private void Start() {
            var fallingController = Player.Instance.GetComponent<FallingController>();
            fallingController.OnPerSection += SpawnEnemy;
        }

        private void SpawnEnemy() {
            EnemyType type = (EnemyType)Random.Range(0, 3);
            switch (type) {
                case EnemyType.SIDE_SPIKE:
                    SpawnSideSpike(false);
                    break;
                case EnemyType.SIDE_LONG_SPIKE:
                    SpawnSideSpike(true);
                    break;
                default:
                    break;
            }

        }

        private void SpawnSideSpike(bool isLong) {
            var enemy = EnemyManager.Instance.SpawnEnemy(isLong ? EnemyType.SIDE_LONG_SPIKE : EnemyType.SIDE_SPIKE);

            var isRight = Random.Range(0, 2) == 0;
            enemy.transform.position = m_spawnPoints[isRight ? ^1 : 0].position;

            enemy.GetComponent<SpriteRenderer>().flipY = isRight;

            m_enemies.Add(enemy);
            if (m_enemies.Count > 5) {
                EnemyManager.Instance.DestroyEnemy(m_enemies[0]);
                m_enemies.Remove(m_enemies[0]);
            }
        }
    }
}