using System.Collections.Generic;
using System.Linq;

namespace Tomino
{
    public class Board
    {
        public readonly int width;
        public readonly int height;
        public List<Block> Blocks { get; private set; } = new List<Block>();

        public int Top
        {
            get
            {
                return height - 1;
            }
        }

        public Board(int width, int height)
        {
            this.width = width;
            this.height = height;
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

        public void Add(Piece piece)
        {
            Blocks.AddRange(piece.blocks);
        }

        public void RemoveFullRows()
        {
            for (int row = height - 1; row >= 0; --row)
            {
                var rowBlocks = GetBlocksFromRow(row);
                if (rowBlocks.Count == width)
                {
                    Remove(rowBlocks);
                    MoveDownBlocksBelowRow(row);
                }
            }
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
