namespace Tomino.Test.Editor.Helper
{
    public static class GameExtension
    {
        public static void WaitUntilPieceFallsAutomatically(this Game game)
        {
            var pieceFinishedFalling = false;

            game.PieceFinishedFallingEvent += EventHandler;

            while (!pieceFinishedFalling)
            {
                game.Update(1.0f);
            }

            game.PieceFinishedFallingEvent -= EventHandler;
            return;

            void EventHandler() { pieceFinishedFalling = true; }
        }
    }
}
