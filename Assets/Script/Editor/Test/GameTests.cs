using NUnit.Framework;
using Tomino;
using System.Collections.Generic;

public class GameTests
{
    StubInput input;
    StubPieceProvider pieceProvider;
    Board board;
    Game game;

    [SetUp]
    public void Initialize()
    {
        input = new StubInput();
        pieceProvider = new StubPieceProvider();
        board = new Board(10, 20);
        game = new Game(board, input, pieceProvider);
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
    public void RotatesPice()
    {
        var secondBlockPositions = new Position[]
        {
            new Position(2, 1),
            new Position(1, 2),
            new Position(0, 1),
            new Position(1, 0),
            new Position(2, 1),
        };

        var blockPositions = new Position[] { new Position(0, 0), new Position(1, 0) };
        pieceProvider.piece = new Piece(blockPositions, Piece.Type.I);

        board = new Board(3, 3);
        game = new Game(board, input, pieceProvider);
        game.Start();

        for (var i = 1; i < secondBlockPositions.Length; ++i)
        {
            UpdateGameWithAction(PlayerAction.Rotate);
            var secondBlock = board.blocks[1];

            Assert.AreEqual(secondBlockPositions[i].row, secondBlock.position.row);
            Assert.AreEqual(secondBlockPositions[i].column, secondBlock.position.column);
        }
    }

    [Test]
    public void AddsNewPieceAfterCurrentPieceFallsDown()
    {
        var blocksCount = game.piece.blocks.Length;

        UpdateGameWithAction(PlayerAction.Fall);

        blocksCount += game.piece.blocks.Length;

        Assert.AreEqual(blocksCount, board.blocks.Count);
    }

    [TestCase(PlayerAction.MoveLeft, false)]
    [TestCase(PlayerAction.MoveRight, false)]
    [TestCase(PlayerAction.MoveLeft, true)]
    [TestCase(PlayerAction.MoveRight, true)]
    public void HandlesCollisions(PlayerAction action, bool blockCollision)
    {
        if (blockCollision)
        {
            for (int row = 0; row < board.height; ++row)
            {
                var leftPosition = new Position(row, 0);
                var rightPostion = new Position(row, board.width - 1);
                board.blocks.Add(new Block(leftPosition, Piece.Type.I));
                board.blocks.Add(new Block(rightPostion, Piece.Type.I));
            }
        }

        for (int i = 0; i < 50; ++i)
        {
            UpdateGameWithAction(action);
            UpdateGameWithAction(PlayerAction.Rotate);
        }

        Assert.IsFalse(board.HasCollisions());
    }

    [Test]
    public void RemovesFullRows()
    {
        for (int row = 0; row < board.height / 2; ++row)
        {
            for (int column = 0; column < board.width; ++column)
            {
                var position = new Position(row, column);
                board.blocks.Add(new Block(position, Piece.Type.I));
            }
        }

        var blocksCount = game.piece.blocks.Length;
        UpdateGameWithAction(PlayerAction.Fall);
        blocksCount += game.piece.blocks.Length;

        Assert.AreEqual(blocksCount, board.blocks.Count);
    }

    private void UpdateGameWithAction(PlayerAction action)
    {
        input.action = action;
        game.Update(0);
    }
}
