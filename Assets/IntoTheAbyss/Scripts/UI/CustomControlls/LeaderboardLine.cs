using UnityEngine.UIElements;

namespace IntoTheAbyss.UI {
    public class LeaderboardLine : VisualElement {
        public new class UxmlFactory : UxmlFactory<LeaderboardLine> { }

        private readonly Label m_nickname;
        private readonly Label m_score;

        public LeaderboardLine() {
            VisualElement line = new() {
                style = {
                    flexDirection = FlexDirection.Row,
                    justifyContent = Justify.SpaceBetween
                }
            };

            m_nickname = new() {
                text = "#1 nickname"
            };

            m_score = new() {
                text = "score"
            };

            line.Add(m_nickname);
            line.Add(m_score);

            Add(line);
        }

        public void SetNameAndScore(int _place, string _nickname, int _score) {
            m_nickname.text = $"#{_place} {_nickname}";
            m_score.text = _score.ToString();
        }
    }
}