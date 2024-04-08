namespace CheckerBoard
{
    public enum PieceType
    {
        Empty,
        White,
        Red
    }

    public class Piece
    {
        public PieceType Type { get; set; }
    }
}