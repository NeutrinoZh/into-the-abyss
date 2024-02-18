using UnityEngine;
using UnityEngine.VFX;

namespace IntoTheAbyss.Game {
    public class PlayerFXController : MonoBehaviour {
        private const string c_eatFXName = "EatFX";
        private VisualEffect m_eatFX;
        private AudioSource m_audioSource;

        private void Start() {
            var fxObj = transform.Find(c_eatFXName);

            m_eatFX = fxObj.GetComponent<VisualEffect>();
            m_audioSource = fxObj.GetComponent<AudioSource>();

            var eatController = GetComponent<EatController>();
            eatController.OnEat += PlayEatFX;
        }

        private void PlayEatFX() {
            m_eatFX.Play();
            m_audioSource.Play();
        }
    }
}