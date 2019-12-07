using NUnit.Framework;
using Tomino;

public class BoardTests
{
    [Test]
    public void ComputesHashCode()
    {
        var first = CreateEmptyBoard();
        var second = CreateEmptyBoard();

        Assert.AreEqual(first.GetHashCode(), second.GetHashCode());

        first.AddPiece();
        Assert.AreNotEqual(first.GetHashCode(), second.GetHashCode());

        second.AddPiece();
        Assert.AreEqual(first.GetHashCode(), second.GetHashCode());

        first.RemoveAllBlocks();
        Assert.AreNotEqual(first.GetHashCode(), second.GetHashCode());

        second.RemoveAllBlocks();
        Assert.AreEqual(first.GetHashCode(), second.GetHashCode());
    }

    [Test]
    public void ComputesPieceShadow()
    {
        var board = new Board(3, 3, new StubPieceProvider());
        board.AddPiece();
        var shadow = board.GetPieceShadow();

        Assert.AreEqual(shadow.Length, 1);
        Assert.AreEqual(shadow[0], new Position(0, 1));
    }

    public void ComputesNextGeneratedPiece()
    {
        var board = new Board(20, 20, new BalancedRandomPieceProvider());
        var nextPiece = board.nextPiece;

        board.AddPiece();

        Assert.AreEqual(board.piece.GetType(), nextPiece.GetType());
    }

    Board CreateEmptyBoard() => new Board(3, 3, new StubPieceProvider());
}
