namespace Tomino
{
    class NotRotatingPiece : Piece
    {
        public NotRotatingPiece(Position[] blockPositions, Type type)
            : base(blockPositions, type)
        {

        }

        public override void Rotate()
        {

        }
    }
}
