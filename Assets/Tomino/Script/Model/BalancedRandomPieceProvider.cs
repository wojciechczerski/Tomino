using System;
using System.Collections.Generic;
using Tomino.Shared;

namespace Tomino.Model
{
    public class BalancedRandomPieceProvider : IPieceProvider
    {
        private readonly Random _random = new();
        private readonly List<int> _pool = new();
        private const int NumDuplicates = 4;

        public Piece GetPiece()
        {
            return AvailablePieces.All()[GetPopulatedPool().TakeFirst()];
        }

        public Piece GetNextPiece()
        {
            return AvailablePieces.All()[GetPopulatedPool()[0]];
        }

        private List<int> GetPopulatedPool()
        {
            if (_pool.Count == 0)
            {
                PopulatePool();
            }
            return _pool;
        }

        private void PopulatePool()
        {
            for (var index = 0; index < AvailablePieces.All().Length; ++index)
            {
                _pool.Add(index, NumDuplicates);
            }
            _pool.Shuffle(_random);
        }
    }
}
