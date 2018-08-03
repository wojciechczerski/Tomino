using Tomino;

public class StubPieceProvider : IPieceProvider
{
    public Piece piece = StubPiece();

    public Piece GetPiece()
    {
        return piece;
    }

    static Piece StubPiece()
    {
        var blockPositions = new Position[]
        {
            new Position(0, 0),
            new Position(1, 0)
        };
        return new Piece(blockPositions, Piece.Type.I);
    }
}

