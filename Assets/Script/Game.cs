namespace Tomino
{
    public class Game
    {
        public delegate void GameEventHandler();
        public event GameEventHandler FinishedEvent = delegate { };
        public event GameEventHandler PieceFinishedFallingEvent = delegate { };
        public int Score { get; private set; }

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
            Score = 0;
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
            switch (action)
            {
                case PlayerAction.MoveLeft:
                    board.MoveLeft(fallingPiece);
                    break;

                case PlayerAction.MoveRight:
                    board.MoveRight(fallingPiece);
                    break;

                case PlayerAction.MoveDown:
                    ResetElapsedTime();
                    if (!board.MoveDown(fallingPiece))
                    {
                        PieceFinishedFalling();
                    }
                    break;

                case PlayerAction.Rotate:
                    board.Rotate(fallingPiece);
                    break;

                case PlayerAction.Fall:
                    board.Fall(fallingPiece);
                    ResetElapsedTime();
                    PieceFinishedFalling();
                    break;
            }
        }

        void PieceFinishedFalling()
        {
            PieceFinishedFallingEvent();
            Score += board.RemoveFullRows();
            AddPiece();
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
