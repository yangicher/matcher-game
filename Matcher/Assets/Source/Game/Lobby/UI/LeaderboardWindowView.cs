using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Matcher.Core.UI;
using Matcher.Game.Data;
using Matcher.Game.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace Matcher.Game.Lobby.UI
{
    public class LeaderboardWindowView : BaseWindow<LeaderboardWindowModel>
    {
        [SerializeField] private ToggleGroup _toggleGroup;
        
        [SerializeField] private Toggle _toggleEasy;
        [SerializeField] private Toggle _toggleHard;
        
        [SerializeField] private LeaderboardListView _recycledListView;
        [SerializeField] private Transform _itemsContainer;
        [SerializeField] private LeaderboardItemView _itemPrefab;
        [SerializeField] private GameObject _loadingGameObject;

        [SerializeField] private Button _closeButton;

        public event Action<DifficultyLevel> OnTabClicked;
        public event Action OnCloseClicked;
        public event Action<LeaderboardItemView, int> OnItemRequested;
        
        private void Awake()
        {
            _toggleEasy.isOn = true;
            _toggleHard.isOn = false;
            _toggleEasy.group = _toggleGroup;
            _toggleHard.group = _toggleGroup;
            
            _toggleEasy.onValueChanged.AddListener((value) =>
            {
                if (value)
                {
                    OnTabClicked?.Invoke(DifficultyLevel.Easy);
                }
            });
            _toggleHard.onValueChanged.AddListener((value) =>
            {
                if (value)
                {
                    OnTabClicked?.Invoke(DifficultyLevel.Hard);
                }
            });
            _closeButton.onClick.AddListener(() => OnCloseClicked?.Invoke());
            _recycledListView.OnBindItem += (itemView, index) => OnItemRequested?.Invoke(itemView, index);
        }

        public void ShowLoading(bool isLoading)
        {
            _loadingGameObject.SetActive(isLoading);
        }

        public void SetDataCount(int count)
        {
            _recycledListView.SetDataCount(count);
        }

        public override Task PlayShowAnimationAsync()
        {
            gameObject.SetActive(true); 
            return Task.CompletedTask;
        }

        public override Task PlayHideAnimationAsync()
        {
            gameObject.SetActive(false); 
            return Task.CompletedTask;
        }
    }
}