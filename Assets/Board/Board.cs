public class Board
{
    private readonly int _width;
    private readonly int _height;
    private readonly BoardCell[,] _cells;

    public int Width => _width;
    public int Height => _height;

    public Board(int width, int height)
    {
        _width = width;
        _height = height;
        _cells = new BoardCell[width, height];

        InitializeCells();
    }

    private void InitializeCells()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                BoardPosition position = new BoardPosition(x, y);
                _cells[x, y] = new BoardCell(position);
            }
        }
    }

    public bool IsInside(BoardPosition position)
    {
        return position.X >= 0 &&
               position.X < _width &&
               position.Y >= 0 &&
               position.Y < _height;
    }

    public BoardCell GetCell(BoardPosition position)
    {
        if (!IsInside(position))
        {
            return null;
        }

        return _cells[position.X, position.Y];
    }

    public bool IsWalkable(BoardPosition position)
    {
        BoardCell cell = GetCell(position);

        if (cell == null)
        {
            return false;
        }

        return cell.IsWalkable();
    }

    //public bool IsOccupied(BoardPosition position)
    //{
    //    BoardCell cell = GetCell(position);

    //    if (cell == null)
    //    {
    //        return false;
    //    }

    //    return cell.IsOccupied();
    //}
}