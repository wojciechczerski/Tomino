using System;
using System.Collections.Generic;

namespace Tomino
{
    public class BalancedRandomPieceProvider : IPieceProvider
    {
        private Random random = new Random();
        private List<int> pool = new List<int>();
        private const int numDuplicates = 4;

        public Piece GetPiece() => AvailablePieces.All()[GetPopulatedPool().TakeFirst()];

        public Piece GetNextPiece() => AvailablePieces.All()[GetPopulatedPool()[0]];

        private List<int> GetPopulatedPool()
        {
            if (pool.Count == 0)
            {
                PopulatePool();
            }
            return pool;
        }

        private void PopulatePool()
        {
            for (var index = 0; index < AvailablePieces.All().Length; ++index)
            {
                pool.Add(index, numDuplicates);
            }
            pool.Shuffle(random);
        }
    }
}