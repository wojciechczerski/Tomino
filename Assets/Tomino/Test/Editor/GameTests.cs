using NUnit.Framework;
using Tomino.Model;
using Tomino.Test.Editor.Helper;

namespace Tomino.Test.Editor
{
    public class GameTests
    {
        private StubInput _input;
        private StubPieceProvider _pieceProvider;
        private Board _board;
        private Game _game;

        [SetUp]
        public void Initialize()
        {
            _input = new StubInput();
            _pieceProvider = new StubPieceProvider();
            _board = new Board(10, 20, _pieceProvider);
            _game = new Game(_board, _input);
            _game.Start();
        }

        [Test]
        public void CreatesNewPieceWhenTheGameStarts()
        {
            Assert.IsNotEmpty(_board.Blocks);
        }

        [Test]
        public void IgnoresInputWhenGameIsPaused()
        {
            var callbackCalled = false;
            _game.PieceFinishedFallingEvent += delegate
            {
                callbackCalled = true;
            };

            _game.Pause();
            UpdateGameWithAction(PlayerAction.Fall);

            Assert.IsFalse(callbackCalled);

            _game.Resume();
            UpdateGameWithAction(PlayerAction.Fall);
            Assert.IsTrue(callbackCalled);
        }

        [TestCase(PlayerAction.MoveLeft, 0, -1)]
        [TestCase(PlayerAction.MoveRight, 0, 1)]
        [TestCase(PlayerAction.MoveDown, -1, 0)]
        public void MovesPiece(PlayerAction action, int rowOffset, int columnOffset)
        {
            var positions = _board.GetBlockPositions();

            UpdateGameWithAction(action);

            foreach (var block in _board.Blocks)
            {
                var start = positions[block];
                var end = block.Position;
                Assert.AreEqual(end.Row, start.Row + rowOffset);
                Assert.AreEqual(end.Column, start.Column + columnOffset);
            }
        }

        [Test]
        public void MovesPieceDownWhenUpdating()
        {
            var positions = _board.GetBlockPositions();
            _game.Update(10);

            foreach (var block in _board.Blocks)
            {
                Assert.AreEqual(block.Position.Row, positions[block].Row - 1);
            }
        }

        [Test]
        public void RotatesPiece()
        {
            var secondBlockPositions = new Position[]
            {
                new(2, 1),
                new(1, 2),
                new(0, 1),
                new(1, 0),
                new(2, 1)
            };

            _pieceProvider = new StubPieceProvider(StubPieceType.TwoBlocks);
            _board = new Board(3, 3, _pieceProvider);
            _game = new Game(_board, _input);
            _game.Start();

            for (var i = 1; i < secondBlockPositions.Length; ++i)
            {
                UpdateGameWithAction(PlayerAction.Rotate);
                var secondBlock = _board.Blocks[1];

                Assert.AreEqual(secondBlockPositions[i].Row, secondBlock.Position.Row);
                Assert.AreEqual(secondBlockPositions[i].Column, secondBlock.Position.Column);
            }
        }

        [Test]
        public void HandlesDirectlyConfiguredPlayerInput()
        {
            var positions = _board.GetBlockPositions();
            _game.SetNextAction(PlayerAction.MoveRight);
            _game.Update(10);

            foreach (var block in _board.Blocks)
            {
                var start = positions[block];
                var end = block.Position;
                Assert.AreEqual(end.Row, start.Row);
                Assert.AreEqual(end.Column, start.Column + 1);
            }
        }

        [Test]
        public void AddsNewPieceAfterCurrentPieceFallsDown()
        {
            var blocksCount = _board.Blocks.Count;

            UpdateGameWithAction(PlayerAction.Fall);

            blocksCount += _pieceProvider.GetPiece().blocks.Length;

            Assert.AreEqual(blocksCount, _board.Blocks.Count);
        }

        [TestCase(PlayerAction.MoveLeft, false)]
        [TestCase(PlayerAction.MoveRight, false)]
        [TestCase(PlayerAction.MoveLeft, true)]
        [TestCase(PlayerAction.MoveRight, true)]
        public void HandlesCollisions(PlayerAction action, bool blockCollision)
        {
            if (blockCollision)
            {
                for (var row = 0; row < _board.height; ++row)
                {
                    var leftPosition = new Position(row, 0);
                    var rightPosition = new Position(row, _board.width - 1);
                    _board.Blocks.Add(new Block(leftPosition, PieceType.I));
                    _board.Blocks.Add(new Block(rightPosition, PieceType.I));
                }
            }

            for (var i = 0; i < 50; ++i)
            {
                UpdateGameWithAction(action);
                UpdateGameWithAction(PlayerAction.Rotate);
            }

            Assert.IsFalse(_board.HasCollisions());
        }

        [Test]
        public void RemovesFullRows()
        {
            _board.AddFullRows(_board.height / 2);
            var blocksCount = _pieceProvider.GetPiece().blocks.Length;
            UpdateGameWithAction(PlayerAction.Fall);
            blocksCount += _pieceProvider.GetPiece().blocks.Length;

            Assert.AreEqual(blocksCount, _board.Blocks.Count);
        }

        [Test]
        public void FiresAnEventWhenTheGameFinishes()
        {
            var spy = new GameEventSpy();
            _game.FinishedEvent += spy.OnGameFinished;

            for (var i = 0; i < _board.FallDistance() + 1; ++i)
            {
                UpdateGameWithAction(PlayerAction.Fall);
            }

            Assert.IsTrue(spy.gameFinishedCalled);
        }

        [TestCase(1, 100)]
        [TestCase(2, 300)]
        [TestCase(3, 500)]
        [TestCase(4, 800)]
        public void UpdatesScoreWhenRowsAreCleared(int fullRowsCount, int score)
        {
            _board.AddFullRows(fullRowsCount);
            _game.WaitUntilPieceFallsAutomatically();

            Assert.AreEqual(score, _game.Score.Value);
        }

        [Test]
        public void UpdatesScoreWhenPieceFalls()
        {
            var distance = _board.FallDistance();
            UpdateGameWithAction(PlayerAction.Fall);

            Assert.AreEqual(distance * 2, _game.Score.Value);
        }

        [TestCase(5, 1)]
        [TestCase(10, 2)]
        [TestCase(15, 2)]
        [TestCase(20, 3)]
        public void UpdatesLevelAfterClearingRows(int numRowsCleared, int expectedLevel)
        {
            for (var i = 0; i < numRowsCleared; ++i)
            {
                _board.AddFullRows(1);
                UpdateGameWithAction(PlayerAction.Fall);
            }

            Assert.AreEqual(expectedLevel, _game.Level.Number);
        }

        [Test]
        public void IncreasesPieceFallingSpeedAfterAdvancingToTheNextLevel()
        {
            var initialFallDelay = _game.Level.FallDelay;

            _board.AddFullRows(10);
            UpdateGameWithAction(PlayerAction.Fall);

            Assert.Less(_game.Level.FallDelay, initialFallDelay);
        }

        private void UpdateGameWithAction(PlayerAction action)
        {
            _input.action = action;
            _game.Update(0);
        }
    }
}
