using UnityEngine;
using UnityEngine.VFX;

namespace IntoTheAbyss.Game {
    public class PlayerFXController : MonoBehaviour {

        [SerializeField] private AudioSource m_crunchSound;
        [SerializeField] private AudioSource m_slideSound;
        [SerializeField] private AudioSource m_deathSound;

        private const string c_eatFXName = "EatFX";
        private VisualEffect m_eatFX;


        private void Start() {
            m_eatFX = transform.Find(c_eatFXName).GetComponent<VisualEffect>();

            Player.OnEat += PlayEatFX;
            Player.OnSlide += PlaySlideFX;
            Player.OnDie += PlayDeathFX;
        }

        private void OnDestroy() {
            Player.OnEat -= PlayEatFX;
            Player.OnSlide -= PlaySlideFX;
            Player.OnDie -= PlayDeathFX;
        }

        private void PlayEatFX() {
            m_eatFX.Play();
            m_crunchSound.Play();
        }

        private void PlaySlideFX() {
            m_slideSound.Play();
        }

        private void PlayDeathFX() {
            m_deathSound.Play();
        }
    }
}