using Matcher.Core.UI;

namespace Matcher.Game.Gameplay.UI.Win
{
    public class WinGameWindowModel : IWindowModel
    {
        public int Moves { get; set; }
        public int CompletionTime { get; set; }
    }
}