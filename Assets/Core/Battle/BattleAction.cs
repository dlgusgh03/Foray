public class BattleAction
{
    private readonly BoardPosition _from;
    private readonly BoardPosition _to;

    public BoardPosition From => _from;
    public BoardPosition To => _to;

    public BattleAction(BoardPosition from, BoardPosition to)
    {
        _from = from;
        _to = to;
    }

    public override string ToString()
    {
        return $"Action: {_from} -> {_to}";
    }
}