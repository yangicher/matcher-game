using Matcher.Core.Installer;
using Matcher.Core.UI;
using Matcher.Game.Gameplay.UI;
using Matcher.Game.Gameplay.UI.GameOver;
using Matcher.Game.Gameplay.UI.Win;

namespace Matcher.Game.Installers
{
    public class GameUIInstaller : IInstaller
    {
        private readonly WindowManager _windowManager;

        public GameUIInstaller(WindowManager windowManager)
        {
            _windowManager = windowManager;
        }

        public void Install()
        {
            _windowManager.RegisterWindow<WinGameWindowController>("Prefabs/UI/Windows/WinWindow");
            _windowManager.RegisterWindow<GameOverWindowController>("Prefabs/UI/Windows/GameOverWindow");
        }

        public void Dispose()
        {
            _windowManager.UnregisterWindow<WinGameWindowController>();
            _windowManager.UnregisterWindow<GameOverWindowController>();
        }
    }
}