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
            AddPiece(AvailablePieces.Random());
        }

        public void AddPiece(Piece newPiece)
        {
            piece = newPiece;
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

        public void HandlePlayerAction(PlayerAction action)
        {
            switch (action)
            {
                case PlayerAction.MoveLeft:
                    MovePieceLeft();
                    break;

                case PlayerAction.MoveRight:
                    MovePieceRight();
                    break;
            }
        }

        public void MovePieceLeft()
        {
            piece.MoveLeft();
            if (board.HasCollisions())
            {
                piece.MoveRight();
            }
        }

        public void MovePieceRight()
        {
            piece.MoveRight();
            if (board.HasCollisions())
            {
                piece.MoveLeft();
            }
        }
    }
}
