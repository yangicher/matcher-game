using Matcher.Core.Game;
using Matcher.Scenes.Game;

namespace Matcher.Cards.Scenes
{
    public class CardsGameScene : GameScene
    {
        protected override IGameSession CreateSession()
        {
            return new CardsGameSession();
        }
    }
}