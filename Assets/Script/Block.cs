namespace Tomino
{
    public class Block
    {
        public Piece.Type type;
        public Position position;

        public Block(Position position, Piece.Type type)
        {
            this.position = position;
            this.type = type;
        }
    }
}
