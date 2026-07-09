public class ShopItem
{
    private readonly ShopItemType _itemType;
    private readonly PieceType _pieceType;
    private readonly CardChest _cardChest;
    private readonly int _price;
    private bool _isSold;

    public ShopItemType ItemType => _itemType;
    public PieceType PieceType => _pieceType;
    public CardChest CardChest => _cardChest;
    public int Price => _price;
    public bool IsSold => _isSold;

    public ShopItem(PieceType pieceType, int price)
    {
        _itemType = ShopItemType.Piece;
        _pieceType = pieceType;
        _cardChest = null;
        _price = price;
        _isSold = false;
    }

    public ShopItem(CardChest cardChest, int price)
    {
        _itemType = ShopItemType.CardChest;
        _pieceType = PieceType.None;
        _cardChest = cardChest;
        _price = price;
        _isSold = false;
    }

    public void MarkSold()
    {
        _isSold = true;
    }
}