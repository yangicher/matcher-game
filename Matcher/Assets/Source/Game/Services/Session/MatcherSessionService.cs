using System.Collections.Generic;
using System.Threading.Tasks;
using Matcher.Game.Data;
using Matcher.Game.Services.Database;

namespace Matcher.Game.Services.Session
{
    public class MatcherSessionService : ISessionService
    {
        private const string LEADERBOARD_COLLECTION = "leaderboard";

        private readonly IDatabaseService _dbService;

        public MatcherSessionService(IDatabaseService dbService)
        {
            _dbService = dbService;
        }

        public async Task SaveGameResultAsync(SessionResult result)
        {
            var dataDict = new Dictionary<string, object>
            {
                { "playerName", result.PlayerName },
                { "difficulty", result.PairsCount },
                { "score", result.Score },
                { "moves", result.Moves },
                { "timestamp", result.TimestampUnix }
            };

            await _dbService.AddDocumentAsync(LEADERBOARD_COLLECTION, dataDict);
        }
        
        public async Task<List<SessionResult>> GetLeaderboardAsync(int pairsCount, int limit = 10)
        {
            var rawData = await _dbService.GetTopDocumentsAsync(LEADERBOARD_COLLECTION, "difficulty", pairsCount, "score", limit);
    
            var results = new List<SessionResult>();
            foreach(var dict in rawData)
            {
                string name = dict.ContainsKey("playerName") ? dict["playerName"].ToString() : "Unknown";
                int score = dict.ContainsKey("score") ? System.Convert.ToInt32(dict["score"]) : 0;
                int moves = dict.ContainsKey("moves") ? System.Convert.ToInt32(dict["moves"]) : 0;
                int pairs = dict.ContainsKey("difficulty") ? System.Convert.ToInt32(dict["difficulty"]) : pairsCount;
                long timestamp = dict.ContainsKey("timestamp") ? System.Convert.ToInt64(dict["timestamp"]) : 0;

                results.Add(new SessionResult(name, score, moves, pairs, timestamp));
            }
    
            return results;
        }
    }
}