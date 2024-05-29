using ChessLogic.Moves;

namespace ChessLogic.Pieces
{
    public class Queen : Piece
    {
        public override PieceType Type => PieceType.Queen;

        public override Player Color { get; }

        private static readonly Direction[] directions = new Direction[]
        {
            Direction.North,
            Direction.South,
            Direction.East,
            Direction.West,
            Direction.NorthWest,
            Direction.NorthEast,
            Direction.SouthWest,
            Direction.SouthEast
        };

        public Queen(Player color)
        {
            Color = color;
        }

        public override Piece Copy()
        {
            Queen copy = new Queen(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return MovePositionsInDirection(from, board, directions).Select(to => new NormalMove(from, to));
        }
    }
}
