namespace Tomino
{
    public class OPiece : Piece
    {
        public OPiece() : base(GetPositions(), Color.Green) { }

        static Position[] GetPositions()
        {
            return new Position[] {
                new Position(0, 0),
                new Position(1, 0),
                new Position(1, 1),
                new Position(0, 1)
            };
        }
    }
}
