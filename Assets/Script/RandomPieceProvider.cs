using System;

namespace Tomino
{
    public class RandomPieceProvider : IPieceProvider
    {
        Random random = new Random();

        public Piece GetPiece()
        {
            var allPieces = AvailablePieces.All();
            var index = random.Next(allPieces.Length);
            return allPieces[index];
        }
    }
}
