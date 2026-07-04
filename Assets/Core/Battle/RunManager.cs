public class RunManager
{
    private const int BattleWinGold = 10;

    private PlayerArmy _playerArmy;
    private int _stageIndex;
    private int _gold;
    private bool _isRunOver;
    private RunState _currentState;

    private Board _currentBoard;
    private BattleManager _currentBattleManager;

    public PlayerArmy PlayerArmy => _playerArmy;
    public int StageIndex => _stageIndex;
    public int Gold => _gold;
    public bool IsRunOver => _isRunOver;
    public RunState CurrentState => _currentState;
    public Board CurrentBoard => _currentBoard;
    public BattleManager CurrentBattleManager => _currentBattleManager;

    public RunManager()
    {
        _playerArmy = new PlayerArmy();
        _stageIndex = 1;
        _gold = 0;
        _isRunOver = false;
        _currentState = RunState.None;
        _currentBoard = null;
        _currentBattleManager = null;
    }

    public void StartRun()
    {
        _playerArmy = new PlayerArmy();
        _stageIndex = 1;
        _gold = 0;
        _isRunOver = false;
        _currentState = RunState.Battle;

        StartBattle();
    }

    private void StartBattle()
    {
        MapPreset mapPreset = SelectMapPreset();
        EnemyPreset enemyPreset = SelectEnemyPreset();

        _currentBattleManager = BattleSetup.CreateBattle(mapPreset, enemyPreset, _playerArmy);

        if (_currentBattleManager == null)
        {
            _currentBoard = null;
            _isRunOver = true;
            _currentState = RunState.GameOver;
            return;
        }

        _currentBoard = _currentBattleManager.Board;
        _currentState = RunState.Battle;
    }

    public void ResolveCurrentBattle()
    {
        if (_currentBattleManager == null)
        {
            return;
        }

        if (!_currentBattleManager.IsBattleOver())
        {
            return;
        }

        PieceOwner? winner = _currentBattleManager.GetWinner();

        if (winner == PieceOwner.Player)
        {
            HandleBattleWin();
            return;
        }

        if (winner == PieceOwner.Enemy)
        {
            HandleBattleLose();
            return;
        }
    }

    private void HandleBattleWin()
    {
        _gold += BattleWinGold;
        _stageIndex++;
        _currentState = RunState.Shop;
    }

    private void HandleBattleLose()
    {
        _isRunOver = true;
        _currentState = RunState.GameOver;
    }

    public void StartNextBattle()
    {
        if (_isRunOver)
        {
            return;
        }

        if (_currentState != RunState.Shop)
        {
            return;
        }

        StartBattle();
    }

    private MapPreset SelectMapPreset()
    {
        return MapPreset.CreateSquare(7);
    }

    private EnemyPreset SelectEnemyPreset()
    {
        return EnemyPreset.CreateEasySoldiers();
    }
}