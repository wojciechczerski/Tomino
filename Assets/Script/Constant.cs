namespace Constant
{
    public static class Text
    {
        public static readonly string GameFinished = "GAME FINISHED";
        public static readonly string GamePaused = "GAME PAUSED";
        public static readonly string PlayAgain = "PLAY AGAIN";
        public static readonly string Resume = "RESUME";
        public static readonly string NewGame = "NEW GAME";
    }

    public static class ScoreFormat
    {
        public static readonly int Length = 9;
        public static readonly char PadCharacter = '0';
    }

    public static class Input
    {
        public static readonly float KeyRepeatDelay = 0.18f;
        public static readonly float KeyRepeatInterval = 0.07f;
    }
}
