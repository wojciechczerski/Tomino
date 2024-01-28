namespace Tomino.Model
{
    public static class Text
    {
        public const string GameFinished = "GAME FINISHED";
        public const string GamePaused = "GAME PAUSED";
        public const string PlayAgain = "PLAY AGAIN";
        public const string Resume = "RESUME";
        public const string NewGame = "NEW GAME";
        public const string Settings = "SETTINGS";
        public const string Music = "MUSIC";
        public const string ScreenButtons = "SCREEN BUTTONS";
        public const string Theme = "THEME";
        public const string DefaultTheme = "DEFAULT";
        public const string AutumnTheme = "AUTUMN";
        public const string SummerTheme = "SUMMER";
        public const string TealTheme = "TEAL";
        public const string Close = "CLOSE";
    }

    public static class ScoreFormat
    {
        public const int Length = 9;
        public const char PadCharacter = '0';
    }

    public static class Input
    {
        public const float KeyRepeatDelay = 0.18f;
        public const float KeyRepeatInterval = 0.07f;
    }
}
