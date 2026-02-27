using Matcher.Core.Installer;
using Matcher.Core.UI;
using Matcher.Game.Lobby.UI;

namespace Matcher.Game.Installers
{
    public class LobbyUIInstaller : IInstaller
    {
        private readonly WindowManager _windowManager;

        public LobbyUIInstaller(WindowManager windowManager)
        {
            _windowManager = windowManager;
        }

        public void Install()
        {
            _windowManager.RegisterWindow<LeaderboardWindowController>("Prefabs/UI/Windows/LeaderboardWindow");
        }

        public void Dispose()
        {
            _windowManager.UnregisterWindow<LeaderboardWindowController>();
        }
    }
}