using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace IntoTheAbyss.UI {
    public class LeaderboardTable : VisualElement {
        public new class UxmlFactory : UxmlFactory<LeaderboardTable> { }

        private const int c_topPlaces = 5;
        private const int c_bottomPlaces = 5;

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
                    height = 5,
                    backgroundColor = new StyleColor(new Color(0.93f, 0.7f, 0.41f)),
                }
            };

            Add(separatorLine);

            for (int i = 0; i < c_bottomPlaces; ++i) {
                var line = new LeaderboardLine();
                m_leaderboardLines.Add(line);
                Add(line);
            }
        }

        public void SetData(List<string[]> _data) {
            if (_data.Count > c_topPlaces + c_bottomPlaces) {
                Debug.LogError("(Leaderboard table): Incorrect data");
                return;
            }

            for (int i = 0; i < c_topPlaces + c_bottomPlaces; ++i)
                if (i >= _data.Count)
                    m_leaderboardLines[i].SetNameAndScore("0", "placeholder", "0");
                else
                    m_leaderboardLines[i].SetNameAndScore(_data[i][0], _data[i][1], _data[i][2]);
        }
    }
}