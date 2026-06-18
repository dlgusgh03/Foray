class Piece
{
    private PieceType _type;
    private PieceOwner _owner;
    private BoardPosition _position;
    private bool _isAlive;

    public PieceType Type => _type;
    public PieceOwner Owner => _owner;
    public BoardPosition Position => _position;
    public bool IsAlive => _isAlive;

    public Piece(PieceType type, PieceOwner owner, BoardPosition position)
    {
        _type = type;
        _owner = owner;
        _position = position;
        _isAlive = true;
    }

    public void MoveTo(BoardPosition position)
    {
        _position = position;
    }

    public void Kill()
    {
        _isAlive = false;
    }

    public bool IsOwnedBy(PieceOwner owner)
    {
        return _owner == owner;
    }

    public override string ToString()
    {
        return $"{_owner} {_type} at {_position}, Alive: {_isAlive}";
    }
}