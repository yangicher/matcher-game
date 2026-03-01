using Matcher.Core.Factory;
using Matcher.Core.Game.Element;
using UnityEngine;

namespace Matcher.Core.Game.Factory
{
    public class GameBoardFactory : BaseFactory, IGameBoardFactory
    {
        private const string GameElementItemPath = "Prefabs/GameElement";
        
        public virtual IGameElement CreateGameElement(Transform parent)
        {
            var gameElement = CreateObject<BaseGameElement>(GameElementItemPath, parent);
            return gameElement;
        }
    }
}