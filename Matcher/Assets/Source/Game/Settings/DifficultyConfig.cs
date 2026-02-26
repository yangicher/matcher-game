using System;

namespace Matcher.Game.Settings
{
    [Serializable]
    public class DifficultyConfig
    {
        public int PairsCount;
        public int GridColumns;
        public float TimeLimit;
    }
}