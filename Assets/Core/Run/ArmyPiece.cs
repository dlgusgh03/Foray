public class ArmyPiece
{
    private PieceType _type;
    private int _instanceId;
    
    public int InstanceId => _instanceId;
    public PieceType Type => _type;

    public ArmyPiece(PieceType type, int instanceId)
    {
        _type = type;
        _instanceId = instanceId;
    }
}
