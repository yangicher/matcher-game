using System;
using System.Collections.Generic;
using Matcher.Core.Project;
using Matcher.Core.UI;
using Matcher.Game.Data;

namespace Matcher.Game.Lobby.UI
{
    public class LeaderboardWindowController : BaseWindowController<LeaderboardWindowModel, LeaderboardWindowView>
    {
        private int _difficulty = 4;

        private List<SessionResult> _currentData = new List<SessionResult>();
        public LeaderboardWindowController(LeaderboardWindowModel model) : base(model)
        {
        }
        
        protected override void SubscribeToEvents()
        {
            WindowView.OnCloseClicked += Close;
            WindowView.OnTabClicked += LoadLeaderboardForTab;
            WindowView.OnItemRequested += BindItemData;
        }

        protected override void UnsubscribeFromEvents()
        {
            WindowView.OnCloseClicked -= Close;
            WindowView.OnTabClicked -= LoadLeaderboardForTab;
            WindowView.OnItemRequested -= BindItemData;
        }

        protected override void OnOpened()
        {
            LoadLeaderboardForTab(_difficulty);
        }
        
        private void BindItemData(LeaderboardItemView itemView, int index)
        {
            if (index < 0 || index >= _currentData.Count)
            {
                return;
            }

            var result = _currentData[index];
            string formattedDate = DateTimeOffset.FromUnixTimeSeconds(result.TimestampUnix)
                .LocalDateTime.ToString("dd.MM.yy HH:mm");

            itemView.Setup(
                index + 1, 
                result.PlayerName, 
                result.Score, 
                result.Moves, 
                formattedDate
            );
        }

        private async void LoadLeaderboardForTab(int pairsCount)
        {
            _difficulty = pairsCount;
            
            WindowView.ShowLoading(true);
            WindowView.SetDataCount(0);

            _currentData = await ProjectContext.SessionService.GetLeaderboardAsync(_difficulty, 10);

            WindowView.ShowLoading(false);
            WindowView.SetDataCount(_currentData.Count);
        }
    }
}