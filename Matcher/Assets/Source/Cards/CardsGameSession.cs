using Matcher.Core.Game;
using Matcher.Core.Game.Factory;

namespace Matcher.Cards
{
    public class CardsGameSession : GameSession
    {
        protected override IGameBoardFactory GetGameBoardFactory()
        {
            return new CardsGameBoardFactory();
        }
        
        public override int CalculateScore()
        {
            return base.CalculateScore();
        }
    }
}