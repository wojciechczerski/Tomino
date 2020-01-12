namespace Tomino
{
    public class Game
    {
        public delegate void GameEventHandler();
        public event GameEventHandler ResumedEvent = delegate { };
        public event GameEventHandler PausedEvent = delegate { };
        public event GameEventHandler FinishedEvent = delegate { };
        public event GameEventHandler PieceMovedEvent = delegate { };
        public event GameEventHandler PieceRotatedEvent = delegate { };
        public event GameEventHandler PieceFinishedFallingEvent = delegate { };
        public Score Score { get; private set; }
        public Level Level { get; private set; }

        readonly Board board;
        readonly IPlayerInput input;

        float elapsedTime;
        bool isPlaying;

        public Game(Board board, IPlayerInput input)
        {
            this.board = board;
            this.input = input;
        }

        public void Start()
        {
            isPlaying = true;
            ResumedEvent();
            elapsedTime = 0;
            Score = new Score();
            Level = new Level();
            board.RemoveAllBlocks();
            AddPiece();
        }

        public void Resume()
        {
            isPlaying = true;
            ResumedEvent();
        }

        public void Pause()
        {
            isPlaying = false;
            PausedEvent();
        }

        void AddPiece()
        {
            board.AddPiece();
            if (board.HasCollisions())
            {
                isPlaying = false;
                PausedEvent();
                FinishedEvent();
            }
        }

        public void Update(float deltaTime)
        {
            if (!isPlaying) return;

            input.Update();

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
            if (elapsedTime >= Level.FallDelay)
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
            var pieceMoved = false;
            switch (action)
            {
                case PlayerAction.MoveLeft:
                    pieceMoved = board.MovePieceLeft();
                    break;

                case PlayerAction.MoveRight:
                    pieceMoved = board.MovePieceRight();
                    break;

                case PlayerAction.MoveDown:
                    ResetElapsedTime();
                    if (board.MovePieceDown())
                    {
                        pieceMoved = true;
                        Score.PieceMovedDown();
                    }
                    else
                    {
                        PieceFinishedFalling();
                    }
                    break;

                case PlayerAction.Rotate:
                    var didRotate = board.RotatePiece();
                    if (didRotate) PieceRotatedEvent();
                    break;

                case PlayerAction.Fall:
                    Score.PieceFinishedFalling(board.FallPiece());
                    ResetElapsedTime();
                    PieceFinishedFalling();
                    break;
            }
            if (pieceMoved)
            {
                PieceMovedEvent();
            }
        }

        void PieceFinishedFalling()
        {
            PieceFinishedFallingEvent();
            int rowsCount = board.RemoveFullRows();
            Score.RowsCleared(rowsCount);
            Level.RowsCleared(rowsCount);
            AddPiece();
        }

        void ResetElapsedTime()
        {
            elapsedTime = 0;
        }
    }
}
