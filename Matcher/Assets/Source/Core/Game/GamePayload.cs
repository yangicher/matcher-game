using Matcher.Game.Settings;

namespace Matcher.Core.Game
{
    public class GamePayload
    {
        public string PlayerName { get; }
        public IGameSession GameSession { get; }
        public DifficultyConfig Config { get; }
        public ThemeConfig Theme { get; }

        public GamePayload(string playerName, IGameSession gameSession, DifficultyConfig config, ThemeConfig theme)
        {
            PlayerName = playerName;
            GameSession = gameSession;
            Config = config;
            Theme = theme;
        }
    }
}