using UnityEngine;

namespace Tomino.Model
{
    public static class TextID
    {
        public const string NextPiece = "next";
        public const string Score = "score";
        public const string Lines = "lines";
        public const string Level = "level";
        public const string GameFinished = "game-finished";
        public const string GamePaused = "game-paused";
        public const string PlayAgain = "play-again";
        public const string Resume = "resume";
        public const string NewGame = "new-game";
        public const string Settings = "settings";
        public const string Music = "music";
        public const string ScreenButtons = "screen-buttons";
        public const string Theme = "theme";
        public const string DefaultTheme = "theme-default";
        public const string AutumnTheme = "theme-autumn";
        public const string SummerTheme = "theme-summer";
        public const string TealTheme = "theme-teal";
        public const string Close = "close";
    }

    [CreateAssetMenu(fileName = "Localization", menuName = "Tomino/Localization", order = 3)]
    public class Localization : ScriptableObject
    {
        public string nextPiece = "next";
        public string score = "score";
        public string lines = "lines";
        public string level = "level";
        public string gameFinished = "GAME FINISHED";
        public string gamePaused = "GAME PAUSED";
        public string playAgain = "PLAY AGAIN";
        public string resume = "RESUME";
        public string newGame = "NEW GAME";
        public string settings = "SETTINGS";
        public string music = "MUSIC";
        public string screenButtons = "SCREEN BUTTONS";
        public string theme = "THEME";
        public string defaultTheme = "DEFAULT";
        public string autumnTheme = "AUTUMN";
        public string summerTheme = "SUMMER";
        public string tealTheme = "TEAL";
        public string close = "CLOSE";

        public string GetLocalizedTextForID(string textID)
        {
            return textID switch
            {
                TextID.NextPiece => nextPiece,
                TextID.Score => score,
                TextID.Lines => lines,
                TextID.Level => level,
                TextID.GameFinished => gameFinished,
                TextID.GamePaused => gamePaused,
                TextID.PlayAgain => playAgain,
                TextID.Resume => resume,
                TextID.NewGame => newGame,
                TextID.Settings => settings,
                TextID.Music => music,
                TextID.ScreenButtons => screenButtons,
                TextID.Theme => theme,
                TextID.DefaultTheme => defaultTheme,
                TextID.AutumnTheme => autumnTheme,
                TextID.SummerTheme => summerTheme,
                TextID.TealTheme => tealTheme,
                TextID.Close => close,
                _ => "<null>"
            };
        }
    }
}
