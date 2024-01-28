using Tomino.Model;

namespace Tomino.Test.Editor.Helper
{
    public enum StubPieceType
    {
        OneBlock, TwoBlocks
    }

    public class StubPieceProvider : IPieceProvider
    {
        private readonly StubPieceType _pieceType;

        public StubPieceProvider(StubPieceType type = StubPieceType.OneBlock)
        {
            _pieceType = type;
        }

        public Piece GetPiece()
        {
            return _pieceType switch
            {
                StubPieceType.OneBlock => CreateOneBlockPiece(),
                StubPieceType.TwoBlocks => CreateTwoBlocksPiece(),
                _ => default
            };
        }

        public Piece GetNextPiece()
        {
            return GetPiece();
        }

        private static Piece CreateOneBlockPiece()
        {
            return new Piece(new Position[] { new(0, 0) }, PieceType.I);
        }

        private static Piece CreateTwoBlocksPiece()
        {
            var blockPositions = new Position[] { new(0, 0), new(1, 0) };
            return new Piece(blockPositions, PieceType.I);
        }
    }
}
