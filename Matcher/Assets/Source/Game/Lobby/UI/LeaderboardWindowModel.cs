using Matcher.Core.UI;
using Matcher.Game.Settings;

namespace Matcher.Game.Lobby.UI
{
    public class LeaderboardWindowModel : IWindowModel
    {
        public GameSettings Settings { get; }
        public LeaderboardWindowModel(GameSettings settings)
        {
            Settings = settings;
        }
    }
}