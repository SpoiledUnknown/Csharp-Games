using ChessLogic.Moves;

namespace ChessLogic.Pieces
{
    public class Bishop : Piece
    {
        public override PieceType Type => PieceType.Bishop;

        public override Player Color { get; }

        private static readonly Direction[] directions = new Direction[]
        {
            Direction.NorthEast,
            Direction.NorthWest, 
            Direction.SouthEast, 
            Direction.SouthWest
        };

        public Bishop(Player color)
        {
            Color = color;
        }

        public override Piece Copy()
        {
            Bishop copy = new Bishop(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return MovePositionsInDirection(from, board, directions).Select(to => new NormalMove(from, to));
        }
    }
}
