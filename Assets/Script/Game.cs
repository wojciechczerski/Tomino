namespace Tomino
{
    public class Game
    {
        public delegate void BoardChangedDelgate(Board board);

        public Board board = new Board(10, 20);
        public Position initialPosition = new Position(17, 4);
        public Piece piece;
        public BoardChangedDelgate onBoardChanged;

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
            NotifyDelegateThatBoardHasChanged();
        }

        public void Update()
        {
            HandlePlayerAction(PlayerAction.MoveDown);
        }

        public void HandlePlayerAction(PlayerAction action)
        {
            var hash = board.GetHashCode();
            var resolver = new PieceCollisionResolver(piece, board);

            switch (action)
            {
                case PlayerAction.MoveLeft:
                    piece.MoveLeft();
                    break;

                case PlayerAction.MoveRight:
                    piece.MoveRight();
                    break;

                case PlayerAction.MoveDown:
                    piece.MoveDown();
                    break;

                case PlayerAction.Rotate:
                    piece.Rotate();
                    break;
            }

            if (board.HasCollisions())
            {
                resolver.ResolveCollisions(action == PlayerAction.Rotate);
                if (action == PlayerAction.MoveDown)
                {
                    AddRandomPiece();
                }
            }

            if (hash != board.GetHashCode())
            {
                NotifyDelegateThatBoardHasChanged();
            }
        }

        void NotifyDelegateThatBoardHasChanged()
        {
            if (onBoardChanged != null)
            {
                onBoardChanged(board);
            }
        }
    }
}
