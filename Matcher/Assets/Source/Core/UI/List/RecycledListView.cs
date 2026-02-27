using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Pool;

namespace Matcher.Core.UI.List
{
    [RequireComponent(typeof(ScrollRect))]
    public abstract class RecycledListView<T> : MonoBehaviour where T : Component 
    {
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private RectTransform _content;
        
        [SerializeField] private T _itemPrefab; 
        
        [SerializeField] private float _itemHeight = 110f;
        [SerializeField] private int _visibleItemsCount = 12;

        public event Action<T, int> OnBindItem;

        private ObjectPool<T> _pool;
        private readonly Dictionary<int, T> _activeItems = new Dictionary<int, T>();
        
        private int _totalItemsCount = 0;
        private int _lastStartIndex = -1;

        protected virtual void Awake()
        {
            _pool = new ObjectPool<T>(
                createFunc: () => Instantiate(_itemPrefab, _content),
                actionOnGet: (item) => item.gameObject.SetActive(true),
                actionOnRelease: (item) => item.gameObject.SetActive(false),
                actionOnDestroy: (item) => Destroy(item.gameObject)
            );

            _scrollRect.onValueChanged.AddListener(_ => HandleScroll());
        }

        public void SetDataCount(int count, bool resetScroll = true)
        {
            _totalItemsCount = count;
            _content.sizeDelta = new Vector2(_content.sizeDelta.x, count * _itemHeight);

            if (resetScroll)
            {
                _content.anchoredPosition = Vector2.zero;
            }

            _lastStartIndex = -1;
            ClearAll();
            HandleScroll();
        }

        private void HandleScroll()
        {
            if (_totalItemsCount == 0)
            {
                return;
            }

            float currentY = Mathf.Max(0, _content.anchoredPosition.y);
            int startIndex = Mathf.Max(0, Mathf.FloorToInt(currentY / _itemHeight));

            if (startIndex == _lastStartIndex)
            {
                return;
            }
            _lastStartIndex = startIndex;

            int endIndex = Mathf.Min(startIndex + _visibleItemsCount, _totalItemsCount - 1);

            var indicesToRemove = new List<int>();
            foreach (var kvp in _activeItems)
            {
                if (kvp.Key < startIndex || kvp.Key > endIndex)
                {
                    _pool.Release(kvp.Value);
                    indicesToRemove.Add(kvp.Key);
                }
            }

            foreach (var i in indicesToRemove)
            {
                _activeItems.Remove(i);
            }

            for (int i = startIndex; i <= endIndex; i++)
            {
                if (!_activeItems.ContainsKey(i))
                {
                    T item = _pool.Get();
                    var rt = item.GetComponent<RectTransform>();
                    rt.anchoredPosition = new Vector2(0, -i * _itemHeight);
                    _activeItems[i] = item;
                    
                    OnBindItem?.Invoke(item, i);
                }
            }
        }

        public void ClearAll()
        {
            foreach (var item in _activeItems.Values)
            {
                _pool.Release(item);
            }
            _activeItems.Clear();
        }
    }
}