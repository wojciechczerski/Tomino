using System.Linq;
using NUnit.Framework;
using Tomino.Model;
using Tomino.Test.Editor.Helper;

namespace Tomino.Test.Editor
{
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

            Assert.AreEqual(shadow.Count, 1);
            Assert.AreEqual(shadow.ElementAt(0), new Position(0, 1));
        }

        private static Board CreateEmptyBoard()
        {
            return new Board(3, 3, new StubPieceProvider());
        }
    }
}
