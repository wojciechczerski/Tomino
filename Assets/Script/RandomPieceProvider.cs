using System;

namespace Tomino
{
    public class RandomPieceProvider : IPieceProvider
    {
        readonly Random random = new Random();

        public Piece GetPiece()
        {
            var allPieces = AvailablePieces.All();
            var index = random.Next(allPieces.Length);
            return allPieces[index];
        }
    }
}
