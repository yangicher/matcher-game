using Matcher.Game.Settings;

namespace Matcher.Core.Game
{
    public class GamePayload
    {
        public string PlayerName { get; }
        public DifficultyConfig Config { get; }

        public GamePayload(string playerName, DifficultyConfig config)
        {
            PlayerName = playerName;
            Config = config;
        }
    }
}