namespace Tomino
{
    public class Block
    {
        public PieceType Type { get; private set; }
        public Position Position { get; private set; }

        public Block(Position position, PieceType type)
        {
            Position = position;
            this.Type = type;
        }

        public void MoveTo(int row, int column)
        {
            MoveTo(new Position(row, column));
        }

        public void MoveTo(Position position)
        {
            Position = position;
        }

        public void MoveBy(int rowOffset, int columntOffset)
        {
            MoveTo(Position.Row + rowOffset, Position.Column + columntOffset);
        }
    }
}
