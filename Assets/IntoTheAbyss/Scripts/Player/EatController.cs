using UnityEngine;

namespace IntoTheAbyss.Game {
    public class EatController : MonoBehaviour {
        private const string c_bubblesTag = "Bubbles";

        private void OnTriggerEnter2D(Collider2D _other) {
            if (!_other.transform.CompareTag(c_bubblesTag))
                return;

            if (!_other.transform.TryGetComponent(out Bubble bubble))
                return;

            BubblesManager.Instance.DestroyBubble(bubble.transform);
            Player.OnInhale?.Invoke();
        }
    }
}