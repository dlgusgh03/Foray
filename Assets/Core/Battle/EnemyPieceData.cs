public class EnemyPieceData
{
    private readonly PieceType _type;
    private readonly BoardPosition _deployPosition;

    public PieceType Type => _type;
    public BoardPosition DeployPosition => _deployPosition;

    public EnemyPieceData(PieceType type, BoardPosition deployPosition)
    {
        _type = type;
        _deployPosition = deployPosition;
    }
}