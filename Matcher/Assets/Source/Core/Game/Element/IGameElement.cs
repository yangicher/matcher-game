using System;

namespace Matcher.Core.Game.Element
{
    public interface IGameElement
    {
        int Id { get; }

        GameElementState CurrentState { get; }

        void SetState(GameElementState newState);

        void Initialize(int id, string symbol, Action<IGameElement> onClickCallback);
    }
}