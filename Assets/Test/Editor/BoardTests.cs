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

        first.Add(CreatePiece());
        Assert.AreNotEqual(first.GetHashCode(), second.GetHashCode());

        second.Add(CreatePiece());
        Assert.AreEqual(first.GetHashCode(), second.GetHashCode());

        first.RemoveAllBlocks();
        Assert.AreNotEqual(first.GetHashCode(), second.GetHashCode());

        second.RemoveAllBlocks();
        Assert.AreEqual(first.GetHashCode(), second.GetHashCode());
    }

    Board CreateEmptyBoard() => new Board(3, 3);

    Piece CreatePiece()
    {
        var positions = new Position[] { new Position(1, 1) };
        return new Piece(positions, Piece.Type.I);
    }
}
