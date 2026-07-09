using System.Collections.Generic;

public class Shop
{
    private const int StandardChestPrice = 5;
    private const int LargeChestPrice = 8;
    private const int PopulationPriceIncreaseAmount = 3;
    private const int PopulationIncreaseAmount = 2;

    private readonly RunManager _runManager;
    private readonly PlayerArmy _playerArmy;
    private bool _hasBoughtPopulation;
    private List<ShopItem> _items;

    public RunManager RunManager => _runManager;
    public PlayerArmy PlayerArmy => _playerArmy;
    public bool HasBoughtPopulation => _hasBoughtPopulation;

    public Shop(RunManager runManager)
    {
        _runManager = runManager;
        _playerArmy = runManager.PlayerArmy;
        _hasBoughtPopulation = false;
        _items = new List<ShopItem>();

        GenerateItems();
    }

    public List<ShopItem> GetItems()
    {
        return new List<ShopItem>(_items);
    }

    private void GenerateItems()
    {
        List<PieceType> unlockedPieceTypes = _runManager.GetUnlockedPieceTypes();

        foreach (PieceType type in unlockedPieceTypes)
        {
            AddPieceItem(type);
        }

        AddRandomCardChestItem();
        AddRandomCardChestItem();
    }

    private void AddPieceItem(PieceType type)
    {
        int? price = PieceCatalog.GetBuyPrice(type);

        if (price == null)
        {
            return;
        }

        _items.Add(new ShopItem(type, price.Value));
    }

    private void AddRandomCardChestItem()
    {
        CardChestType chestType = CardChestType.None;
        CardChestSize chestSize = CardChestSize.None;

        int randomChestType = UnityEngine.Random.Range(0, 100);
        int randomChestSize = UnityEngine.Random.Range(0, 100);

        int price = 0;

        if (randomChestType <= 74)
        {
            chestType = CardChestType.Supply;
        }
        else if (randomChestType <= 94)
        {
            chestType = CardChestType.Sealed;
        }
        else
        {
            chestType = CardChestType.Cursed;
        }

        if (randomChestSize <= 64)
        {
            chestSize = CardChestSize.Standard;
            price = StandardChestPrice;
        }
        else
        {
            chestSize = CardChestSize.Large;
            price = LargeChestPrice;
        }

        CardChest chest = new CardChest(chestType, chestSize);
        ShopItem item = new ShopItem(chest, price);
        _items.Add(item);
    }

    public bool BuyItem(int index)
    {
        if (index < 0 || index >= _items.Count)
        {
            return false;
        }

        ShopItem item = _items[index];
        
        if (item.IsSold)
        {
            return false;
        }

        if (CanBuyPiece(item.PieceType))
        {
            _runManager.SpendGold(item.Price);
            _playerArmy.AddPiece(item.PieceType);
            item.MarkSold();

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