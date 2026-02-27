using System;
using Matcher.Core.Game.Board;
using Matcher.Core.Game.Factory;
using Matcher.Core.Game.UI;
using Matcher.Core.UI;
using Matcher.Game.Gameplay.UI.GameOver;
using Matcher.Game.Gameplay.UI.Win;
using Matcher.Game.Settings;
using UnityEngine;

namespace Matcher.Core.Game
{
    public class GameSession : IDisposable
    {
        private readonly GameView _gameView;
        private readonly GameBoardView _gameBoardView;
        private readonly DifficultyConfig _config;
        private readonly WindowManager _windowManager;
        
        private MatchEngine _matchEngine;
        private CountdownTimer _timer;
        private BoardBuilder _boardBuilder;

        private int _currentMoves;
        
        public Action OnSessionEnd;

        public GameSession(
            GameView gameView, 
            GameBoardView gameBoardView, 
            DifficultyConfig config,
            WindowManager windowManager)
        {
            _gameView = gameView;
            _gameBoardView = gameBoardView;
            _config = config;
            _windowManager = windowManager;
        }

        public void Start()
        {
            _gameView.Initialize();
            CreateMatchEngine();
            CreateGameBoard();
            CreateTimer();
        }

        private void CreateGameBoard()
        {
            _boardBuilder = new BoardBuilder(new GameBoardFactory(), _gameBoardView.BoardRect);
            _boardBuilder.BuildBoard(_config.PairsCount, _matchEngine.ProcessElementClick);
            
            int totalCards = _config.PairsCount * 2;
            _gameBoardView.AdjustGridLayout(totalCards, _config.GridColumns);
        }

        private void CreateMatchEngine()
        {
            _matchEngine = new MatchEngine(_config.PairsCount);
            _matchEngine.OnMovesUpdated += HandleMovesUpdated;
            _matchEngine.OnAllPairsMatched += HandleWin;
        }

        private void CreateTimer()
        {
            _timer = new CountdownTimer();
            _timer.Start(_config.TimeLimit);
            
            _timer.OnTimeUpdated += _gameView.UpdateTime;
            _timer.OnTimeUp += HandleLose;
        }

        public void Tick(float deltaTime)
        {
            _timer.Tick(deltaTime);
        }

        public void Restart(bool isReplay = false)
        {
            _currentMoves = 0;
            _gameView.UpdateMoves(_currentMoves);
            
            _matchEngine.Restart();
            if (!isReplay)
            {
                _boardBuilder.BuildBoard(_config.PairsCount, _matchEngine.ProcessElementClick);
            }
            else
            {
                _boardBuilder.ResetBoard();
            }
            
            _timer.Start(_config.TimeLimit);
        }

        private void HandleMovesUpdated(int moves)
        {
            _currentMoves = moves;
            _gameView.UpdateMoves(moves);
            _gameView.UpdateScore(CalculateScore());
        }

        private void HandleWin()
        {
            _timer.Stop();
            
            _windowManager.Open(new WinGameWindowController(new WinGameWindowModel()
            {
                Score = CalculateScore(), 
                Moves = _currentMoves, 
                CompletionTime = Mathf.RoundToInt(_timer.TimeRemaining),
                OnRestartRequest = Restart,
                OnQuitGameRequest = OnSessionEnd
            }));
        }

        private void HandleLose()
        {
            _windowManager.Open(new GameOverWindowController(new GameOverWindowModel()
            {
                Moves = _currentMoves,
                OnRestartRequest = Restart,
                OnQuitGameRequest = OnSessionEnd
            }));
        }

        private int CalculateScore()
        {
            return Math.Max(0, (_config.PairsCount * 100) + (int)(_timer.TimeRemaining * 10) - (_currentMoves * 5));
        }

        public void Dispose()
        {
            _matchEngine.OnMovesUpdated -= HandleMovesUpdated;
            _matchEngine.OnAllPairsMatched -= HandleWin;
            _timer.OnTimeUpdated -= _gameView.UpdateTime;
            _timer.OnTimeUp -= HandleLose;
        }
    }
}