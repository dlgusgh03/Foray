public class RunManager
{
    private PlayerArmy _playerArmy;
    private int _stageIndex;
    private int _gold;

    private Board _currentBoard;
    private BattleManager _currentBattleManager;

    public PlayerArmy PlayerArmy => _playerArmy;
    public int StageIndex => _stageIndex;
    public int Gold => _gold;
    public Board CurrentBoard => _currentBoard;
    public BattleManager CurrentBattleManager => _currentBattleManager;

    public RunManager()
    {
        _playerArmy = new PlayerArmy();
        _stageIndex = 1;
        _gold = 0;
        _currentBoard = null;
        _currentBattleManager = null;
    }

    public void StartRun()
    {
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
            return;
        }

        _currentBoard = _currentBattleManager.Board;
    }

    private MapPreset SelectMapPreset()
    {
        return MapPreset.CreateOpenField();
    }

    private EnemyPreset SelectEnemyPreset()
    {
        return EnemyPreset.CreateEasySoldiers();
    }
}