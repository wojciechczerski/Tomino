namespace Tomino
{
    public class Block
    {
        public Piece.Type type;
        public Position Position { get; private set; }

        public Block(Position position, Piece.Type type)
        {
            Position = position;
            this.type = type;
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
