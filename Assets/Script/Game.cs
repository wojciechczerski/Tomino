namespace Tomino
{
    public class Game
    {
        const float FallDelay = 1.0f;

        public delegate void BoardChangedDelgate(Board board);

        public Board board = new Board(10, 20);
        public Position initialPosition = new Position(17, 4);
        public Piece piece;
        public BoardChangedDelgate onBoardChanged;

        float elapsedTime = FallDelay;

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

        public void Update(float deltaTime)
        {
            elapsedTime += deltaTime;
            if (elapsedTime >= FallDelay)
            {
                HandlePlayerAction(PlayerAction.MoveDown);
                ResetElapsedTime();
            }
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
                    ResetElapsedTime();
                    break;

                case PlayerAction.Rotate:
                    piece.Rotate();
                    break;

                case PlayerAction.Fall:
                    Fall();
                    AddRandomPiece();
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

        void Fall()
        {
            var didMoveDown = false;
            while (!board.HasCollisions())
            {
                piece.MoveDown();
                didMoveDown = true;
            }
            if (didMoveDown)
            {
                piece.Move(1, 0);
            }
        }

        void ResetElapsedTime()
        {
            elapsedTime = 0;
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
