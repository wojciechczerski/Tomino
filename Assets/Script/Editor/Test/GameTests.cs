using NUnit.Framework;
using Tomino;
using System.Collections.Generic;

public class GameTests
{
    StubInput input;
    Game game;

    [SetUp]
    public void Initialize()
    {
        input = new StubInput();
        game = new Game(input);
        game.Start();
    }

    [Test]
    public void CreatesNewPieceWhenTheGameStarts()
    {
        Assert.IsNotNull(game.piece);
    }

    [TestCase(PlayerAction.MoveLeft, 0, -1)]
    [TestCase(PlayerAction.MoveRight, 0, 1)]
    [TestCase(PlayerAction.MoveDown, -1, 0)]
    public void MovesPiece(PlayerAction action, int rowOffset, int columnOffset)
    {
        var positions = game.piece.GetPositions();
        UpdateGameWithAction(action);

        foreach (Block block in game.piece.blocks)
        {
            var start = positions[block];
            var end = block.position;
            Assert.AreEqual(end.row, start.row + rowOffset);
            Assert.AreEqual(end.column, start.column + columnOffset);
        }
    }

    [Test]
    public void MovesPieceDownWhenUpdating()
    {
        var positions = game.piece.GetPositions();
        game.Update(10);

        foreach (Block block in game.piece.blocks)
        {
            Assert.AreEqual(block.position.row, positions[block].row - 1);
        }
    }

    [Test]
    public void RotatesPiece()
    {
        var testCases = new Dictionary<Piece, Position[][]>()
        {
            { AvailablePieces.IPiece(), PieceRotations.IPiece() },
            { AvailablePieces.TPiece(), PieceRotations.TPiece() },
            { AvailablePieces.OPiece(), PieceRotations.OPiece() },
            { AvailablePieces.SPiece(), PieceRotations.SPiece() },
            { AvailablePieces.ZPiece(), PieceRotations.ZPiece() },
            { AvailablePieces.JPiece(), PieceRotations.JPiece() },
            { AvailablePieces.LPiece(), PieceRotations.LPiece() }
        };

        foreach (var testCase in testCases)
        {
            var piece = testCase.Key;
            var rotationPositions = testCase.Value;

            game = new Game(input);
            game.AddPiece(piece);

            foreach (Position[] positions in rotationPositions)
            {
                UpdateGameWithAction(PlayerAction.Rotate);

                for (int i = 0; i < piece.blocks.Length; ++i)
                {
                    var block = piece.blocks[i];
                    var offset = game.initialPosition;
                    var expectedRow = positions[i].row + offset.row;
                    var expectedColumn = positions[i].column + offset.column;

                    Assert.AreEqual(expectedRow, block.position.row);
                    Assert.AreEqual(expectedColumn, block.position.column);
                }
            }
        }
    }

    [Test]
    public void AddsNewPieceAfterCurrentPieceFallsDown()
    {
        var blocksCount = game.piece.blocks.Length;

        UpdateGameWithAction(PlayerAction.Fall);

        blocksCount += game.piece.blocks.Length;

        Assert.AreEqual(blocksCount, game.board.blocks.Count);
    }

    [TestCase(PlayerAction.MoveLeft, false)]
    [TestCase(PlayerAction.MoveRight, false)]
    [TestCase(PlayerAction.MoveLeft, true)]
    [TestCase(PlayerAction.MoveRight, true)]
    public void HandlesCollisions(PlayerAction action, bool blockCollision)
    {
        if (blockCollision)
        {
            for (int row = 0; row < game.board.height; ++row)
            {
                var leftPosition = new Position(row, 0);
                var rightPostion = new Position(row, game.board.width - 1);
                game.board.blocks.Add(new Block(leftPosition, Piece.Type.I));
                game.board.blocks.Add(new Block(rightPostion, Piece.Type.I));
            }
        }

        for (int i = 0; i < 50; ++i)
        {
            UpdateGameWithAction(action);
            UpdateGameWithAction(PlayerAction.Rotate);
        }

        Assert.IsFalse(game.board.HasCollisions());
    }

    [Test]
    public void RemovesFullRows()
    {
        for (int row = 0; row < game.board.height / 2; ++row)
        {
            for (int column = 0; column < game.board.width; ++column)
            {
                var position = new Position(row, column);
                game.board.blocks.Add(new Block(position, Piece.Type.I));
            }
        }

        var blocksCount = game.piece.blocks.Length;
        UpdateGameWithAction(PlayerAction.Fall);
        blocksCount += game.piece.blocks.Length;

        Assert.AreEqual(blocksCount, game.board.blocks.Count);
    }

    private void UpdateGameWithAction(PlayerAction action)
    {
        input.action = action;
        game.Update(0);
    }
}
