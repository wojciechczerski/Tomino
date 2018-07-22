using System;

namespace Tomino
{
    public static class AvailablePieces
    {
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

        public static Piece OPiece()
        {
            var positions = new Position[] {
                new Position(0, 0),
                new Position(1, 0),
                new Position(1, 1),
                new Position(0, 1)
            };
            return new NotRotatingPiece(positions, Piece.Type.O);
        }

        public static Piece TPiece()
        {
            var positions = new Position[] {
                new Position(0, 0),
                new Position(1, 0),
                new Position(-1, 0),
                new Position(0, 1)
            };
            return new Piece(positions, Piece.Type.T);
        }

        public static Piece SPiece()
        {
            var positions = new Position[] {
                new Position(0, 0),
                new Position(1, 0),
                new Position(0, 1),
                new Position(-1, 1)
            };
            return new Piece(positions, Piece.Type.S);
        }

        public static Piece ZPiece()
        {
            var positions = new Position[] {
                new Position(0, 0),
                new Position(-1, 0),
                new Position(0, 1),
                new Position(1, 1)
            };
            return new Piece(positions, Piece.Type.Z);
        }

        public static Piece JPiece()
        {
            var positions = new Position[] {
                new Position(0, 0),
                new Position(1, 0),
                new Position(-1, 0),
                new Position(-1, -1)
            };
            return new Piece(positions, Piece.Type.J);
        }

        public static Piece LPiece()
        {
            var positions = new Position[] {
                new Position(0, 0),
                new Position(1, 0),
                new Position(-1, 0),
                new Position(-1, 1)
            };
            return new Piece(positions, Piece.Type.L);
        }

        public static Piece IPiece()
        {
            var positions = new Position[] {
                new Position(0, 0),
                new Position(1, 0),
                new Position(-1, 0),
                new Position(-2, 0)
            };
            return new Piece(positions, Piece.Type.I);
        }
    }
}
