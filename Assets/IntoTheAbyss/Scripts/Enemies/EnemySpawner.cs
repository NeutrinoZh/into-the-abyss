using System.Collections.Generic;

using UnityEngine;

namespace IntoTheAbyss.Game {
    public class EnemySpawner : MonoBehaviour {
        [SerializeField] private List<Transform> m_spawnPoints;
        private readonly List<Transform> m_enemies = new() { };

        private void Start() {
            Player.OnPerSectionFall += SpawnEnemy;

            SessionManager.Instance.OnRetry += () => {
                foreach (var enemy in m_enemies)
                    EnemyManager.Instance.DestroyEnemy(enemy);

                m_enemies.Clear();
            };
        }

        private void OnDestroy() {
            Player.OnPerSectionFall -= SpawnEnemy;
        }

        private void SpawnEnemy() {
            EnemyType type = (EnemyType)Random.Range(0, 3);
            var enemy = EnemyManager.Instance.SpawnEnemy(type);

            switch (type) {
                case EnemyType.SIDE_SPIKE:
                    SpawnSideSpike(enemy);
                    break;
                case EnemyType.SIDE_LONG_SPIKE:
                    SpawnSideSpike(enemy);
                    break;
                case EnemyType.MIDDLE_SPIKE:
                    SpawnMiddleSpike(enemy);
                    break;
                default:
                    break;
            }

            m_enemies.Add(enemy);
            if (m_enemies.Count > 5) {
                EnemyManager.Instance.DestroyEnemy(m_enemies[0]);
                m_enemies.Remove(m_enemies[0]);
            }
        }

        private void SpawnMiddleSpike(Transform _enemy) {
            var spawnpoint = m_spawnPoints[1];
            _enemy.transform.position = spawnpoint.position;
        }

        private void SpawnSideSpike(Transform _enemy) {
            var isRight = Random.Range(0, 2) == 0;
            _enemy.transform.parent.position = m_spawnPoints[isRight ? ^1 : 0].position;

            _enemy.transform.localPosition = new Vector3(
                isRight ?
                    -Mathf.Abs(_enemy.transform.localPosition.x) :
                     Mathf.Abs(_enemy.transform.localPosition.x),
                _enemy.transform.localPosition.y,
                _enemy.transform.localPosition.z
            );

            _enemy.transform.localScale = new Vector3(
                isRight ?
                    -Mathf.Abs(_enemy.transform.localScale.x) :
                     Mathf.Abs(_enemy.transform.localScale.x),
                _enemy.transform.localScale.y,
                _enemy.transform.localScale.z
            );
        }
    }
}