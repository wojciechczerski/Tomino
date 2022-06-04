using System.Collections.Generic;

namespace Tomino
{
    /// <summary>
    /// Contains collection of blocks placed on the board and allows for moving them within the
    /// defined bounds.
    /// </summary>
    public class Board
    {
        /// <summary>
        /// The width of the board.
        /// </summary>
        public readonly int Width;

        /// <summary>
        /// The height of the board.
        /// </summary>
        public readonly int Height;

        /// <summary>
        /// The colleciton of blocks placed on the board.
        /// </summary>
        public List<Block> Blocks { get; private set; } = new List<Block>();

        /// <summary>
        /// The current falling piece.
        /// </summary>
        /// <value></value>
        public Piece Piece { get; private set; }

        /// <summary>
        /// The piece that will be added to the board when the current piece finishes falling.
        /// </summary>
        /// <returns></returns>
        public Piece NextPiece => pieceProvider.GetNextPiece();

        private readonly IPieceProvider pieceProvider;
        private int Top => Height - 1;

        /// <summary>
        /// Initializes board with specified size and a `BalancedPieceProvider`.
        /// </summary>
        /// <param name="width">The width of the board.</param>
        /// <param name="height">The height of the board.</param>
        public Board(int width, int height) : this(width, height, new BalancedRandomPieceProvider())
        {
        }

        /// <summary>
        /// Initializes board with specified size and piece provider.
        /// </summary>
        /// <param name="width">The width of the board.</param>
        /// <param name="height">The height of the board.</param>
        /// <param name="pieceProvider">The piece provider.</param>
        public Board(int width, int height, IPieceProvider pieceProvider)
        {
            Width = width;
            Height = height;
            this.pieceProvider = pieceProvider;
        }

        /// <summary>
        /// Determines whether blocks on the board collide with board bounds or with themselves.
        /// </summary>
        /// <returns>true if collisions were detected; false otherwise.</returns>
        public bool HasCollisions()
        {
            return HasBoardCollisions() || HasBlockCollisions();
        }

        private bool HasBlockCollisions()
        {
            var allPositions = Blocks.Map(block => block.Position);
            var uniquePositions = new HashSet<Position>(allPositions);
            return allPositions.Length != uniquePositions.Count;
        }

        private bool HasBoardCollisions()
        {
            return Blocks.Find(CollidesWithBoard) != null;
        }

        private bool CollidesWithBoard(Block block)
        {
            return block.Position.Row < 0 ||
                   block.Position.Row >= Height ||
                   block.Position.Column < 0 ||
                   block.Position.Column >= Width;
        }

        public override int GetHashCode()
        {
            int hash = 0;
            foreach (var block in Blocks)
            {
                var row = block.Position.Row;
                var column = block.Position.Column;
                var offset = Width * Height * (int)block.Type;
                var blockHash = offset + (row * Width) + column;
                hash += blockHash;
            }
            return hash;
        }

        /// <summary>
        /// Adds new piece.
        /// </summary>
        public void AddPiece()
        {
            Piece = pieceProvider.GetPiece();

            var offsetRow = Top - Piece.Top;
            var offsetCol = (Width - Piece.Width) / 2;

            foreach (var block in Piece.blocks)
            {
                block.MoveBy(offsetRow, offsetCol);
            }

            Blocks.AddRange(Piece.blocks);
        }

        /// <summary>
        /// Returns position of the piece shadow which is the final piece position if it starts
        /// falling.
        /// </summary>
        /// <returns>Collection of piece blocks positions.</returns>
        public Position[] GetPieceShadow()
        {
            var positions = Piece.GetPositions();
            _ = FallPiece();
            var shadowPositions = Piece.GetPositions().Values.Map(p => p);
            RestoreSavedPiecePosition(positions);
            return shadowPositions;
        }

        /// <summary>
        /// Moves the current piece left by 1 column.
        /// </summary>
        public bool MovePieceLeft()
        {
            return MovePiece(0, -1);
        }

        /// <summary>
        /// Moves the current piece right by 1 column.
        /// </summary>
        public bool MovePieceRight()
        {
            return MovePiece(0, 1);
        }

        /// <summary>
        /// Moves the current piece down by 1 row.
        /// </summary>
        public bool MovePieceDown()
        {
            return MovePiece(-1, 0);
        }

        private bool MovePiece(int rowOffset, int columnOffset)
        {
            foreach (var block in Piece.blocks)
            {
                block.MoveBy(rowOffset, columnOffset);
            }

            if (HasCollisions())
            {
                foreach (var block in Piece.blocks)
                {
                    block.MoveBy(-rowOffset, -columnOffset);
                }
                return false;
            }
            return true;
        }

        /// <summary>
        /// Rotates the current piece clockwise.
        /// </summary>
        public bool RotatePiece()
        {
            if (!Piece.canRotate)
            {
                return false;
            }

            Dictionary<Block, Position> piecePosition = Piece.GetPositions();
            var offset = Piece.blocks[0].Position;

            foreach (var block in Piece.blocks)
            {
                var row = block.Position.Row - offset.Row;
                var column = block.Position.Column - offset.Column;
                block.MoveTo(-column + offset.Row, row + offset.Column);
            }

            if (HasCollisions() && !ResolveCollisionsAfterRotation())
            {
                RestoreSavedPiecePosition(piecePosition);
                return false;
            }
            return true;
        }

        private bool ResolveCollisionsAfterRotation()
        {
            var columnOffsets = new int[] { -1, -2, 1, 2 };
            foreach (int offset in columnOffsets)
            {
                _ = MovePiece(0, offset);

                if (HasCollisions())
                {
                    _ = MovePiece(0, -offset);
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        private void RestoreSavedPiecePosition(Dictionary<Block, Position> piecePosition)
        {
            foreach (Block block in Piece.blocks)
            {
                block.MoveTo(piecePosition[block]);
            }
        }

        /// <summary>
        /// Immediately moves the current piece to the lowest possible row.
        /// </summary>
        /// <returns>Number of rows the piece has been moved down.</returns>
        public int FallPiece()
        {
            int rowsCount = 0;
            while (MovePieceDown())
            {
                rowsCount++;
            }
            return rowsCount;
        }

        /// <summary>
        /// Removes blocks in rows that hold maximum number of possible blocks (= board width). All
        /// blocks placed above the removed row are moved 1 row down.
        /// </summary>
        /// <returns></returns>
        public int RemoveFullRows()
        {
            int rowsRemoved = 0;
            for (int row = Height - 1; row >= 0; --row)
            {
                var rowBlocks = GetBlocksFromRow(row);
                if (rowBlocks.Count == Width)
                {
                    Remove(rowBlocks);
                    MoveDownBlocksBelowRow(row);
                    rowsRemoved += 1;
                }
            }
            return rowsRemoved;
        }

        /// <summary>
        /// Removes all blocks from the board.
        /// </summary>
        public void RemoveAllBlocks()
        {
            Blocks.Clear();
        }

        private List<Block> GetBlocksFromRow(int row)
        {
            return Blocks.FindAll(block => block.Position.Row == row);
        }

        private void Remove(List<Block> blocksToRemove)
        {
            _ = Blocks.RemoveAll(block => blocksToRemove.Contains(block));
        }

        private void MoveDownBlocksBelowRow(int row)
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
