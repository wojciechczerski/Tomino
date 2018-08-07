using NUnit.Framework;
using Tomino;

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
        Assert.IsNotEmpty(board.Blocks);
    }

    [TestCase(PlayerAction.MoveLeft, 0, -1)]
    [TestCase(PlayerAction.MoveRight, 0, 1)]
    [TestCase(PlayerAction.MoveDown, -1, 0)]
    public void MovesPiece(PlayerAction action, int rowOffset, int columnOffset)
    {
        var positions = board.GetBlockPositions();

        UpdateGameWithAction(action);

        foreach (Block block in board.Blocks)
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
        var positions = board.GetBlockPositions();
        game.Update(10);

        foreach (Block block in board.Blocks)
        {
            Assert.AreEqual(block.Position.Row, positions[block].Row - 1);
        }
    }

    [Test]
    public void RotatesPiece()
    {
        var secondBlockPositions = new Position[]
        {
            new Position(2, 1),
            new Position(1, 2),
            new Position(0, 1),
            new Position(1, 0),
            new Position(2, 1),
        };

        board = new Board(3, 3);
        pieceProvider = new StubPieceProvider();
        game = new Game(board, input, pieceProvider);
        game.Start();

        for (var i = 1; i < secondBlockPositions.Length; ++i)
        {
            UpdateGameWithAction(PlayerAction.Rotate);
            var secondBlock = board.Blocks[1];

            Assert.AreEqual(secondBlockPositions[i].Row, secondBlock.Position.Row);
            Assert.AreEqual(secondBlockPositions[i].Column, secondBlock.Position.Column);
        }
    }

    [Test]
    public void AddsNewPieceAfterCurrentPieceFallsDown()
    {
        var blocksCount = board.Blocks.Count;

        UpdateGameWithAction(PlayerAction.Fall);

        blocksCount += pieceProvider.piece.blocks.Length;

        Assert.AreEqual(blocksCount, board.Blocks.Count);
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
                board.Blocks.Add(new Block(leftPosition, Piece.Type.I));
                board.Blocks.Add(new Block(rightPostion, Piece.Type.I));
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
                board.Blocks.Add(new Block(position, Piece.Type.I));
            }
        }

        var blocksCount = pieceProvider.piece.blocks.Length;
        UpdateGameWithAction(PlayerAction.Fall);
        blocksCount += pieceProvider.piece.blocks.Length;

        Assert.AreEqual(blocksCount, board.Blocks.Count);
    }

    void UpdateGameWithAction(PlayerAction action)
    {
        input.action = action;
        game.Update(0);
    }
}
