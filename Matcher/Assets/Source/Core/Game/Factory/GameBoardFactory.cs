using Matcher.Core.Factory;
using Matcher.Core.Game.Element;
using UnityEngine;

namespace Matcher.Core.Game.Factory
{
    public class GameBoardFactory : BaseFactory
    {
        private const string GameElementItemPath = "Prefabs/GameElement";
        
        public BaseGameElement CreateGameElement(Transform parent)
        {
            var gameElement = CreateObject<BaseGameElement>(GameElementItemPath, parent);
            return gameElement;
        }
    }
}