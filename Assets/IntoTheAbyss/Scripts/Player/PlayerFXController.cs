using UnityEngine;
using UnityEngine.VFX;

namespace IntoTheAbyss.Game {
    public class PlayerFXController : MonoBehaviour {
        private const string c_eatFXName = "EatFX";

        [SerializeField] private AudioClip m_crunch;
        [SerializeField] private AudioClip m_slide;

        private VisualEffect m_eatFX;
        private AudioSource m_audioSource;

        private void Start() {
            m_audioSource = GetComponent<AudioSource>();
            m_eatFX = transform.Find(c_eatFXName).GetComponent<VisualEffect>();

            var eatController = GetComponent<EatController>();
            eatController.OnEat += PlayEatFX;
        }

        private void PlayEatFX() {
            m_eatFX.Play();

            m_audioSource.clip = m_crunch;
            m_audioSource.Play();
        }
    }
}