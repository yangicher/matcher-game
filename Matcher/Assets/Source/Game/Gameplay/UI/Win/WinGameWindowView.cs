using System;
using System.Threading.Tasks;
using Matcher.Core.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Matcher.Game.Gameplay.UI.Win
{
    public class WinGameWindowView : BaseWindow<WinGameWindowModel>
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _movesText;
        [SerializeField] private TMP_Text _timeText;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _replayButton;
        [SerializeField] private Button _restartButton;

        public event Action OnReplayClicked;
        public event Action OnRestartClicked;
        public event Action OnCloseClicked;

        private void Awake()
        {
            _replayButton.onClick.AddListener(() => OnReplayClicked?.Invoke());
            _restartButton.onClick.AddListener(() => OnRestartClicked?.Invoke());
            _closeButton.onClick.AddListener(() => OnCloseClicked?.Invoke());
        }

        public void SetScore(int score)
        {
            _scoreText.text = $"Score: {score}";
        }
        
        public void SetMoves(int moves)
        {
            _movesText.text = $"Moves: {moves}";
        }
        
        public void SetTime(int time)
        {
            _timeText.text = $"Competion time: {time}";
        }

        public override Task PlayShowAnimationAsync()
        {
            gameObject.SetActive(true);
            return Task.CompletedTask;
        }

        public override Task PlayHideAnimationAsync()
        {
            gameObject.SetActive(false);
            return Task.CompletedTask;
        }
    }
}