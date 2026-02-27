using Matcher.Core.UI;

namespace Matcher.Game.Gameplay.UI.GameOver
{
    public class GameOverWindowController : BaseWindowController<GameOverWindowModel, GameOverWindowView>
    {
        public GameOverWindowController(GameOverWindowModel model) : base(model)
        {
        }
        
        protected override void SetupView()
        {
            WindowView.SetMoves(Model.Moves);
        }

        protected override void SubscribeToEvents()
        {
            WindowView.OnHomeClicked += OnHomeClicked;
            WindowView.OnReplayClicked += OnReplayClicked;
            WindowView.OnRestartClicked += OnRestartClicked;
        }

        protected override void UnsubscribeFromEvents()
        {
            WindowView.OnHomeClicked -= OnHomeClicked;
            WindowView.OnReplayClicked -= OnReplayClicked;
            WindowView.OnRestartClicked -= OnRestartClicked;
        }

        private void OnReplayClicked()
        {
            Close();
            Model.OnRestartRequest?.Invoke(true);
        }

        private void OnHomeClicked()
        {
            Close();
            Model.OnQuitGameRequest?.Invoke();
        }
        
        private void OnRestartClicked()
        {
            Close();
            Model.OnRestartRequest?.Invoke(false);
        }
    }
}