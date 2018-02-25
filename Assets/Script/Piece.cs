using System.Linq;

namespace Tomino
{
    public class Piece
    {
        public Block[] blocks;

        public Piece(Position[] blockPositions, Color color)
        {
            blocks = blockPositions.Select(position => new Block(position, color)).ToArray();
        }

        public void MoveDown()
        {
            foreach (var block in blocks)
            {
                block.position.row -= 1;
            }
        }

        public void MoveUp()
        {
            foreach (var block in blocks)
            {
                block.position.row += 1;
            }
        }
    }
}
