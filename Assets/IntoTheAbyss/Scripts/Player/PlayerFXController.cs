using UnityEngine;
using UnityEngine.VFX;

namespace IntoTheAbyss.Game {
    public class PlayerFXController : MonoBehaviour {
        private const string c_eatFXName = "EatFX";
        private VisualEffect m_eatFX;

        private void Start() {
            m_eatFX = transform.Find(c_eatFXName).GetComponent<VisualEffect>();

            var eatController = GetComponent<EatController>();
            eatController.OnEat += PlayEatFX;
        }

        private void PlayEatFX() {
            m_eatFX.Play();
        }
    }
}