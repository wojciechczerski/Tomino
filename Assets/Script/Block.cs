namespace Tomino
{
    public class Block
    {
        public Color color;
        public Position position;

        public Block(Position position, Color color)
        {
            this.position = position;
            this.color = color;
        }
    }
}
