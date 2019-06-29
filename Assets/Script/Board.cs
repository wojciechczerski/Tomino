using System.Collections.Generic;
using System.Linq;

namespace Tomino
{
    public class Board
    {
        public readonly int width;
        public readonly int height;
        public readonly int top;
        public List<Block> Blocks { get; private set; } = new List<Block>();

        public Piece piece { get; private set; }

        public Piece nextPiece => pieceProvider.GetNextPiece();

        readonly IPieceProvider pieceProvider;

        public Board(int width, int height, IPieceProvider pieceProvider)
        {
            this.width = width;
            this.height = height;
            this.pieceProvider = pieceProvider;
            top = height - 1;
        }

        public bool HasCollisions()
        {
            return HasBoardCollisions() || HasBlockCollisions();
        }

        public bool HasBlockCollisions()
        {
            var allPositions = Blocks.Select(block => block.Position);
            var uniquePositions = new HashSet<Position>(allPositions);
            return allPositions.Count() != uniquePositions.Count();
        }

        public bool HasBoardCollisions()
        {
            return Blocks.Find(CollidesWithBoard) != null;
        }

        public bool CollidesWithBoard(Block block)
        {
            return block.Position.Row < 0 ||
                   block.Position.Row >= height ||
                   block.Position.Column < 0 ||
                   block.Position.Column >= width;
        }

        override public int GetHashCode()
        {
            return Blocks.Aggregate(0, (hash, block) =>
            {
                var row = block.Position.Row;
                var column = block.Position.Column;
                var offset = width * height * (int)block.Type;
                var blockHash = offset + row * width + column;
                return hash + blockHash;
            });
        }

        public void AddPiece()
        {
            piece = pieceProvider.GetPiece();

            var offsetRow = top - piece.Top;
            var offsetCol = (width - piece.Width) / 2;

            foreach (var block in piece.blocks)
            {
                block.MoveBy(offsetRow, offsetCol);
            }

            Blocks.AddRange(piece.blocks);
        }

        public Position[] GetPieceShadow()
        {
            var positions = piece.GetPositions();
            FallPiece();
            var shadowPositions = piece.GetPositions().Values.ToArray();
            RestoreSavedPiecePosition(positions);
            return shadowPositions;
        }

        public void MovePieceLeft() => MovePiece(0, -1);

        public void MovePieceRight() => MovePiece(0, 1);

        public bool MovePieceDown() => MovePiece(-1, 0);

        public bool MovePiece(int rowOffset, int columnOffset)
        {
            foreach (var block in piece.blocks)
            {
                block.MoveBy(rowOffset, columnOffset);
            }

            if (HasCollisions())
            {
                foreach (var block in piece.blocks)
                {
                    block.MoveBy(-rowOffset, -columnOffset);
                }
                return false;
            }
            return true;
        }

        public void RotatePiece()
        {
            if (!piece.canRotate)
            {
                return;
            }

            Dictionary<Block, Position> piecePosition = piece.GetPositions();
            var offset = piece.blocks[0].Position;

            foreach (var block in piece.blocks)
            {
                var row = block.Position.Row - offset.Row;
                var column = block.Position.Column - offset.Column;
                block.MoveTo(-column + offset.Row, row + offset.Column);
            }

            if (HasCollisions() && !ResolveCollisionsAfterRotation())
            {
                RestoreSavedPiecePosition(piecePosition);
            }
        }

        bool ResolveCollisionsAfterRotation()
        {
            var columnOffsets = new int[] { -1, -2, 1, 2 };
            foreach (int offset in columnOffsets)
            {
                MovePiece(0, offset);

                if (HasCollisions())
                {
                    MovePiece(0, -offset);
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        void RestoreSavedPiecePosition(Dictionary<Block, Position> piecePosition)
        {
            foreach (Block block in piece.blocks)
            {
                block.MoveTo(piecePosition[block]);
            }
        }

        public int FallPiece()
        {
            int rowsCount = 0;
            while (MovePieceDown())
            {
                rowsCount++;
            }
            return rowsCount;
        }

        public int RemoveFullRows()
        {
            int rowsRemoved = 0;
            for (int row = height - 1; row >= 0; --row)
            {
                var rowBlocks = GetBlocksFromRow(row);
                if (rowBlocks.Count == width)
                {
                    Remove(rowBlocks);
                    MoveDownBlocksBelowRow(row);
                    rowsRemoved += 1;
                }
            }
            return rowsRemoved;
        }

        public void RemoveAllBlocks()
        {
            Blocks.Clear();
        }

        List<Block> GetBlocksFromRow(int row)
        {
            return Blocks.FindAll(block => block.Position.Row == row);
        }

        void Remove(List<Block> blocksToRemove)
        {
            Blocks.RemoveAll(block => blocksToRemove.Contains(block));
        }

        void MoveDownBlocksBelowRow(int row)
        {
            foreach (Block block in Blocks)
            {
                if (block.Position.Row > row)
                {
                    block.MoveBy(-1, 0);
                }
            }
        }
    }
}
