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
            board.AddPiece(piece);

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
                    board.MovePieceLeft();
                    break;

                case PlayerAction.MoveRight:
                    board.MovePieceRight();
                    break;

                case PlayerAction.MoveDown:
                    ResetElapsedTime();
                    if (!board.MovePieceDown())
                    {
                        PieceFinishedFalling();
                    }
                    break;

                case PlayerAction.Rotate:
                    board.RotatePiece();
                    break;

                case PlayerAction.Fall:
                    board.FallPiece();
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
    }
}
