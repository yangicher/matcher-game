using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Matcher.Core.Game
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timeText;
        [SerializeField] private TMP_Text _movesText;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _userNameText;
        
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _homeButton;

        public Action OnRestartClicked;
        public Action OnHomeClicked;

        private void Awake()
        {
            _restartButton.onClick.AddListener(() => OnRestartClicked?.Invoke());
            _homeButton.onClick.AddListener(() => OnHomeClicked?.Invoke());
        }

        public void Initialize()
        {
            UpdateMoves(0);
        }

        public void UpdateTime(float timeRemaining)
        {
            TimeSpan time = TimeSpan.FromSeconds(timeRemaining);
            _timeText.text = $"Time: {time.ToString(@"mm\:ss")}";
        }

        public void UpdateMoves(int moves)
        {
            _movesText.text = $"Moves: {moves}";
        }
        
        public void UpdateScore(int score)
        {
            _scoreText.text = $"Score: {score}";
        }
        
        public void SetUserName(string userName)
        {
            _movesText.text = userName;
        }
    }
}