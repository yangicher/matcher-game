using UnityEngine;
using System.Linq;
using Matcher.Game.Data;

namespace Matcher.Game.Settings
{
    [CreateAssetMenu(fileName = "NewThemeConfig", menuName = "Matcher/Theme Config")]
    public class ThemeConfig : ScriptableObject
    {
        [Header("Theme Info")]
        public string ThemeName; 
        
        [Header("Content")]
        public MatchModel[] AvailableModels; 

        public MatchModel[] GetModelsForGame(int pairsCount)
        {
            if (pairsCount > AvailableModels.Length)
            {
                return AvailableModels; 
            }

            return AvailableModels.Take(pairsCount).ToArray();
        }
    }
}