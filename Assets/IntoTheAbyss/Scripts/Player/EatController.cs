using UnityEngine;

namespace IntoTheAbyss.Game {
    public class EatController : MonoBehaviour {
        private const string c_drumstickTag = "Drumstick";

        private void OnTriggerEnter2D(Collider2D _other) {
            if (!_other.transform.CompareTag(c_drumstickTag))
                return;

            if (!_other.transform.TryGetComponent(out Bubble drumstick))
                return;

            BubblesManager.Instance.DestroyBubble(drumstick.transform);
            Player.OnEat?.Invoke();
        }
    }
}