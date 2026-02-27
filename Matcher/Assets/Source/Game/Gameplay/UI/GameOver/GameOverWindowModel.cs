using System;
using Matcher.Core.UI;

namespace Matcher.Game.Gameplay.UI.GameOver
{
    public class GameOverWindowModel : IWindowModel
    {
        public int Moves { get; set; }
        
        public Action OnQuitGameRequest { get; set; }
        
        public Action<bool> OnRestartRequest { get; set; }
    }
}