using System;

namespace Tomino
{
    public static class AvailablePieces
    {
        static Random random = new Random();

        public static Piece[] All()
        {
            return new Piece[] {
                OPiece(),
                TPiece(),
                SPiece(),
                ZPiece(),
                JPiece(),
                LPiece(),
                IPiece()
            };
        }

        public static Piece Random()
        {
            var allPieces = All();
            var index = random.Next(allPieces.Length);
            return allPieces[index];
        }

        public static Piece OPiece()
        {
            var positions = new Position[] {
                new Position(0, 0),
                new Position(1, 0),
                new Position(1, 1),
                new Position(0, 1)
            };
            return new Piece(positions, Color.Green);
        }

        public static Piece TPiece()
        {
            var positions = new Position[] {
                new Position(0, 0),
                new Position(1, 0),
                new Position(-1, 0),
                new Position(0, 1)
            };
            return new Piece(positions, Color.Red);
        }

        public static Piece SPiece()
        {
            var positions = new Position[] {
                new Position(0, 0),
                new Position(1, 0),
                new Position(0, 1),
                new Position(-1, 1)
            };
            return new Piece(positions, Color.Blue);
        }

        public static Piece ZPiece()
        {
            var positions = new Position[] {
                new Position(0, 0),
                new Position(-1, 0),
                new Position(0, 1),
                new Position(1, 1)
            };
            return new Piece(positions, Color.Yellow);
        }

        public static Piece JPiece()
        {
            var positions = new Position[] {
                new Position(0, 0),
                new Position(1, 0),
                new Position(-1, 0),
                new Position(-1, -1)
            };
            return new Piece(positions, Color.Orange);
        }

        public static Piece LPiece()
        {
            var positions = new Position[] {
                new Position(0, 0),
                new Position(1, 0),
                new Position(-1, 0),
                new Position(-1, 1)
            };
            return new Piece(positions, Color.Purple);
        }

        public static Piece IPiece()
        {
            var positions = new Position[] {
                new Position(0, 0),
                new Position(1, 0),
                new Position(-1, 0),
                new Position(-2, 0)
            };
            return new Piece(positions, Color.Brown);
        }
    }
}
