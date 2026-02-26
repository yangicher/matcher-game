using System;
using Matcher.Core.Game.Board;
using Matcher.Core.Game.UI;
using Matcher.Game.Settings;

namespace Matcher.Core.Game
{
    public class GameSession : IDisposable
    {
        private readonly MatchEngine _matchEngine;
        private readonly CountdownTimer _timer;
        private readonly BoardBuilder _boardBuilder;
        private readonly GameView _view;
        private readonly GameBoardView _gameBoardView;
        private readonly DifficultyConfig _config;

        private int _currentMoves;

        public GameSession(
            MatchEngine matchEngine, 
            CountdownTimer timer, 
            BoardBuilder boardBuilder, 
            GameView view, 
            GameBoardView gameBoardView, 
            DifficultyConfig config)
        {
            _matchEngine = matchEngine;
            _timer = timer;
            _boardBuilder = boardBuilder;
            _view = view;
            _gameBoardView = gameBoardView;
            _config = config;

            _matchEngine.OnMovesUpdated += HandleMovesUpdated;
            _matchEngine.OnAllPairsMatched += HandleWin;
            _timer.OnTimeUpdated += _view.UpdateTime;
            _timer.OnTimeUp += HandleLose;
        }

        public void Start()
        {
            _view.Initialize();
            _boardBuilder.BuildBoard(_config.PairsCount, _matchEngine.ProcessElementClick);
            _timer.Start(_config.TimeLimit);
            
            int totalCards = _config.PairsCount * 2;
            _gameBoardView.AdjustGridLayout(totalCards, _config.GridColumns);
        }

        public void Tick(float deltaTime)
        {
            _timer.Tick(deltaTime);
        }

        private void HandleMovesUpdated(int moves)
        {
            _currentMoves = moves;
            _view.UpdateMoves(moves);
        }

        private void HandleWin()
        {
            _timer.Stop();
            
            int score = CalculateScore();
            _view.ShowWinScreen(score, _currentMoves, _timer.TimeRemaining);
        }

        private void HandleLose()
        {
            _view.ShowLoseScreen(_currentMoves);
        }

        private int CalculateScore()
        {
            return Math.Max(0, (_config.PairsCount * 100) + (int)(_timer.TimeRemaining * 10) - (_currentMoves * 5));
        }

        public void Dispose()
        {
            _matchEngine.OnMovesUpdated -= HandleMovesUpdated;
            _matchEngine.OnAllPairsMatched -= HandleWin;
            _timer.OnTimeUpdated -= _view.UpdateTime;
            _timer.OnTimeUp -= HandleLose;
        }
    }
}