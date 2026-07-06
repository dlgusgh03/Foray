public class ShopItem
{
    private readonly PieceType _pieceType;
    private readonly int _price;
    private bool _isSold;

    public PieceType PieceType => _pieceType;
    public int Price => _price;
    public bool IsSold => _isSold;
    
    public ShopItem(PieceType pieceType, int price)
    {
        _pieceType = pieceType;
        _price = price;
        _isSold = false;
    }

    public void MarkSold()
    {
        _isSold = true;
    }
}