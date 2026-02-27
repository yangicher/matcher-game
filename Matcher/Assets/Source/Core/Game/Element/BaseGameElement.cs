using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Matcher.Core.Game.Element
{
    public class BaseGameElement : MonoBehaviour, IGameElement
    {
        [SerializeField] protected Button _button;
        [SerializeField] protected GameObject _hiddenVisuals;
        [SerializeField] protected GameObject _revealedVisuals;
        
        [SerializeField] protected TMP_Text _symbolText;

        public int Id { get; private set; }
        public GameElementState CurrentState { get; private set; }

        private Action<BaseGameElement> _onElementClicked;

        protected virtual void Awake()
        {
            _button.onClick.AddListener(HandleClick);
        }

        public virtual void Initialize(int id, string symbol, Action<IGameElement> onClickCallback)
        {
            Id = id;
            _onElementClicked = onClickCallback;

            if (_symbolText != null)
            {
                _symbolText.text = symbol;
            }

            SetState(GameElementState.Hidden);
        }

        protected virtual void HandleClick()
        {
            if (CurrentState != GameElementState.Hidden)
            {
                return;
            }
            _onElementClicked?.Invoke(this);
        }

        public virtual void SetState(GameElementState newState)
        {
            CurrentState = newState;

            switch (newState)
            {
                case GameElementState.Hidden:
                    _hiddenVisuals.SetActive(true);
                    _revealedVisuals.SetActive(false);
                    break;
                case GameElementState.Revealed:
                case GameElementState.Matched:
                    _hiddenVisuals.SetActive(false);
                    _revealedVisuals.SetActive(true);
                    break;
            }
        }
    }
}