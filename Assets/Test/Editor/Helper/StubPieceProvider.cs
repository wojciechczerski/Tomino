using Tomino;

public enum StubPieceType
{
    OneBlock, TwoBlocks
}

public class StubPieceProvider : IPieceProvider
{
    private StubPieceType pieceType;

    public StubPieceProvider(StubPieceType type = StubPieceType.OneBlock)
    {
        pieceType = type;
    }

    public Piece GetPiece()
    {
        switch (pieceType)
        {
            case StubPieceType.OneBlock:
                return CreateOneBlockPiece();
            case StubPieceType.TwoBlocks:
                return CreateTwoBlocksPiece();
        }
        return default(Piece);
    }

    public Piece GetNextPiece() => GetPiece();

    static Piece CreateOneBlockPiece()
    {
        return new Piece(new Position[] { new Position(0, 0), }, PieceType.I);
    }

    static Piece CreateTwoBlocksPiece()
    {
        var blockPositions = new Position[] { new Position(0, 0), new Position(1, 0) };
        return new Piece(blockPositions, PieceType.I);
    }
}
