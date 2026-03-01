using System;
using Matcher.Core.Game.Board;
using Matcher.Core.UI;
using Matcher.Game.Data;
using Matcher.Game.Settings;

namespace Matcher.Core.Game
{
    public interface IGameSession : IDisposable
    {
        Action OnQuitGame { get; set; }
        Action<SessionResult> OnSessionEnd { get; set; }
        
        void Start();

        void Init(
            string playerName,
            GameView gameView,
            GameBoardView gameBoardView,
            DifficultyConfig config,
            ThemeConfig themeConfig,
            WindowManager windowManager);

        void Tick(float deltaTime);
    }
}