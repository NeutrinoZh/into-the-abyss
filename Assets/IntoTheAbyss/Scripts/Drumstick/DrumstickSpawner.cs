using System.Collections.Generic;

using UnityEngine;

namespace IntoTheAbyss.Game {
    public class DrumstickSpawner : MonoBehaviour {
        [SerializeField] private List<Transform> m_spawnPoints;


        private void Start() {
            Player.OnEveryCellFall += SpawnDrumstick;
        }

        private void OnDestroy() {
            Player.OnEveryCellFall -= SpawnDrumstick;
        }

        private void SpawnDrumstick() {
            var drumstick = DrumstickManager.Instance.SpawnDrumstick();

            var ind = Random.Range(0, 3);
            drumstick.transform.position = m_spawnPoints[ind].position;
        }
    }
}