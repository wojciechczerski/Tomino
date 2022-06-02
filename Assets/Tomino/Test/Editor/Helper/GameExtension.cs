using Tomino;

public static class GameExtension
{
    public static void WaitUntilPieceFallsAutomatically(this Game game)
    {
        var pieceFinishedFalling = false;
        void eventHandler() { pieceFinishedFalling = true; }

        game.PieceFinishedFallingEvent += eventHandler;

        while (!pieceFinishedFalling)
        {
            game.Update(1.0f);
        }

        game.PieceFinishedFallingEvent -= eventHandler;
    }
}
