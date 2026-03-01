using System;
using Matcher.Game.Data;
using UnityEngine;

namespace Matcher.Core.Game.Element
{
    public interface IGameElement
    {
        int Id { get; }
        RectTransform RectTransform { get; }

        GameElementState CurrentState { get; }

        void SetState(GameElementState newState);

        void Initialize(int id, MatchModel model, Action<IGameElement> onClickCallback);
    }
}