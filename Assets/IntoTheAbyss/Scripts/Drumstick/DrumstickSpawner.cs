using System.Collections.Generic;

using IntoTheAbyss.Game;

using UnityEngine;

namespace IntoTheAbyss {
    public class DrumstickSpawner : MonoBehaviour {
        [SerializeField] private List<Transform> m_spawnPoints;

        private readonly List<Transform> m_drumsticks = new() { };

        private void Start() {
            var fallingController = Player.Instance.GetComponent<FallingController>();
            fallingController.OnPerSection += SpawnDrumstick;
        }

        private void SpawnDrumstick() {

        }

    }
}