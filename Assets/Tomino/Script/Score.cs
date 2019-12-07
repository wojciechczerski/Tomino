using System.Collections.Generic;

namespace Tomino
{
    public class Score
    {
        public int Value { get; private set; }

        private Dictionary<int, int> scoreForClearedRows = new Dictionary<int, int>()
        {
            {1, 100}, {2, 300}, {3, 500}, {4, 800}
        };

        public void RowsCleared(int count)
        {
            int valueIncrease = 0;
            scoreForClearedRows.TryGetValue(count, out valueIncrease);
            Value += valueIncrease;
        }

        public void PieceFinishedFalling(int rowsCount) => Value += rowsCount * 2;

        public void PieceMovedDown() => Value++;
    }
}