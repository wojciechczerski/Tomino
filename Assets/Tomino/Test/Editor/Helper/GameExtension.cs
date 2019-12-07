using System;
using Tomino;

public static class GameExtension
{
    public static void WaitUntilPieceFallsAutomatically(this Game game)
    {
        var pieceFinishedFalling = false;
        Game.GameEventHandler eventHandler = delegate { pieceFinishedFalling = true; };

        game.PieceFinishedFallingEvent += eventHandler;

        while (!pieceFinishedFalling) game.Update(1.0f);

        game.PieceFinishedFallingEvent -= eventHandler;
    }
}