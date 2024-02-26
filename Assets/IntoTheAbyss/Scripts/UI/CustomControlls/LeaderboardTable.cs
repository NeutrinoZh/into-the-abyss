using System.Collections.Generic;

using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace IntoTheAbyss.UI {
    public class LeaderboardTable : VisualElement {
        public new class UxmlFactory : UxmlFactory<LeaderboardTable> { }

        private const int c_topPlaces = 3;
        private const int c_bottomPlaces = 3;

        private readonly List<LeaderboardLine> m_leaderboardLines = new();

        public LeaderboardTable() {
            style.height = Length.Percent(80);
            style.justifyContent = Justify.SpaceBetween;

            for (int i = 0; i < c_topPlaces; ++i) {
                var line = new LeaderboardLine();
                m_leaderboardLines.Add(line);
                Add(line);
            }

            VisualElement separatorLine = new() {
                style = {
                    width = Length.Percent(100),
                    height = 10,
                    backgroundColor = new StyleColor(new UnityEngine.Color(0f, 0f, 0f)),
                }
            };

            Add(separatorLine);

            for (int i = 0; i < c_bottomPlaces; ++i) {
                var line = new LeaderboardLine();
                m_leaderboardLines.Add(line);
                Add(line);
            }
        }

        public void SetData(int[] _places, string[] _nicknames, int[] _scores) {
            Assert.AreEqual(_places.Length, c_topPlaces + c_bottomPlaces);
            Assert.AreEqual(_nicknames.Length, c_topPlaces + c_bottomPlaces);
            Assert.AreEqual(_scores.Length, c_topPlaces + c_bottomPlaces);

            for (int i = 0; i < c_topPlaces + c_bottomPlaces; ++i)
                m_leaderboardLines[i].SetNameAndScore(_places[i], _nicknames[i], _scores[i]);
        }
    }
}