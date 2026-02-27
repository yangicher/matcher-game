using System;
using Matcher.Core.UI;

namespace Matcher.Game.Gameplay.UI.Win
{
    public class WinGameWindowModel : IWindowModel
    {
        public int Score { get; set; }
        public int Moves { get; set; }
        public int CompletionTime { get; set; }
        
        public Action OnQuitGameRequest { get; set; }
        
        public Action<bool> OnRestartRequest { get; set; }
    }
}