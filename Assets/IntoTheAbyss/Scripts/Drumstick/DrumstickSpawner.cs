using System.Collections.Generic;

using UnityEngine;

namespace IntoTheAbyss.Game {
    public class DrumstickSpawner : MonoBehaviour {
        [SerializeField] private List<Transform> m_spawnPoints;


        private void Start() {
            var fallingController = Player.Instance.GetComponent<FallingController>();
            fallingController.OnEveryCell += SpawnDrumstick;
        }

        private void OnDestroy() {
            var fallingController = Player.Instance.GetComponent<FallingController>();
            fallingController.OnEveryCell -= SpawnDrumstick;
        }

        private void SpawnDrumstick() {
            var drumstick = DrumstickManager.Instance.SpawnDrumstick();

            var ind = Random.Range(0, 3);
            drumstick.transform.position = m_spawnPoints[ind].position;
        }
    }
}