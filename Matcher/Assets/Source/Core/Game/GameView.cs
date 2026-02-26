using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Matcher.Core.Game
{
    //TODO: REMOVE MONO, use update from scene logic
    public class GameView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timeText;
        [SerializeField] private TMP_Text _movesText;

        [SerializeField] private GameObject _winPanel;
        [SerializeField] private GameObject _losePanel;
        [SerializeField] private TMP_Text _winScoreText;
        [SerializeField] private TMP_Text _winStatsText;
        [SerializeField] private TMP_Text _loseStatsText;

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
            _winPanel.SetActive(false);
            _losePanel.SetActive(false);
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

        public void ShowWinScreen(int score, int moves, float timeRemaining)
        {
            _winPanel.SetActive(true);
            _winScoreText.text = $"Score: {score}";
            _winStatsText.text = $"Moves: {moves}\nTime Left: {Mathf.RoundToInt(timeRemaining)}s";
        }

        public void ShowLoseScreen(int moves)
        {
            _losePanel.SetActive(true);
            _loseStatsText.text = $"You made {moves} moves";
        }
    }
}