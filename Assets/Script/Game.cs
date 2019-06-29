namespace Tomino
{
    public class Game
    {
        public delegate void GameEventHandler();
        public event GameEventHandler FinishedEvent = delegate { };
        public event GameEventHandler PieceFinishedFallingEvent = delegate { };
        public Score Score { get; private set; }

        const float FallDelay = 1.0f;

        readonly Board board;
        readonly IPlayerInput input;

        float elapsedTime = FallDelay;
        bool isPlaying;

        public Game(Board board, IPlayerInput input)
        {
            this.board = board;
            this.input = input;
        }

        public void Start()
        {
            isPlaying = true;
            elapsedTime = 0;
            Score = new Score();
            board.RemoveAllBlocks();
            AddPiece();
        }

        void AddPiece()
        {
            board.AddPiece();
            if (board.HasCollisions())
            {
                isPlaying = false;
                FinishedEvent();
            }
        }

        public void Update(float deltaTime)
        {
            if (!isPlaying) return;

            var action = input?.GetPlayerAction();
            if (action.HasValue)
            {
                HandlePlayerAction(action.Value);
            }
            else
            {
                HandleAutomaticPieceFalling(deltaTime);
            }
        }

        void HandleAutomaticPieceFalling(float deltaTime)
        {
            elapsedTime += deltaTime;
            if (elapsedTime >= FallDelay)
            {
                if (!board.MovePieceDown())
                {
                    PieceFinishedFalling();
                }
                ResetElapsedTime();
            }
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
                    Score.PieceFinishedFalling(board.FallPiece());
                    ResetElapsedTime();
                    PieceFinishedFalling();
                    break;
            }
        }

        void PieceFinishedFalling()
        {
            PieceFinishedFallingEvent();
            Score.RowsCleared(board.RemoveFullRows());
            AddPiece();
        }

        void ResetElapsedTime()
        {
            elapsedTime = 0;
        }
    }
}
