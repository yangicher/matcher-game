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
            WindowView.SetMoves(Model.Moves);
        }

        protected override void SubscribeToEvents()
        {
            WindowView.OnCloseClicked += Close;
        }

        protected override void UnsubscribeFromEvents()
        {
            WindowView.OnCloseClicked -= Close;
        }
    }
}