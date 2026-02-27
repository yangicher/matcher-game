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
            WindowView.OnHomeClicked += Close;
        }

        protected override void UnsubscribeFromEvents()
        {
            WindowView.OnHomeClicked -= Close;
        }
    }
}