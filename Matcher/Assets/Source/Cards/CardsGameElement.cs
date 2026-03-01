using System;
using Matcher.Core.Game.Element;
using Matcher.Game.Data;
using TMPro;
using UnityEngine;

namespace Matcher.Cards
{
    public class CardsGameElement : BaseGameElement
    {
        [SerializeField] protected TMP_Text _metaInfoTextFirst;
        [SerializeField] protected TMP_Text _metaInfoTextSecond;

        public override void Initialize(int id, MatchModel model, Action<IGameElement> onClickCallback)
        {
            base.Initialize(id, model, onClickCallback);
            _symbolText.text = model.Symbol;
            _metaInfoTextFirst.text = model.MetaInfo;
            _metaInfoTextSecond.text = model.MetaInfo;
        }
    }
}