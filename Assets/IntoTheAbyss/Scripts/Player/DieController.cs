using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace IntoTheAbyss.Game {
    public class DieController : MonoBehaviour {
        [SerializeField] private GameObject m_brokenPlayerPrefab;
        [SerializeField] private GameObject m_normalPlayer;
        [SerializeField] private float m_dieAnimationDuration;

        private const string c_enemyTag = "Enemy";

        private Coroutine m_dieCoroutine = null;

        private void OnCollisionEnter2D(Collision2D _other) {
            if (!_other.transform.CompareTag(c_enemyTag))
                return;

            m_dieCoroutine ??= StartCoroutine(DieAnimation());
        }

        private IEnumerator DieAnimation() {
            m_normalPlayer.SetActive(false);

            var brokenPlayer = Instantiate(m_brokenPlayerPrefab);
            brokenPlayer.transform.position = m_normalPlayer.transform.position;

            Player.OnDie?.Invoke();

            yield return new WaitForSeconds(m_dieAnimationDuration);

            Player.OnAfterDie?.Invoke();

            Destroy(brokenPlayer);
            m_dieCoroutine = null;

            m_normalPlayer.SetActive(true);
        }
    }
}