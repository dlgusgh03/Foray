class BattleManager
{
    private readonly Board _board;
    private TurnManager _turnManager;
    private EnemyAI _enemyAI;

    public TurnManager TurnManager => _turnManager;
    public EnemyAI EnemyAI => _enemyAI;

    public BattleManager(Board board)
    {
        _board = board;
        _turnManager = new TurnManager(board);
        _enemyAI = new EnemyAI(board);
    }

    public bool PlayerAct(BoardPosition from, BoardPosition to)
    {
        if (IsBattleOver())
        {
            return false;
        }

        return _turnManager.TryAct(from, to);
    }
    
    public void EnemyAct()
    {
        if (IsBattleOver())
        {
            return;
        }

        BattleAction action = _enemyAI.DecideAction();

        if (action == null)
        {
            return;
        }

        _turnManager.TryAct(action.From, action.To);
    }

    public bool IsBattleOver()
    {
        return _turnManager.IsBattleOver;
    }

    public PieceOwner? GetWinner()
    {
        return _turnManager.Winner;
    }
}