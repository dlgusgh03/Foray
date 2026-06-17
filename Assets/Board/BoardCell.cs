class BoardCell
{
    private BoardPosition _position;
    private bool _isBlocked;

    
    public BoardPosition Position => _position;
    public bool IsBlocked => _isBlocked;

    public BoardCell(BoardPosition position)
    {
        _position = position;
        _isBlocked = false;
    }

    public BoardCell(BoardPosition position, bool isBlocked)
    {
        _position = position;
        _isBlocked = isBlocked;
    }

    public void SetBlocked(bool isBlocked)
    {
        _isBlocked = isBlocked;
    }

    public bool IsWalkable()
    {
        return !_isBlocked;
    }

    public override string ToString()
    {
        return $"Cell {_position}, Blocked: {_isBlocked}";
    }
}