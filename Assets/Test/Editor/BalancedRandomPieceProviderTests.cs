using NUnit.Framework;
using Tomino;
using System.Collections.Generic;
using System;

public class BalancedRandomPieceProviderTests
{
    [Test]
    public void GeneratesRandomPiecesWithSimilarProbability()
    {
        var sampleSize = 1000;
        var provider = new BalancedRandomPieceProvider();
        var pieceCount = new Dictionary<PieceType, int>();

        for (int i = 0; i < sampleSize; i++)
        {
            var pieceType = provider.GetPiece().Type;

            if (pieceCount.ContainsKey(pieceType))
            {
                pieceCount[pieceType] += 1;
            }
            else
            {
                pieceCount[pieceType] = 1;
            }
        }

        var averageCount = (float)sampleSize / (float)AvailablePieces.All().Length;
        foreach (float count in pieceCount.Values)
        {
            var difference = (count - averageCount) / averageCount;
            Assert.True(Math.Abs(difference) < 0.05f);
        }
    }
}