using System.Collections.Generic;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Unity.Services.Authentication;
using Unity.Services.Leaderboards;

using UnityEngine;

namespace IntoTheAbyss {
    public class LeaderboardAPI : MonoBehaviour {
        private const string c_leaderboardId = "IntoTheAbyss";

        public static async void AddScore(int _score) {
            if (!AuthenticationService.Instance.IsSignedIn)
                return;

            await LeaderboardsService.Instance.AddPlayerScoreAsync(c_leaderboardId, _score);
        }

        public static async Task<List<string[]>> GetScores() {
            var result = new List<string[]>() { };

            if (!AuthenticationService.Instance.IsSignedIn)
                return result;

            var playerScores = await LeaderboardsService.Instance.GetPlayerScoreAsync(c_leaderboardId);

            var topOptions = new GetScoresOptions { Offset = 0, Limit = playerScores.Rank < 10 ? 10 : 5 };
            var topScores = await LeaderboardsService.Instance.GetScoresAsync(c_leaderboardId, topOptions);
            for (int i = 0; i < topScores.Results.Count; ++i)
                result.Add(new string[]{
                    (topScores.Results[i].Rank + 1).ToString(),
                    topScores.Results[i].PlayerName,
                    topScores.Results[i].Score.ToString()
                });

            if (playerScores.Rank >= 10) {
                var bottomOptions = new GetPlayerRangeOptions { RangeLimit = 2 };
                var bottomScores = await LeaderboardsService.Instance.GetPlayerRangeAsync(c_leaderboardId, bottomOptions);
                for (int i = 0; i < bottomScores.Results.Count; ++i)
                    result.Add(new string[]{
                    (bottomScores.Results[i].Rank + 1).ToString(),
                    bottomScores.Results[i].PlayerName,
                    bottomScores.Results[i].Score.ToString()
                });
            }

            return result;
        }
    }
}