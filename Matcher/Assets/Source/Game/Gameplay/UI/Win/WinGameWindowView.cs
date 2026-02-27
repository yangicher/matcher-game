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
        [SerializeField] private TMP_Text _movesText;
        [SerializeField] private TMP_Text _timeText;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _restartButton;

        public event Action OnCloseClicked;

        private void Awake()
        {
            _closeButton.onClick.AddListener(() => OnCloseClicked?.Invoke());
        }

        public void SetMoves(int score)
        {
            _movesText.text = $"Moves: {score}";
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