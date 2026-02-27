using Matcher.Core.Project;
using Matcher.Core.UI;
using Matcher.Game.Data;

namespace Matcher.Game.Lobby.UI
{
    public class LeaderboardWindowController : BaseWindowController<LeaderboardWindowModel, LeaderboardWindowView>
    {
        private int _currentTabPairsCount = 8;

        public LeaderboardWindowController(LeaderboardWindowModel model) : base(model)
        {
        }
        
        protected override void SubscribeToEvents()
        {
            WindowView.OnCloseClicked += Close;
            WindowView.OnTabClicked += LoadLeaderboardForTab;
        }

        protected override void UnsubscribeFromEvents()
        {
            WindowView.OnCloseClicked -= Close;
            WindowView.OnTabClicked -= LoadLeaderboardForTab;
        }

        protected override void OnOpened()
        {
            LoadLeaderboardForTab(_currentTabPairsCount);
        }

        private async void LoadLeaderboardForTab(int pairsCount)
        {
            _currentTabPairsCount = pairsCount;
            
            WindowView.ShowLoading(true);
            WindowView.PopulateList(new System.Collections.Generic.List<SessionResult>());

            var results = await ProjectContext.SessionService.GetLeaderboardAsync(_currentTabPairsCount, 10);

            WindowView.ShowLoading(false);
            WindowView.PopulateList(results);
        }
    }
}