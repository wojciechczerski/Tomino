using System.Collections.Generic;
using System.Linq;

namespace Tomino
{
    public class Board
    {
        public int width;
        public int height;
        public List<Block> blocks = new List<Block>();

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
            var allPositions = blocks.Select(block => block.Position);
            var uniquePositions = new HashSet<Position>(allPositions);
            return allPositions.Count() != uniquePositions.Count();
        }

        public bool HasBoardCollisions()
        {
            return blocks.Find(CollidesWithBoard) != null;
        }

        public bool CollidesWithBoard(Block block)
        {
            return block.Position.row < 0 ||
                   block.Position.row >= height ||
                   block.Position.column < 0 ||
                   block.Position.column >= width;
        }

        override public int GetHashCode()
        {
            var list = blocks.Select(b => b.Position.row ^ b.Position.column);
            return list.GetHashCode();
        }

        public void Add(Piece piece)
        {
            blocks.AddRange(piece.blocks);
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

        List<Block> GetBlocksFromRow(int row)
        {
            return blocks.FindAll(block => block.Position.row == row);
        }

        void Remove(List<Block> blocksToRemove)
        {
            blocks.RemoveAll(block => blocksToRemove.Contains(block));
        }

        void MoveDownBlocksBelowRow(int row)
        {
            foreach (Block block in blocks)
            {
                if (block.Position.row > row)
                {
                    block.MoveBy(-1, 0);
                }
            }
        }
    }
}
