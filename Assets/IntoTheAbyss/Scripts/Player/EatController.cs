using UnityEngine;

namespace IntoTheAbyss.Game {
    public class EatController : MonoBehaviour {
        private const string c_drumstickTag = "Drumstick";

        private void OnCollisionEnter2D(Collision2D _other) {
            if (!_other.transform.CompareTag(c_drumstickTag))
                return;

            if (!_other.transform.TryGetComponent(out Drumstick drumstick))
                return;

            DrumstickManager.Instance.DestroyDrumstick(drumstick.transform);
            Player.OnEat?.Invoke();
        }
    }
}