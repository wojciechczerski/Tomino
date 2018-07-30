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
            var allPositions = blocks.Select(block => block.position);
            var uniquePositions = new HashSet<Position>(allPositions);
            return allPositions.Count() != uniquePositions.Count();
        }

        public bool HasBoardCollisions()
        {
            return blocks.Find(CollidesWithBoard) != null;
        }

        public bool CollidesWithBoard(Block block)
        {
            return block.position.row < 0 ||
                   block.position.row >= height ||
                   block.position.column < 0 ||
                   block.position.column >= width;
        }

        override public int GetHashCode()
        {
            var list = blocks.Select(b => b.position.row ^ b.position.column);
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
            return blocks.FindAll(block => block.position.row == row);
        }

        void Remove(List<Block> blocksToRemove)
        {
            blocks.RemoveAll(block => blocksToRemove.Contains(block));
        }

        void MoveDownBlocksBelowRow(int row)
        {
            foreach (Block block in blocks)
            {
                if (block.position.row > row)
                {
                    block.position.row -= 1;
                }
            }
        }
    }
}
