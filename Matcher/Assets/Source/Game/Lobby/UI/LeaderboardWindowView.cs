using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Matcher.Core.UI;
using Matcher.Game.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Matcher.Game.Lobby.UI
{
    public class LeaderboardWindowView : BaseWindow<LeaderboardWindowModel>
    {
        [SerializeField] private ToggleGroup _toggleGroup;
        
        [SerializeField] private Toggle _toggleEasy;
        [SerializeField] private Toggle _toggleHard;
        
        [SerializeField] private Transform _itemsContainer;
        [SerializeField] private LeaderboardItemView _itemPrefab;
        [SerializeField] private GameObject _loadingGameObject;

        [SerializeField] private Button _closeButton;

        public event Action<int> OnTabClicked;
        public event Action OnCloseClicked;

        private List<LeaderboardItemView> _activeItems = new List<LeaderboardItemView>();

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
                    OnTabClicked?.Invoke(4);
                }
            });
            _toggleHard.onValueChanged.AddListener((value) =>
            {
                if (value)
                {
                    OnTabClicked?.Invoke(8);
                }
            });
            _closeButton.onClick.AddListener(() => OnCloseClicked?.Invoke());
        }

        public void ShowLoading(bool isLoading)
        {
            _loadingGameObject.SetActive(isLoading);
        }

        public void PopulateList(List<SessionResult> results)
        {
            foreach (var item in _activeItems) Destroy(item.gameObject);
            _activeItems.Clear();

            for (int i = 0; i < results.Count; i++)
            {
                var result = results[i];
                var itemView = Instantiate(_itemPrefab, _itemsContainer);
                
                itemView.Setup(i + 1, result.PlayerName, result.Score, result.Moves, DateTimeOffset.FromUnixTimeSeconds(result.TimestampUnix).LocalDateTime.ToString("dd.MM.yy HH:mm"));
                _activeItems.Add(itemView);
            }
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