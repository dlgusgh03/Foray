public class RunManager
{
    private const int NormalBattleWinGold = 5;
    private const int BossBattleWinGold = 10;
    private const int InterestGoldUnit = 5;
    private const int MaxInterestGold = 5;
    private const int BossStageInterval = 3;

    private PlayerArmy _playerArmy;
    private int _stageIndex;
    private int _gold;
    private int _bossClearCount;
    private int _populationPrice;
    private bool _isRunOver;
    private RunState _currentState;

    private Board _currentBoard;
    private BattleManager _currentBattleManager;
    private Shop _currentShop;

    public PlayerArmy PlayerArmy => _playerArmy;
    public int StageIndex => _stageIndex;
    public int Gold => _gold;
    public int BossClearCount => _bossClearCount;
    public int PopulationPrice => _populationPrice;
    public bool IsRunOver => _isRunOver;
    public RunState CurrentState => _currentState;
    public Board CurrentBoard => _currentBoard;
    public BattleManager CurrentBattleManager => _currentBattleManager;
    public Shop CurrentShop => _currentShop;

    public RunManager()
    {
        _playerArmy = new PlayerArmy();
        _stageIndex = 1;
        _gold = 0;
        _bossClearCount = 0;
        _populationPrice = 5;
        _isRunOver = false;
        _currentState = RunState.None;
        _currentBoard = null;
        _currentBattleManager = null;
        _currentShop = null;
    }

    public void StartRun()
    {
        _playerArmy = new PlayerArmy();
        _stageIndex = 1;
        _gold = 0;
        _bossClearCount = 0;
        _populationPrice = 5;
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
        if (IsBossStage())
        {
            ApplyBossBattleReward();
            _bossClearCount++;
        }
        else
        {
            ApplyNormalBattleReward();
        }

        _stageIndex++;
        _currentShop = new Shop(this);
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

        _currentShop = null;
        StartBattle();
    }

    public void LeaveShop()
    {
        StartNextBattle();
    }

    private MapPreset SelectMapPreset()
    {
        return MapPreset.CreateSquare(7);
    }

    private EnemyPreset SelectEnemyPreset()
    {
        return EnemyPreset.CreateEasySoldiers();
    }

    public void AddGold(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        _gold += amount;
    }

    public bool CanSpendGold(int amount)
    {
        if (amount < 0)
        {
            return false;
        }

        return _gold >= amount;
    }

    public bool SpendGold(int amount)
    {
        if (!CanSpendGold(amount))
        {
            return false;
        }

        _gold -= amount;
        return true;
    }

    public int CalculateInterest()
    {
        int interest = _gold / InterestGoldUnit;

        if (interest > MaxInterestGold)
        {
            interest = MaxInterestGold;
        }

        return interest;
    }

    public int ApplyInterest()
    {
        int interest = CalculateInterest();
        AddGold(interest);
        return interest;
    }

    public void ApplyNormalBattleReward()
    {
        AddGold(NormalBattleWinGold);
        ApplyInterest();
    }

    public void ApplyBossBattleReward()
    {
        AddGold(BossBattleWinGold);
        ApplyInterest();
    }

    private bool IsBossStage()
    {
        return _stageIndex % BossStageInterval == 0;
    }

    public void IncreasePopulationPrice(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        _populationPrice += amount;
    }

#if UNITY_EDITOR
    public void DebugForceBattleWin()
    {
        HandleBattleWin();
    }
#endif
}