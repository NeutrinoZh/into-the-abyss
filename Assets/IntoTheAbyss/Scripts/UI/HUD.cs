using IntoTheAbyss.Game;

using UnityEngine;
using UnityEngine.UIElements;

namespace IntoTheAbyss.UI {
    public class HUD : MonoBehaviour {
        private UIDocument m_document;
        private Label m_score;

        private string m_score_pattern;

        private void Awake() {
            m_document = GetComponent<UIDocument>();
            m_score = m_document.rootVisualElement.Query<Label>();
            m_score_pattern = m_score.text;
        }

        private void Start() {
            SessionManager.Instance.OnChangeScore += ChangeScoreHandle;
            ChangeScoreHandle();
        }

        private void OnDestroy() {
            SessionManager.Instance.OnChangeScore -= ChangeScoreHandle;
        }

        private void ChangeScoreHandle() {
            m_score.text = m_score_pattern.Replace("{}", SessionManager.Instance.Score.ToString());
        }
    }
}