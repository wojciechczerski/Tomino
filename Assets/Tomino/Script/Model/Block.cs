namespace Tomino.Model
{
    /// <summary>
    /// A block with specified type that can be placed (and moved) on a board.
    /// </summary>
    public class Block
    {
        /// <summary>
        /// The type of the block.
        /// </summary>
        public PieceType Type { get; private set; }

        /// <summary>
        /// The position of the block.
        /// </summary>
        public Position Position { get; private set; }

        /// <summary>
        /// Creates a block with specified position and type.
        /// </summary>
        /// <param name="position">The position of the block.</param>
        /// <param name="type">The type of the block.</param>
        public Block(Position position, PieceType type)
        {
            Position = position;
            Type = type;
        }

        /// <summary>
        /// Moves the block to specified row and column.
        /// </summary>
        /// <param name="row">The row to which to move the block.</param>
        /// <param name="column">The column to which to move the block.</param>
        public void MoveTo(int row, int column)
        {
            MoveTo(new Position(row, column));
        }

        /// <summary>
        /// Moves the block to new position.
        /// </summary>
        /// <param name="position">The new position.</param>
        public void MoveTo(Position position)
        {
            Position = position;
        }

        /// <summary>
        /// Moves the block by specified offset.
        /// </summary>
        /// <param name="rowOffset">The row offset.</param>
        /// <param name="columntOffset">The column offset.</param>
        public void MoveBy(int rowOffset, int columntOffset)
        {
            MoveTo(Position.Row + rowOffset, Position.Column + columntOffset);
        }
    }
}
