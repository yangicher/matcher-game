using System.Collections.Generic;
using System.Threading.Tasks;
using Matcher.Game.Data;

namespace Matcher.Game.Services.Session
{
    public interface ISessionService
    {
        Task SaveGameResultAsync(SessionResult result);
        Task<List<SessionResult>> GetLeaderboardAsync(int pairsCount, int limit = 10);
    }
}