#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using Matcher.Game.Settings;
using Matcher.Game.Data;

public class ThemeGenerator
{
    [MenuItem("Tools/Matcher/Generate Cards Theme")]
    public static void GenerateCardsTheme()
    {
        ThemeConfig cardsTheme = ScriptableObject.CreateInstance<ThemeConfig>();
        cardsTheme.ThemeName = "Playing Cards";

        cardsTheme.AvailableModels = new MatchModel[]
        {
            new MatchModel("A", "♠"),
            new MatchModel("K", "♥"),
            new MatchModel("Q", "♦"),
            new MatchModel("J", "♣"),
            new MatchModel("10", "♠"),
            new MatchModel("9", "♥"),
            new MatchModel("8", "♦"),
            new MatchModel("7", "♣"),
            new MatchModel("A", "♥"),
            new MatchModel("K", "♦"),
            new MatchModel("Q", "♣"),
            new MatchModel("J", "♠"),
            new MatchModel("10", "♥"),
            new MatchModel("9", "♦")
        };

        string path = "Assets/Settings/CardsThemeConfig.asset";
        AssetDatabase.CreateAsset(cardsTheme, path);
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = cardsTheme;
    }
}
#endif