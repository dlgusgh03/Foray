using System;

public readonly struct BoardPosition
{
    private readonly int _x;
    private readonly int _y;

    public int X => _x;
    public int Y => _y;

    public BoardPosition(int x, int y)
    {
        _x = x; _y = y;
    }

    public BoardPosition Add(int xOffest, int yOffset)
    {
        return new BoardPosition(_x + xOffest, _y + yOffset);
    }

    public override bool Equals(object obj)
    {
        if(obj is not BoardPosition other)
        {
            return false;
        }

        return _x == other._x && _y == other._y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_x, _y);
    }

    public override string ToString()
    {
        return $"({_x}, {_y})";
    }
}