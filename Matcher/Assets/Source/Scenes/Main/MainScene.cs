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

            if (difficulty == DifficultyLevel.Easy)
            {
                ProjectContext.TransitionController.LoadSceneAsync(SceneNames.Game, new GamePayload(playerName, new DifficultyConfig { PairsCount = 4, GridColumns = 4, TimeLimit = 60f }));
            }
            else
            {
                ProjectContext.TransitionController.LoadSceneAsync(SceneNames.Game, new GamePayload(playerName, new DifficultyConfig { PairsCount = 8, GridColumns = 4, TimeLimit = 60f }));
            }
        }

        private void OpenLeaderboard()
        {
            ProjectContext.WindowManager.Open(new LeaderboardWindowController(new LeaderboardWindowModel()));
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