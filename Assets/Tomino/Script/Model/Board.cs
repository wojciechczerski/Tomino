using System.Collections.Generic;
using System.Linq;
using Tomino.Shared;

namespace Tomino.Model
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
        public readonly int width;

        /// <summary>
        /// The height of the board.
        /// </summary>
        public readonly int height;

        /// <summary>
        /// The collection of blocks placed on the board.
        /// </summary>
        public List<Block> Blocks { get; } = new();

        /// <summary>
        /// The current falling piece.
        /// </summary>
        /// <value></value>
        public Piece Piece { get; private set; }

        /// <summary>
        /// The piece that will be added to the board when the current piece finishes falling.
        /// </summary>
        /// <returns></returns>
        public Piece NextPiece => _pieceProvider.GetNextPiece();

        private readonly IPieceProvider _pieceProvider;
        private int Top => height - 1;

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
            this.width = width;
            this.height = height;
            _pieceProvider = pieceProvider;
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
                   block.Position.Row >= height ||
                   block.Position.Column < 0 ||
                   block.Position.Column >= width;
        }

        public override int GetHashCode()
        {
            return (from block in Blocks
                let row = block.Position.Row
                let column = block.Position.Column
                let offset = width * height * (int)block.Type
                select offset + row * width + column).Sum();
        }

        /// <summary>
        /// Adds new piece.
        /// </summary>
        public void AddPiece()
        {
            Piece = _pieceProvider.GetPiece();

            var offsetRow = Top - Piece.Top;
            var offsetCol = (width - Piece.Width) / 2;

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
        public ICollection<Position> GetPieceShadow()
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

            if (!HasCollisions()) return true;

            foreach (var block in Piece.blocks)
            {
                block.MoveBy(-rowOffset, -columnOffset);
            }

            return false;
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

            var piecePosition = Piece.GetPositions();
            var offset = Piece.blocks[0].Position;

            foreach (var block in Piece.blocks)
            {
                var row = block.Position.Row - offset.Row;
                var column = block.Position.Column - offset.Column;
                block.MoveTo(-column + offset.Row, row + offset.Column);
            }

            if (!HasCollisions() || ResolveCollisionsAfterRotation()) return true;

            RestoreSavedPiecePosition(piecePosition);
            return false;
        }

        private bool ResolveCollisionsAfterRotation()
        {
            var columnOffsets = new[] { -1, -2, 1, 2 };
            foreach (var offset in columnOffsets)
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

        private void RestoreSavedPiecePosition(IReadOnlyDictionary<Block, Position> piecePosition)
        {
            foreach (var block in Piece.blocks)
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
            var rowsCount = 0;
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
            var rowsRemoved = 0;
            for (var row = height - 1; row >= 0; --row)
            {
                var rowBlocks = GetBlocksFromRow(row);
                if (rowBlocks.Count != width) continue;

                Remove(rowBlocks);
                MoveDownBlocksBelowRow(row);
                rowsRemoved += 1;
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

        private void Remove(ICollection<Block> blocksToRemove)
        {
            _ = Blocks.RemoveAll(blocksToRemove.Contains);
        }

        private void MoveDownBlocksBelowRow(int row)
        {
            foreach (var block in Blocks.Where(block => block.Position.Row > row))
            {
                block.MoveBy(-1, 0);
            }
        }
    }
}
