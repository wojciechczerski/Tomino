using Tomino;

public class StubPieceProvider : IPieceProvider
{
    public Piece piece = AvailablePieces.TPiece();

    public Piece GetPiece()
    {
        return piece;
    }
}

