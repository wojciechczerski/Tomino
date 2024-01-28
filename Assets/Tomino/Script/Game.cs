using Tomino.Input;
using Tomino.Model;

namespace Tomino
{
    /// <summary>
    /// Controls the game logic by handling user input and updating the board state.
    /// </summary>
    public class Game
    {
        public delegate void GameEventHandler();

        /// <summary>
        /// The event triggered when the game is finished.
        /// </summary>
        public event GameEventHandler FinishedEvent = delegate { };

        /// <summary>
        /// The event triggered when the piece is moved.
        /// </summary>
        public event GameEventHandler PieceMovedEvent = delegate { };

        /// <summary>
        /// The event triggered when the piece is rotated.
        /// </summary>
        public event GameEventHandler PieceRotatedEvent = delegate { };

        /// <summary>
        /// The event triggered when the piece finishes falling.
        /// </summary>
        public event GameEventHandler PieceFinishedFallingEvent = delegate { };

        /// <summary>
        /// The current score.
        /// </summary>
        public Score Score { get; private set; }

        /// <summary>
        /// The current level.
        /// </summary>
        public Level Level { get; private set; }

        private readonly Board _board;
        private readonly IPlayerInput _input;

        private PlayerAction? _nextAction;
        private float _elapsedTime;
        private bool _isPlaying;

        /// <summary>
        /// Creates a game with specified board and input.
        /// </summary>
        /// <param name="board">The board on which the blocks will be placed.</param>
        /// <param name="input">The input used for pooling player events.</param>
        public Game(Board board, IPlayerInput input)
        {
            _board = board;
            _input = input;
            PieceFinishedFallingEvent += input.Cancel;
        }

        /// <summary>
        /// Starts the game.
        /// </summary>
        public void Start()
        {
            _isPlaying = true;
            _elapsedTime = 0;
            Score = new Score();
            Level = new Level();
            _board.RemoveAllBlocks();
            AddPiece();
        }

        /// <summary>
        /// Resumes paused game.
        /// </summary>
        public void Resume()
        {
            _isPlaying = true;
        }

        /// <summary>
        /// Pauses started game.
        /// </summary>
        public void Pause()
        {
            _isPlaying = false;
        }

        /// <summary>
        /// Sets the player action that the game should process in the next update.
        /// </summary>
        /// <param name="action">The next player action to process.</param>
        public void SetNextAction(PlayerAction action)
        {
            _nextAction = action;
        }

        private void AddPiece()
        {
            _board.AddPiece();
            if (!_board.HasCollisions()) return;

            _isPlaying = false;
            FinishedEvent();
        }

        /// <summary>
        /// Updates the game by processing user input.
        /// </summary>
        /// <param name="deltaTime"></param>
        public void Update(float deltaTime)
        {
            if (!_isPlaying)
            {
                return;
            }

            _input.Update();

            var action = _input?.GetPlayerAction();
            if (action.HasValue)
            {
                HandlePlayerAction(action.Value);
            }
            else if (_nextAction.HasValue)
            {
                HandlePlayerAction(_nextAction.Value);
                _nextAction = null;
            }
            else
            {
                HandleAutomaticPieceFalling(deltaTime);
            }
        }

        private void HandleAutomaticPieceFalling(float deltaTime)
        {
            _elapsedTime += deltaTime;
            if (!(_elapsedTime >= Level.FallDelay)) return;

            if (!_board.MovePieceDown())
            {
                PieceFinishedFalling();
            }
            ResetElapsedTime();
        }

        private void HandlePlayerAction(PlayerAction action)
        {
            var pieceMoved = false;
            switch (action)
            {
                case PlayerAction.MoveLeft:
                    pieceMoved = _board.MovePieceLeft();
                    break;

                case PlayerAction.MoveRight:
                    pieceMoved = _board.MovePieceRight();
                    break;

                case PlayerAction.MoveDown:
                    ResetElapsedTime();
                    if (_board.MovePieceDown())
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
                    var didRotate = _board.RotatePiece();
                    if (didRotate)
                    {
                        PieceRotatedEvent();
                    }

                    break;

                case PlayerAction.Fall:
                    Score.PieceFinishedFalling(_board.FallPiece());
                    ResetElapsedTime();
                    PieceFinishedFalling();
                    break;
            }
            if (pieceMoved)
            {
                PieceMovedEvent();
            }
        }

        private void PieceFinishedFalling()
        {
            PieceFinishedFallingEvent();
            var rowsCount = _board.RemoveFullRows();
            Score.RowsCleared(rowsCount);
            Level.RowsCleared(rowsCount);
            AddPiece();
        }

        private void ResetElapsedTime()
        {
            _elapsedTime = 0;
        }
    }
}
