using UnityEngine;
using UnityEngine.VFX;

namespace IntoTheAbyss.Game {
    public class PlayerFXController : MonoBehaviour {

        [SerializeField] private AudioSource m_bubblesSound;
        [SerializeField] private AudioSource m_slideSound;
        [SerializeField] private AudioSource m_deathSound;

        private const string c_eatFXName = "BubblesFX";
        private VisualEffect m_bubblesFX;


        private void Start() {
            m_bubblesFX = transform.Find(c_eatFXName).GetComponent<VisualEffect>();

            Player.OnInhale += PlayBubblesFX;
            Player.OnSlide += PlaySlideFX;
            Player.OnDie += PlayDeathFX;
        }

        private void OnDestroy() {
            Player.OnInhale -= PlayBubblesFX;
            Player.OnSlide -= PlaySlideFX;
            Player.OnDie -= PlayDeathFX;
        }

        private void PlayBubblesFX() {
            //m_bubblesFX.Play();
            m_bubblesSound.Play();
        }

        private void PlaySlideFX() {
            m_slideSound.Play();
        }

        private void PlayDeathFX() {
            m_deathSound.Play();
        }
    }
}