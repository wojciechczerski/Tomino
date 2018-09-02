using System.Collections.Generic;

namespace Tomino
{
    public class PieceCollisionResolver
    {
        readonly Piece piece;
        readonly Board board;
        readonly Dictionary<Block, Position> piecePosition;

        public PieceCollisionResolver(Piece piece, Board board)
        {
            this.piece = piece;
            this.board = board;
            piecePosition = piece.GetPositions();
        }

        public void ResolveCollisions(bool afterRotation)
        {
            var columnOffsets = new int[] { };
            if (afterRotation)
            {
                columnOffsets = new int[] { -1, -2, 1, 2 };
            }
            ResolveCollisions(columnOffsets);
        }

        void ResolveCollisions(int[] columnOffsets)
        {
            foreach (int offset in columnOffsets)
            {
                piece.Move(0, offset);

                if (board.HasCollisions())
                {
                    piece.Move(0, -offset);
                }
                else
                {
                    return;
                }
            }
            RestoreSavedPiecePosition();
        }

        void RestoreSavedPiecePosition()
        {
            foreach (Block block in piece.blocks)
            {
                block.MoveTo(piecePosition[block]);
            }
        }
    }
}
