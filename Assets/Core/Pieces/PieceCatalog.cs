public static class PieceCatalog
{
    public static int GetPopulationCost(PieceType type)
    {
        switch (type)
        {
            case PieceType.King:
                return 2;
            case PieceType.Soldier:
                return 2;
            case PieceType.Horse:
                return 3;
            case PieceType.Cannon:
                return 4;
            case PieceType.Chariot:
                return 6;
            default:
                return 0;
        }
    }
    public static int? GetBuyPrice(PieceType type) {
        switch (type)
        {
            case PieceType.King:
                return null;
            case PieceType.Soldier:
                return 3;
            case PieceType.Horse:
                return 5;
            case PieceType.Cannon:
                return 7;
            case PieceType.Chariot:
                return 9;
            default:
                return null;
        }
    }
    public static int? GetSellPrice(PieceType type)
    {
        switch (type)
        {
            case PieceType.King:
                return null;
            case PieceType.Soldier:
                return 1;
            case PieceType.Horse:
                return 2;
            case PieceType.Cannon:
                return 3;
            case PieceType.Chariot:
                return 4;
            default:
                return null;
        }
    }
}