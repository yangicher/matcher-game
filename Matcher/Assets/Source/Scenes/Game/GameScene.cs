using System.Threading.Tasks;
using Matcher.Core.Game;
using Matcher.Core.Game.Board;
using Matcher.Core.Game.Factory;
using Matcher.Core.Game.UI;
using Matcher.Core.Project;
using Matcher.Core.Scenes;
using UnityEngine;

namespace Matcher.Scenes.Game
{
    public class GameScene : BaseScene
    {
        [SerializeField] private Transform _elementsContainer;
        [SerializeField] private GameBoardView _gameBoardView;
        [SerializeField] private GameView _gameView;

        private GameSession _session;
        private GamePayload _currentPayload;
        
        public override Task LoadAsync(object payload = null)
        {
            _currentPayload = (GamePayload) payload;

            BoardBuilder boardBuilder = new BoardBuilder(new GameBoardFactory(), _elementsContainer);

            MatchEngine matchEngine = new MatchEngine(_currentPayload.Config.PairsCount);
            CountdownTimer timer = new CountdownTimer();

            _session = new GameSession(matchEngine, timer, boardBuilder, _gameView, _gameBoardView, _currentPayload.Config);

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
            
            if (_gameView != null)
            {
                _gameView.OnRestartClicked = null;
                _gameView.OnHomeClicked = null;
            }
        }
    }
}