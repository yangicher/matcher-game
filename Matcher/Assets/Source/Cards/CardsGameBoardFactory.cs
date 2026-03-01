using Matcher.Core.Game.Element;
using Matcher.Core.Game.Factory;
using UnityEngine;

namespace Matcher.Cards
{
    public class CardsGameBoardFactory : GameBoardFactory
    {
        private const string CardsGameElementItemPath = "Prefabs/Cards/GameElement";

        public override IGameElement CreateGameElement(Transform parent)
        {
            var gameElement = CreateObject<CardsGameElement>(CardsGameElementItemPath, parent);
            return gameElement;
        }
    }
}