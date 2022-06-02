using Tomino;

public enum StubPieceType
{
    OneBlock, TwoBlocks
}

public class StubPieceProvider : IPieceProvider
{
    private readonly StubPieceType pieceType;

    public StubPieceProvider(StubPieceType type = StubPieceType.OneBlock)
    {
        pieceType = type;
    }

    public Piece GetPiece()
    {
        return pieceType switch
        {
            StubPieceType.OneBlock => CreateOneBlockPiece(),
            StubPieceType.TwoBlocks => CreateTwoBlocksPiece(),
            _ => default,
        };
    }

    public Piece GetNextPiece()
    {
        return GetPiece();
    }

    private static Piece CreateOneBlockPiece()
    {
        return new Piece(new Position[] { new Position(0, 0), }, PieceType.I);
    }

    private static Piece CreateTwoBlocksPiece()
    {
        var blockPositions = new Position[] { new Position(0, 0), new Position(1, 0) };
        return new Piece(blockPositions, PieceType.I);
    }
}
