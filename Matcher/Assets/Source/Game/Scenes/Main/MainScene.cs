using UnityEngine;
using System.Threading.Tasks;
using Matcher.Core.Game;
using Matcher.Core.Project;
using Matcher.Core.Scenes;
using Matcher.Game.Installers;
using Matcher.Game.Lobby.UI;
using Matcher.Game.Settings;
using Matcher.Scenes.Main.Lobby;

namespace Matcher.Scenes.Main
{
    public class MainScene : BaseScene
    {
        [SerializeField] private LobbyView _lobbyView;
        [SerializeField] private GameSettings _settings;

        private LobbyUIInstaller _lobbyUIInstaller;
        
        public override Task LoadAsync(object payload = null)
        {
            _lobbyUIInstaller = new LobbyUIInstaller(ProjectContext.WindowManager);
            _lobbyUIInstaller.Install();
            
            _lobbyView.OnPlayEasyClicked += StartEasyGame;
            _lobbyView.OnPlayHardClicked += StartHardGame;
            _lobbyView.OnLeaderboardClicked += OpenLeaderboard;

            _lobbyView.SetInteractable(true);

            return Task.CompletedTask;
        }

        private void StartEasyGame(string playerName)
        {
            StartGame(playerName, DifficultyLevel.Easy);
        }

        private void StartHardGame(string playerName)
        {
            StartGame(playerName, DifficultyLevel.Hard);
        }

        private void StartGame(string playerName, DifficultyLevel difficulty)
        {
            _lobbyView.SetInteractable(false);

            playerName = string.IsNullOrWhiteSpace(playerName) ? "Guest" : playerName;
            _settings.CurrentDifficulty = difficulty;
            ProjectContext.TransitionController.LoadSceneAsync(SceneNames.Game, new GamePayload(playerName, _settings.GetCurrentConfig()));
        }

        private void OpenLeaderboard()
        {
            ProjectContext.WindowManager.Open(new LeaderboardWindowController(new LeaderboardWindowModel(_settings)));
        }

        public override void Dispose()
        {
            base.Dispose();
            _lobbyUIInstaller?.Dispose();
            if (_lobbyView != null)
            {
                _lobbyView.Dispose();
                _lobbyView.OnPlayEasyClicked -= StartEasyGame;
                _lobbyView.OnPlayHardClicked -= StartHardGame;
                _lobbyView.OnLeaderboardClicked -= OpenLeaderboard;
            }
        }
    }
}