namespace Snake_Game.src
{
    public class GameState
    {
        #nullable disable
        public int Rows { get; }
        public int Columns { get; }
        public GridValue[,] Grid { get; }
        public Direction Direction { get; private set; }
        public int Score { get; private set; }
        public bool GameOver { get; private set; }

        private readonly LinkedList<Direction> _directionChanges = new LinkedList<Direction>();
        private readonly LinkedList<Position> _snakePositions = new LinkedList<Position>();
        private readonly Random _random = new Random();

        public GameState(int rows, int columns)
        {
            Rows = rows; 
            Columns = columns;
            Grid = new GridValue[Rows, Columns];
            Direction = Direction.Right;

            AddSnake();
            AddFood();
        }

        private void AddSnake()
        {
            int r = Rows / 2;
            for (int c = 1; c <= 3; c++)
            {
                Grid[r, c] = GridValue.Snake;
                _snakePositions.AddFirst(new Position(r, c));
            }
        }

        private IEnumerable<Position> EmptyPositions()
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    if (Grid[r, c] == GridValue.Empty)
                    {
                        yield return new Position(r, c);
                    }
                }
            }
        }

        private void AddFood()
        {
            List<Position> empty = new List<Position>(EmptyPositions());

            if (empty.Count == 0) return;

            Position pos = empty[_random.Next(empty.Count)];
            Grid[pos.Row, pos.Column] = GridValue.Food;
        }

        public Position HeadPosition() => _snakePositions.First.Value;
        public Position TailPosition() => _snakePositions.Last.Value;

        public IEnumerable<Position> SnakePositions() => _snakePositions;

        private void AddHead(Position pos)
        {
            _snakePositions.AddFirst(pos);
            Grid[pos.Row, pos.Column] = GridValue.Snake;
        }

        private void RemoveTail()
        {
            Position tail = _snakePositions.Last.Value;
            Grid[tail.Row, tail.Column] = GridValue.Empty;
            _snakePositions.RemoveLast();
        }

        private Direction GetLastDirection()
        {
            if (_directionChanges.Count == 0)
                return Direction;

            return _directionChanges.Last.Value;
        }

        private bool CanChangeDir(Direction newDir)
        {
            if (_directionChanges.Count == 2)
                return false;

            Direction lastDir = GetLastDirection();
            return newDir != lastDir && newDir != lastDir.Opposite();
        }

        public void ChangeDirection(Direction dir)
        {
            if (CanChangeDir(dir))
            {
                _directionChanges.AddLast(dir);
            }
        }

        private bool OutsideGrid(Position pos) => pos.Row < 0 || pos.Row >= Rows || pos.Column < 0 || pos.Column >= Columns;

        private GridValue WillHit(Position newHeadPos)
        {
            if (OutsideGrid(newHeadPos))
                return GridValue.Outside;

            if (newHeadPos == TailPosition())
                return GridValue.Empty;

            return Grid[newHeadPos.Row, newHeadPos.Column];
        }

        public void Move()
        {
            if (_directionChanges.Count > 0)
            {
                Direction = _directionChanges.First.Value;
                _directionChanges.RemoveFirst();
            }

            Position newHeadPos = HeadPosition().Translate(Direction);
            GridValue hit = WillHit(newHeadPos);

            if (hit == GridValue.Outside || hit == GridValue.Snake)
            {
                GameOver = true;
            }
            else if (hit == GridValue.Empty)
            {
                RemoveTail();
                AddHead(newHeadPos);
            }
            else if (hit == GridValue.Food)
            {
                AddHead(newHeadPos);
                Score++;
                AddFood();
            }
        }
    }
}
