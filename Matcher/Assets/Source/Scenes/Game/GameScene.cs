using System.Threading.Tasks;
using Matcher.Core.Game;
using Matcher.Core.Game.Board;
using Matcher.Core.Game.Factory;
using Matcher.Core.Game.UI;
using Matcher.Core.Installer;
using Matcher.Core.Project;
using Matcher.Core.Scenes;
using Matcher.Game.Installers;
using UnityEngine;

namespace Matcher.Scenes.Game
{
    public class GameScene : BaseScene
    {
        [SerializeField] private Transform _elementsContainer;
        [SerializeField] private GameBoardView _gameBoardView;
        [SerializeField] private GameView _gameView;
        
        [SerializeField] private Transform _localWindowsCanvas;

        private GameSession _session;
        private GamePayload _currentPayload;
        private IInstaller _uiInstaller;
        
        public override Task LoadAsync(object payload = null)
        {
            _currentPayload = (GamePayload) payload;
            
            _uiInstaller = new GameUIInstaller(ProjectContext.WindowManager);
            _uiInstaller.Install();

            BoardBuilder boardBuilder = new BoardBuilder(new GameBoardFactory(), _elementsContainer);

            MatchEngine matchEngine = new MatchEngine(_currentPayload.Config.PairsCount);
            CountdownTimer timer = new CountdownTimer();

            _session = new GameSession(matchEngine, timer, boardBuilder, _gameView, _gameBoardView, _currentPayload.Config, ProjectContext.WindowManager);

            _gameView.OnRestartClicked += () => _ = ProjectContext.TransitionController.LoadSceneAsync(SceneNames.Game);
            _gameView.OnHomeClicked += () => _ = ProjectContext.TransitionController.LoadSceneAsync(SceneNames.Main);

            _session.Start();

            return Task.CompletedTask;
        }

        private void Update()
        {
            _session?.Tick(Time.deltaTime);
        }

        public override void Dispose()
        {
            base.Dispose();
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