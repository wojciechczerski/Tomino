namespace Tomino.Model
{
    public static class AvailablePieces
    {
        public static Piece[] All()
        {
            return new[] {
                new Piece(new Position[] {
                    new(0, 0),
                    new(1, 0),
                    new(1, 1),
                    new(0, 1)
                }, PieceType.O, false),
                new Piece(new Position[] {
                    new(0, 0),
                    new(1, 0),
                    new(-1, 0),
                    new(0, 1)
                }, PieceType.T),
                new Piece(new[] {
                    new Position(0, 0),
                    new Position(1, 0),
                    new Position(0, 1),
                    new Position(-1, 1)
                }, PieceType.S),
                new Piece(new[] {
                    new Position(0, 0),
                    new Position(-1, 0),
                    new Position(0, 1),
                    new Position(1, 1)
                }, PieceType.Z),
                new Piece(new[] {
                    new Position(0, 0),
                    new Position(1, 0),
                    new Position(-1, 0),
                    new Position(-1, -1)
                }, PieceType.J),
                new Piece(new[] {
                    new Position(0, 0),
                    new Position(1, 0),
                    new Position(-1, 0),
                    new Position(-1, 1)
                }, PieceType.L),
                new Piece(new[] {
                    new Position(0, 0),
                    new Position(1, 0),
                    new Position(-1, 0),
                    new Position(-2, 0)
                }, PieceType.I)
            };
        }
    }
}
