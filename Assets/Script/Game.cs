namespace Tomino
{
    public class Game
    {
        public delegate void GameEventHandler();
        public event GameEventHandler FinishedEvent = delegate { };
        public event GameEventHandler PieceFinishedFallingEvent = delegate { };

        const float FallDelay = 1.0f;

        readonly Board board;
        readonly IPlayerInput input;
        readonly IPieceProvider pieceProvider;

        Piece fallingPiece;
        float elapsedTime = FallDelay;
        bool isPlaying;

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
            isPlaying = true;
            elapsedTime = 0;
            board.RemoveAllBlocks();
            AddPiece();
        }

        void AddPiece()
        {
            AddPiece(pieceProvider.GetPiece());
        }

        void AddPiece(Piece piece)
        {
            MovePieceToInitialPosition(piece);
            fallingPiece = piece;
            board.Add(fallingPiece);

            if (board.HasCollisions())
            {
                isPlaying = false;
                FinishedEvent();
            }
        }

        public void Update(float deltaTime)
        {
            if (!isPlaying) return;

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
            return input?.GetPlayerAction();
        }

        void HandlePlayerAction(PlayerAction action)
        {
            var resolver = new PieceCollisionResolver(fallingPiece, board);

            switch (action)
            {
                case PlayerAction.MoveLeft:
                    fallingPiece.MoveLeft();
                    break;

                case PlayerAction.MoveRight:
                    fallingPiece.MoveRight();
                    break;

                case PlayerAction.MoveDown:
                    fallingPiece.MoveDown();
                    ResetElapsedTime();
                    break;

                case PlayerAction.Rotate:
                    fallingPiece.Rotate();
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
            PieceFinishedFallingEvent();
            board.RemoveFullRows();
            AddPiece();
        }

        void Fall()
        {
            var didMoveDown = false;
            while (!board.HasCollisions())
            {
                fallingPiece.MoveDown();
                didMoveDown = true;
            }
            if (didMoveDown)
            {
                fallingPiece.Move(1, 0);
            }
        }

        void ResetElapsedTime()
        {
            elapsedTime = 0;
        }

        void MovePieceToInitialPosition(Piece piece)
        {
            var offsetRow = board.Top - piece.Top;
            var offsetCol = (board.width - piece.Width) / 2;

            foreach (var block in piece.blocks)
            {
                block.MoveBy(offsetRow, offsetCol);
            }
        }
    }
}
