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
        
        [SerializeField] private Toggle _toggle;
        [SerializeField] private Toggle _toggle8Pairs;
        
        [SerializeField] private Transform _itemsContainer;
        [SerializeField] private LeaderboardItemView _itemPrefab;
        [SerializeField] private GameObject _loadingSpinner;

        [SerializeField] private Button _closeButton;

        public event Action<int> OnTabClicked;
        public event Action OnCloseClicked;

        private List<LeaderboardItemView> _activeItems = new List<LeaderboardItemView>();

        private void Awake()
        {
            _closeButton.onClick.AddListener(() => OnCloseClicked?.Invoke());
        }

        public void ShowLoading(bool isLoading)
        {
            _loadingSpinner.SetActive(isLoading);
        }

        public void PopulateList(List<SessionResult> results)
        {
            foreach (var item in _activeItems) Destroy(item.gameObject);
            _activeItems.Clear();

            for (int i = 0; i < results.Count; i++)
            {
                var result = results[i];
                var itemView = Instantiate(_itemPrefab, _itemsContainer);
                
                itemView.Setup(i + 1, result.PlayerName, result.Score, result.Moves, TimeSpan.FromTicks(result.TimestampUnix).ToString());
                _activeItems.Add(itemView);
            }
        }

        public override Task PlayShowAnimationAsync() { gameObject.SetActive(true); return Task.CompletedTask; }
        public override Task PlayHideAnimationAsync() { gameObject.SetActive(false); return Task.CompletedTask; }
    }
}