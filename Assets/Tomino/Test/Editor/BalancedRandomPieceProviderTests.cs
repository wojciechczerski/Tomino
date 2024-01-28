using System;
using System.Collections.Generic;
using NUnit.Framework;
using Tomino.Model;

namespace Tomino.Test.Editor
{
    public class BalancedRandomPieceProviderTests
    {
        [Test]
        public void GeneratesRandomPiecesWithSimilarProbability()
        {
            const int sampleSize = 1000;
            var provider = new BalancedRandomPieceProvider();
            var pieceCount = new Dictionary<PieceType, int>();

            for (var i = 0; i < sampleSize; i++)
            {
                var pieceType = provider.GetPiece().Type;

                if (!pieceCount.TryAdd(pieceType, 1))
                {
                    pieceCount[pieceType] += 1;
                }
            }

            var averageCount = sampleSize / (float)AvailablePieces.All().Length;
            foreach (float count in pieceCount.Values)
            {
                var difference = (count - averageCount) / averageCount;
                Assert.True(Math.Abs(difference) < 0.05f);
            }
        }
    }
}
