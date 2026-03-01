using System;
using Matcher.Core.Game.Board;
using Matcher.Core.Game.Factory;
using Matcher.Core.Game.UI;
using Matcher.Core.UI;
using Matcher.Game.Data;
using Matcher.Game.Gameplay.UI.GameOver;
using Matcher.Game.Gameplay.UI.Win;
using Matcher.Game.Settings;
using UnityEngine;

namespace Matcher.Core.Game
{
    public class GameSession : IGameSession
    {
        private string _playerName;
        private GameView _gameView;
        private GameBoardView _gameBoardView;
        private DifficultyConfig _config;
        private ThemeConfig _themeConfig;
        private WindowManager _windowManager;
        
        private MatchEngine _matchEngine;
        private CountdownTimer _timer;
        private BoardBuilder _boardBuilder;

        private int _currentMoves;
        
        public Action OnQuitGame { get; set; }
        public Action<SessionResult> OnSessionEnd { get; set; }

        public void Init(
            string playerName,
            GameView gameView, 
            GameBoardView gameBoardView, 
            DifficultyConfig config,
            ThemeConfig themeConfig,
            WindowManager windowManager)
        {
            _playerName = playerName;
            _gameView = gameView;
            _gameBoardView = gameBoardView;
            _config = config;
            _themeConfig = themeConfig;
            _windowManager = windowManager;
        }

        public void Start()
        {
            _gameView.Initialize();
            _gameView.SetUserName(_playerName);
            _gameView.OnRestartClicked += () => Restart();
            _gameView.OnHomeClicked += OnQuitGame;

            CreateMatchEngine();
            CreateGameBoard();
            CreateTimer();
        }

        public void CreateGameBoard()
        {
            _boardBuilder = new BoardBuilder(GetGameBoardFactory(), _themeConfig, _gameBoardView.BoardRect);
            _boardBuilder.BuildBoard(_config.PairsCount, _matchEngine.ProcessElementClick);
            
            _gameBoardView.AdjustGridLayout(_config.GridColumns, _boardBuilder.ActiveElement.RectTransform.rect.size);
        }

        public void CreateMatchEngine()
        {
            _matchEngine = new MatchEngine(_config.PairsCount);
            _matchEngine.OnMovesUpdated += HandleMovesUpdated;
            _matchEngine.OnAllPairsMatched += HandleWin;
        }

        protected virtual IGameBoardFactory GetGameBoardFactory()
        {
            return new GameBoardFactory();
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

        private void Restart(bool isReplay = false)
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
            var score = CalculateScore();
            OnSessionEnd?.Invoke(new SessionResult(_playerName, score, _currentMoves, _config.PairsCount, System.DateTimeOffset.UtcNow.ToUnixTimeSeconds()));
            
            _windowManager.Open(new WinGameWindowController(new WinGameWindowModel()
            {
                Score = score, 
                Moves = _currentMoves, 
                CompletionTime = Mathf.RoundToInt(_timer.TimeRemaining),
                OnRestartRequest = Restart,
                OnQuitGameRequest = OnQuitGame
            }));
        }

        private void HandleLose()
        {
            _windowManager.Open(new GameOverWindowController(new GameOverWindowModel()
            {
                Moves = _currentMoves,
                OnRestartRequest = Restart,
                OnQuitGameRequest = OnQuitGame
            }));
        }

        public virtual int CalculateScore()
        {
            return Math.Max(0, (_config.PairsCount * 100) + (int)(_timer.TimeRemaining * 10) - (_currentMoves * 5));
        }

        public void Dispose()
        {
            _gameView.OnRestartClicked = null;
            _gameView.OnHomeClicked -= OnQuitGame;
            _matchEngine.OnMovesUpdated -= HandleMovesUpdated;
            _matchEngine.OnAllPairsMatched -= HandleWin;
            _timer.OnTimeUpdated -= _gameView.UpdateTime;
            _timer.OnTimeUp -= HandleLose;
        }
    }
}