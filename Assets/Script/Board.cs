using System.Collections.Generic;
using System.Linq;

namespace Tomino
{
    public class Board
    {
        public int width;
        public int height;
        public List<Block> blocks = new List<Block>();

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
    }
}
