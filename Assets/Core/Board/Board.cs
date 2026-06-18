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

    public bool IsOccupied(BoardPosition position)
    {
        BoardCell cell = GetCell(position);

        if (cell == null)
        {
            return false;
        }

        return cell.IsOccupied();
    }

    public Piece GetPiece(BoardPosition position)
    {
        BoardCell cell = GetCell(position);

        if (cell == null)
        {
            return null;
        }

        return cell.Piece;
    }

    public bool PlacePiece(Piece piece, BoardPosition position)
    {
        if (piece == null)
        {
            return false;
        }

        BoardCell cell = GetCell(position);

        if (cell == null)
        {
            return false;
        }

        if (!cell.IsWalkable())
        {
            return false;
        }

        if (cell.IsOccupied())
        {
            return false;
        }

        cell.SetPiece(piece);
        piece.MoveTo(position);

        return true;
    }

    public Piece RemovePiece(BoardPosition position)
    {
        BoardCell cell = GetCell(position);

        if (cell == null)
        {
            return null;
        }

        Piece piece = cell.Piece;

        if (piece == null)
        {
            return null;
        }

        cell.ClearPiece();

        return piece;
    }

    public bool MovePiece(BoardPosition from, BoardPosition to)
    {
        BoardCell fromCell = GetCell(from);
        BoardCell toCell = GetCell(to);

        if (fromCell == null || toCell == null)
        {
            return false;
        }

        if (!fromCell.IsOccupied())
        {
            return false;
        }

        if (!toCell.IsWalkable())
        {
            return false;
        }

        if (toCell.IsOccupied())
        {
            return false;
        }

        Piece piece = fromCell.Piece;

        fromCell.ClearPiece();
        toCell.SetPiece(piece);
        piece.MoveTo(to);

        return true;
    }
}