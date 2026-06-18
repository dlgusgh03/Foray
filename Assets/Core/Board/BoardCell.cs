public class BoardCell
{
    private BoardPosition _position;
    private bool _isBlocked;
    private Piece _piece;


    public BoardPosition Position => _position;
    public bool IsBlocked => _isBlocked;
    public Piece Piece => _piece;

    public BoardCell(BoardPosition position)
    {
        _position = position;
        _isBlocked = false;
        _piece = null;
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

    public bool IsOccupied()
    {
        return _piece != null;
    }

    public void SetPiece(Piece piece)
    {
        _piece = piece;
    }

    public void ClearPiece()
    {
        _piece = null;
    }

    public override string ToString()
    {
        string pieceText = _piece == null ? "Empty" : _piece.ToString();
        return $"Cell {_position}, Blocked: {_isBlocked}, Piece: {pieceText}";
    }
}