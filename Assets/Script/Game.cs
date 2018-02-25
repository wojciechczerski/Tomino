namespace Tomino
{
    public class Game
    {
        public Board board = new Board(10, 20);
        public Position initialPosition = new Position(17, 4);
        public Piece piece;

        public void Start()
        {
            AddRandomPiece();
        }

        public void AddRandomPiece()
        {
            piece = new OPiece();
            foreach (var block in piece.blocks)
            {
                block.position.row += initialPosition.row;
                block.position.column += initialPosition.column;
                board.blocks.Add(block);
            }
        }

        public void Update()
        {
            piece.MoveDown();
            if (board.HasCollisions())
            {
                piece.MoveUp();
                AddRandomPiece();
            }
        }
    }
}
