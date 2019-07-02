namespace Tomino
{
    public class Level
    {
        public int Number => Lines / 10 + 1;

        public int Lines { get; private set; }

        public void RowsCleared(int count) => Lines += count;
    }
}