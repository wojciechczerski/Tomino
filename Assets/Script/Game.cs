namespace Tomino
{
    public class Game
    {
        const float FallDelay = 1.0f;

        public Position initialPosition = new Position(17, 4);
        public Piece piece;

        Board board;
        IPlayerInput input;
        IPieceProvider pieceProvider;
        float elapsedTime = FallDelay;

        public Game(Board board,
                    IPlayerInput input,
                    IPieceProvider pieceProvider)
        {
            this.board = board;
            this.input = input;
            this.pieceProvider = pieceProvider;
        }

        public void Start()
        {
            AddPiece();
        }

        void AddPiece()
        {
            AddPiece(pieceProvider.GetPiece());
        }

        void AddPiece(Piece newPiece)
        {
            piece = newPiece;
            board.Add(piece, initialPosition);
        }

        public void Update(float deltaTime)
        {
            var action = GetInputAction();
            if (action.HasValue)
            {
                HandlePlayerAction(action.Value);
            }
            else
            {
                elapsedTime += deltaTime;
                if (elapsedTime >= FallDelay)
                {
                    HandlePlayerAction(PlayerAction.MoveDown);
                    ResetElapsedTime();
                }
            }
        }

        PlayerAction? GetInputAction()
        {
            if (input != null)
            {
                return input.GetPlayerAction();
            }
            return null;
        }

        void HandlePlayerAction(PlayerAction action)
        {
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
                    PieceFinishedFalling();
                    break;
            }

            if (board.HasCollisions())
            {
                resolver.ResolveCollisions(action == PlayerAction.Rotate);
                if (action == PlayerAction.MoveDown)
                {
                    PieceFinishedFalling();
                }
            }
        }

        void PieceFinishedFalling()
        {
            board.RemoveFullRows();
            AddPiece();
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
    }
}
