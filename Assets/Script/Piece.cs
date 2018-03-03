using System.Linq;

namespace Tomino
{
    public class Piece
    {
        public enum Type
        {
            I, J, L, O, S, T, Z
        }

        public Block[] blocks;

        public Piece(Position[] blockPositions, Type type)
        {
            blocks = blockPositions.Select(position => new Block(position, type)).ToArray();
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
