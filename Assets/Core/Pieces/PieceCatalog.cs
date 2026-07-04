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
}