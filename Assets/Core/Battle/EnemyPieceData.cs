public class EnemyPieceData
{
    private readonly PieceType _type;
    private readonly BoardPosition _position;

    public PieceType Type => _type;
    public BoardPosition Position => _position;

    public EnemyPieceData(PieceType type, BoardPosition position)
    {
        _type = type;
        _position = position;
    }
}