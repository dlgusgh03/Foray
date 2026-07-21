public class Piece
{
    private PieceType _type;
    private PieceOwner _owner;
    private BoardPosition _position;
    private int? _armyPieceId;
    private bool _isAlive;

    public PieceType Type => _type;
    public PieceOwner Owner => _owner;
    public BoardPosition Position => _position;
    public bool IsAlive => _isAlive;
    public int? ArmyPieceId => _armyPieceId;

    public Piece(PieceType type, PieceOwner owner, BoardPosition position, int? armyPieceId)
    {
        _type = type;
        _owner = owner;
        _position = position;
        _armyPieceId = armyPieceId;
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