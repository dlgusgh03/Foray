using System.Collections.Generic;
using UnityEngine;

public class RunManager
{
    private const int NormalBattleWinGold = 5;
    private const int BossBattleWinGold = 10;
    private const int InterestGoldUnit = 5;
    private const int MaxInterestGold = 5;
    private const int BossStageInterval = 3;
    private const int MaxAugmentCount = 5;
    private const int MaxCardCount = 3;

    private PlayerArmy _playerArmy;
    private int _stageIndex;
    private int _roundIndex;
    private int _gold;
    private int _bossClearCount;
    private int _populationPrice;
    private bool _isRunOver;
    private RunState _currentState;

    private Board _currentBoard;
    private BattleManager _currentBattleManager;
    private Shop _currentShop;
    private readonly List<PieceType> _unlockedPieceTypes;
    private readonly List<Augment> _currentAugments;
    private readonly List<Augment> _augmentChoices;
    private Augment _pendingAugment;
    private readonly List<TacticalCard> _ownedCards; // 실제로 런 동안 보유 중인 사용 가능한 카드
    private readonly List<TacticalCard> _currentCardChoices; // 지금 개봉한 상자에서 고를 수 있는 카드
    private int _remainingCardSelections; // 이번 상자에서 앞으로 몇 장 더 고를 수 있는지

    public PlayerArmy PlayerArmy => _playerArmy;
    public int StageIndex => _stageIndex;
    public int RoundIndex => _roundIndex;
    public int Gold => _gold;
    public int BossClearCount => _bossClearCount;
    public int PopulationPrice => _populationPrice;
    public bool IsRunOver => _isRunOver;
    public RunState CurrentState => _currentState;
    public Board CurrentBoard => _currentBoard;
    public BattleManager CurrentBattleManager => _currentBattleManager;
    public Shop CurrentShop => _currentShop;
    public List<PieceType> GetUnlockedPieceTypes()
    {
        return new List<PieceType>(_unlockedPieceTypes);
    }
    public List<Augment> GetCurrentAugments()
    {
        return new List<Augment>(_currentAugments);
    }
    public List<Augment> GetAugmentChoices()
    {
        return new List<Augment>(_augmentChoices);
    }
    public Augment PendingAugment => _pendingAugment;
    public List<TacticalCard> GetOwnedCards()
    {
        return new List<TacticalCard>(_ownedCards);
    }

    public List<TacticalCard> GetCurrentCardChoices()
    {
        return new List<TacticalCard>(_currentCardChoices);
    }
    public int RemainingCardSelections => _remainingCardSelections;

    public RunManager()
    {
        _playerArmy = new PlayerArmy();
        _stageIndex = 1;
        _roundIndex = 1;
        _gold = 0;
        _bossClearCount = 0;
        _populationPrice = 5;
        _isRunOver = false;
        _currentState = RunState.None;
        _currentBoard = null;
        _currentBattleManager = null;
        _currentShop = null;
        _unlockedPieceTypes = new List<PieceType>();
        InitializeUnlockedPieces();
        _currentAugments = new List<Augment>();
        InitializeCurrentAugments();
        _augmentChoices = new List<Augment>();
        InitializeAugmentChoices();
        _pendingAugment = null;
        _ownedCards = new List<TacticalCard>();
        InitializeOwnedCards();
        _currentCardChoices = new List<TacticalCard>();
        InitializeCurrentCardChoices();
        _remainingCardSelections = 0;
    }

    public void StartRun()
    {
        _playerArmy = new PlayerArmy();
        _stageIndex = 1;
        _roundIndex = 1;
        _gold = 0;
        _bossClearCount = 0;
        _populationPrice = 5;
        _isRunOver = false;
        _currentState = RunState.Battle;
        InitializeUnlockedPieces();
        InitializeCurrentAugments();
        InitializeAugmentChoices();
        _pendingAugment = null;
        InitializeOwnedCards();
        InitializeCurrentCardChoices();
        _remainingCardSelections = 0;

        StartBattle();
    }

    private void InitializeUnlockedPieces()
    {
        _unlockedPieceTypes.Clear();
        UnlockPiece(PieceType.Soldier);
        UnlockPiece(PieceType.Horse);
    }

    private void InitializeCurrentAugments()
    {
        _currentAugments.Clear();
    }

    private void InitializeAugmentChoices()
    {
        _augmentChoices.Clear();
    }

    private void InitializeOwnedCards()
    {
        _ownedCards.Clear();
    }

    private void InitializeCurrentCardChoices()
    {
        _currentCardChoices.Clear();
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
            _stageIndex++;
            _roundIndex = 1;
            UpdatePieceUnlocks();

            GenerateAugmentChoices();
            _currentState = RunState.AugmentSelection;

            return;
        }
        else
        {
            ApplyNormalBattleReward();
        }

        _roundIndex++;
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

    public void StartCardSelection(List<TacticalCard> cardChoices, int selectableCardCount)
    {
        _currentCardChoices.Clear();
        _currentCardChoices.AddRange(cardChoices);
        _remainingCardSelections = selectableCardCount;
        _currentState = RunState.CardSelection;
    }

    public void SelectCard(int index)
    {
        if (_currentState != RunState.CardSelection)
        {
            return;
        }

        if (index < 0 || index >= _currentCardChoices.Count)
        {
            return;
        }

        TacticalCard selectedCard = _currentCardChoices[index];

        if (selectedCard.UseTiming != CardUseTiming.OnAcquire &&
            _ownedCards.Count >= MaxCardCount)
        {
            return;
        }

        _currentCardChoices.RemoveAt(index);

        if (selectedCard.UseTiming == CardUseTiming.OnAcquire)
        {
            if (selectedCard.Effect != null)
            {
                selectedCard.Effect.Apply(this);
            }
        }
        else
        {
            AddCard(selectedCard);
        }

        _remainingCardSelections--;

        if (_remainingCardSelections == 0)
        {
            _currentCardChoices.Clear();
            _currentState = RunState.Shop;
        }
    }

    public void SkipCardSelection()
    {
        if (_currentState != RunState.CardSelection)
        {
            return;
        }

        _currentCardChoices.Clear();
        _remainingCardSelections = 0;
        _currentState = RunState.Shop;
    }

    public bool SellCard(int index)
    {
        if(index < 0 || index >= _ownedCards.Count)
        {
            return false;
        }

        CardType cardType = _ownedCards[index].CardType;
        int? price = TacticalCardCatalog.GetSellPrice(cardType);

        if(price == null)
        {
            return false;
        }

        _ownedCards.RemoveAt(index);
        AddGold(price.Value);

        return true;
    }

    public void GenerateAugmentChoices()
    {
        _augmentChoices.Clear();
        List<Augment> augmentList = AugmentCatalog.GetAllAugments();

        for (int i = augmentList.Count - 1; i >= 0; i--)
        {
            Augment availableAugment = augmentList[i];

            foreach (Augment currentAugment in _currentAugments)
            {
                if (availableAugment.Equals(currentAugment))
                {
                    augmentList.RemoveAt(i);
                    break;
                }
            }
        }

        int choiceCount = augmentList.Count < 3 ? augmentList.Count : 3;

        for (int i = 0; i < choiceCount; ++i)
        {
            int randomIndex = UnityEngine.Random.Range(0, augmentList.Count);
            Augment randomAugment = augmentList[randomIndex];
            augmentList.RemoveAt(randomIndex);
            _augmentChoices.Add(randomAugment);
        }
    }

    public void SelectAugment(int index)
    {
        if(_currentState != RunState.AugmentSelection)
        {
            return;
        }

        if(index < 0 || index >= _augmentChoices.Count)
        {
            return;
        }

        if(_currentAugments.Count < MaxAugmentCount)
        {
            AddAugment(_augmentChoices[index]);
            _augmentChoices.Clear();

            _currentShop = new Shop(this);
            _currentState = RunState.Shop;

            return;
        }
        
        else
        {
            _pendingAugment = _augmentChoices[index];
            _augmentChoices.Clear();
            _currentState = RunState.AugmentReplacement;
        }
    }

    public void ReplaceAugment(int index)
    {
        if (_currentState != RunState.AugmentReplacement)
        {
            return;
        }

        if (index < 0 || index >= _currentAugments.Count)
        {
            return;
        }

        if (_pendingAugment == null)
        {
            return;
        }

        _currentAugments[index] = _pendingAugment;
        _pendingAugment = null;

        _currentShop = new Shop(this);
        _currentState = RunState.Shop;
    }

    public void SkipAugment()
    {
        if (_currentState != RunState.AugmentReplacement)
        {
            return;
        }

        _pendingAugment = null;

        _currentShop = new Shop(this);
        _currentState = RunState.Shop;
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

    private void UpdatePieceUnlocks()
    {
        if (_bossClearCount >= 1)
        {
            UnlockPiece(PieceType.Cannon);
        }

        if (_bossClearCount >= 2)
        {
            UnlockPiece(PieceType.Chariot);
        }
    }

    private void UnlockPiece(PieceType type)
    {
        if (_unlockedPieceTypes.Contains(type))
        {
            return;
        }

        _unlockedPieceTypes.Add(type);
    }

    private void AddAugment(Augment augment)
    {
        if(_currentAugments.Count >= MaxAugmentCount)
        {
            return;
        }

        _currentAugments.Add(augment);
    }

    private void RemoveAugment(int index)
    {
        if(index < 0 || index >= _currentAugments.Count)
        {
            return;
        }

        if (_currentAugments[index] == null)
        {
            return;
        }

        _currentAugments.RemoveAt(index);
    }

    private void AddCard(TacticalCard card)
    {
        if (_ownedCards.Count >= MaxCardCount)
        {
            return;
        }

        _ownedCards.Add(card);
    }

    private void RemoveCard(int index)
    {
        if (index < 0 || index >= _ownedCards.Count)
        {
            return;
        }

        if (_ownedCards[index] == null)
        {
            return;
        }

        _ownedCards.RemoveAt(index);
    }

    private bool IsBossStage()
    {
        return _roundIndex % BossStageInterval == 0;
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