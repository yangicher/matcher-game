using System;
using System.Collections.Generic;
using Matcher.Core.Game.Element;
using Matcher.Core.Game.Factory;
using UnityEngine;
using Random = System.Random;

namespace Matcher.Core.Game.Board
{
    public class BoardBuilder
    {
        private readonly Transform _boardContainer;
        private readonly GameBoardFactory _factory;
        
        private readonly string[] _availableSymbols = { 
            "A", "B", "C", "D", "E", "F", "G", "H", 
            "I", "J", "K", "L", "M", "N", "O", "P" 
        };

        public BoardBuilder(GameBoardFactory factory, Transform boardContainer)
        {
            _factory = factory;
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

            foreach (int id in ids)
            {
                var element = _factory.CreateGameElement(_boardContainer);
                string symbol = _availableSymbols[id % _availableSymbols.Length];
                element.Initialize(id, symbol, onElementClicked);
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