using System.Collections.Generic;

public class Shop
{
    private const int PopulationPriceIncreaseAmount = 3;
    private const int PopulationIncreaseAmount = 2;

    private readonly RunManager _runManager;
    private readonly PlayerArmy _playerArmy;
    private bool _hasBoughtPopulation;

    public RunManager RunManager => _runManager;
    public PlayerArmy PlayerArmy => _playerArmy;
    public bool HasBoughtPopulation => _hasBoughtPopulation;

    public Shop(RunManager runManager)
    {
        _runManager = runManager;
        _playerArmy = runManager.PlayerArmy;
        _hasBoughtPopulation = false;
    }

    public bool BuyPiece(PieceType type)
    {
        int? price = PieceCatalog.GetBuyPrice(type);

        if (CanBuyPiece(type))
        {
            _runManager.SpendGold(price.Value);
            _playerArmy.AddPiece(type);

            return true;
        }

        return false;
    }

    public bool SellPiece(int index)
    {
        if(index < 0 || index >= _playerArmy.PieceCount)
        {
            return false;
        }

        List<PieceType> ownedPieceTypes = _playerArmy.GetOwnedPieceTypes();
        int? price = PieceCatalog.GetSellPrice(ownedPieceTypes[index]);

        if (price == null)
        {
            return false;
        }

        if (_playerArmy.RemovePieceAt(index))
        {
            _runManager.AddGold(price.Value);

            return true;
        }
        
        return false;
    }

    public bool BuyPopulation()
    {
        if (CanBuyPopulation())
        {
            _runManager.SpendGold(_runManager.PopulationPrice);
            _playerArmy.IncreaseMaxPopulation(PopulationIncreaseAmount);
            _runManager.IncreasePopulationPrice(PopulationPriceIncreaseAmount);
            _hasBoughtPopulation = true;

            return true;
        }

        return false;
    }

    public bool CanBuyPiece(PieceType type)
    {
        int? price = PieceCatalog.GetBuyPrice(type);

        if (price == null)
        {
            return false;
        }

        if (!_runManager.CanSpendGold(price.Value))
        {
            return false;
        }

        if (!_playerArmy.CanAddPiece(type))
        {
            return false;
        }

        return true;
    }

    private bool CanBuyPopulation()
    {
        if (_hasBoughtPopulation)
        {
            return false;
        }

        return _runManager.CanSpendGold(_runManager.PopulationPrice);
    }
}