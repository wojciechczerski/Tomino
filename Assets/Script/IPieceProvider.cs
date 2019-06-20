namespace Tomino
{
    public interface IPieceProvider
    {
        Piece GetPiece();
        Piece GetNextPiece();
    }
}
