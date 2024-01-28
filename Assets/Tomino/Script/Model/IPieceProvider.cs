namespace Tomino.Model
{
    public interface IPieceProvider
    {
        Piece GetPiece();
        Piece GetNextPiece();
    }
}
