using System.Collections.Generic;

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
    }
}
