using System;
using System.Collections.Generic;
using Matcher.Core.Game.Element;
using Matcher.Core.Game.Factory;
using Matcher.Game.Settings;
using UnityEngine;
using Random = System.Random;

namespace Matcher.Core.Game.Board
{
    public class BoardBuilder
    {
        private readonly Transform _boardContainer;
        private readonly IGameBoardFactory _factory;
        private readonly List<IGameElement> _activeElements = new List<IGameElement>();
        
        private readonly ThemeConfig _themeConfig;
        
        public IGameElement ActiveElement => _activeElements[0];

        public BoardBuilder(IGameBoardFactory factory, ThemeConfig themeConfig, Transform boardContainer)
        {
            _factory = factory;
            _themeConfig = themeConfig;
            _boardContainer = boardContainer;
        }

        public void BuildBoard(int pairsCount, Action<IGameElement> onElementClicked)
        {
            List<int> ids = new List<int>();
            for (int i = 0; i < pairsCount; i++)
            {
                ids.Add(i);
                ids.Add(i);
            }

            Shuffle(ids);

            int totalElements = pairsCount * 2;
            for (int i = 0; i < totalElements; i++)
            {
                int id = ids[i];
                var matchModel = _themeConfig.AvailableModels[id % _themeConfig.AvailableModels.Length];

                if (i < _activeElements.Count)
                {
                    _activeElements[i].Initialize(id, matchModel, onElementClicked);
                }
                else
                {
                    var element = _factory.CreateGameElement(_boardContainer);
                    element.Initialize(id, matchModel, onElementClicked);
                    _activeElements.Add(element);
                }
            }
        }
        
        public void ResetBoard()
        {
            foreach (var element in _activeElements)
            {
                element.SetState(GameElementState.Hidden);
            }
        }

        private void Shuffle(List<int> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                int value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}