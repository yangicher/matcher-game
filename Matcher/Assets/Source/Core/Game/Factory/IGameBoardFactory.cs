using Matcher.Core.Game.Element;
using UnityEngine;

namespace Matcher.Core.Game.Factory
{
    public interface IGameBoardFactory
    {
        IGameElement CreateGameElement(Transform parent);
    }
}