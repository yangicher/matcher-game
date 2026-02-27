using System.Threading.Tasks;
using Matcher.Core.Game;
using Matcher.Core.Game.Board;
using Matcher.Core.Installer;
using Matcher.Core.Project;
using Matcher.Core.Scenes;
using Matcher.Game.Data;
using Matcher.Game.Installers;
using UnityEngine;

namespace Matcher.Scenes.Game
{
    public class GameScene : BaseScene
    {
        [SerializeField] private GameBoardView _gameBoardView;
        [SerializeField] private GameView _gameView;
        
        private GameSession _session;
        private GamePayload _currentPayload;
        private IInstaller _uiInstaller;
        
        public override Task LoadAsync(object payload = null)
        {
            _currentPayload = (GamePayload) payload;
            
            _uiInstaller = new GameUIInstaller(ProjectContext.WindowManager);
            _uiInstaller.Install();
            
            _session = new GameSession(_currentPayload.PlayerName, _gameView, _gameBoardView, _currentPayload.Config, ProjectContext.WindowManager);
            _session.OnQuitGame += OnQuitGame;
            _session.OnSessionEnd += OnSessionEnd;

            _session.Start();

            return Task.CompletedTask;
        }

        private void OnSessionEnd(SessionResult result)
        {
            ProjectContext.SessionService.SaveGameResultAsync(result);
        }

        private void OnQuitGame()
        {
            GoToLobby();
        }

        private void Update()
        {
            _session?.Tick(Time.deltaTime);
        }

        private void GoToLobby()
        {
            ProjectContext.TransitionController.LoadSceneAsync(SceneNames.Main);
        }

        public override void Dispose()
        {
            base.Dispose();
            _session.OnQuitGame -= OnQuitGame;
            _session.OnSessionEnd -= OnSessionEnd;
            _session?.Dispose();
            _uiInstaller?.Dispose();
            
            if (_gameView != null)
            {
                _gameView.OnRestartClicked = null;
                _gameView.OnHomeClicked = null;
            }
        }
    }
}