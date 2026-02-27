using Matcher.Core.UI;

namespace Matcher.Game.Gameplay.UI.Win
{
    public class WinGameWindowController : BaseWindowController<WinGameWindowModel, WinGameWindowView>
    {
        public WinGameWindowController(WinGameWindowModel model) : base(model)
        {
        }
        
        protected override void SetupView()
        {
            WindowView.SetScore(Model.Score);
            WindowView.SetMoves(Model.Moves);
            WindowView.SetTime(Model.CompletionTime);
        }

        protected override void SubscribeToEvents()
        {
            WindowView.OnReplayClicked += OnReplayClicked;
            WindowView.OnRestartClicked += OnRestartClicked;
            WindowView.OnCloseClicked += OnCloseClicked;
        }

        protected override void UnsubscribeFromEvents()
        {
            WindowView.OnReplayClicked -= OnReplayClicked;
            WindowView.OnRestartClicked -= OnRestartClicked;
            WindowView.OnCloseClicked -= OnCloseClicked;
        }

        private void OnReplayClicked()
        {
            Close();
            Model.OnRestartRequest?.Invoke(true);
        }
        
        private void OnRestartClicked()
        {
            Close();
            Model.OnRestartRequest?.Invoke(false);
        }
        
        private void OnCloseClicked()
        {
            Close();
            Model.OnQuitGameRequest?.Invoke();
        }
    }
}