using UnityEngine;
using System;

namespace Matcher.Game.Settings
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Matcher/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [Header("Current Session Data")]
        public string PlayerName = "Player";
        public DifficultyLevel CurrentDifficulty;

        [Header("Difficulty Presets")]
        public DifficultyConfig EasyConfig = new DifficultyConfig { PairsCount = 4, GridColumns = 2, TimeLimit = 30f };
        public DifficultyConfig HardConfig = new DifficultyConfig { PairsCount = 8, GridColumns = 4, TimeLimit = 60f };

        public DifficultyConfig GetCurrentConfig() 
        {
            return CurrentDifficulty == DifficultyLevel.Easy ? EasyConfig : HardConfig;
        }
    }
}