using System;
using System.Threading.Tasks;
using Matcher.Core.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Matcher.Game.Gameplay.UI.GameOver
{
    public class GameOverWindowView : BaseWindow<GameOverWindowModel>
    {
        [SerializeField] private TMP_Text _reasonText;
        
        [SerializeField] private TMP_Text _movesText;
        [SerializeField] private Button _homeButton;
        [SerializeField] private Button _restartButton;

        public event Action OnHomeClicked;
        public event Action OnRestartClicked;

        private void Awake()
        {
            _homeButton.onClick.AddListener(() => OnHomeClicked?.Invoke());
        }

        public void SetMoves(int score)
        {
            _movesText.text = $"Moves: {score}";
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