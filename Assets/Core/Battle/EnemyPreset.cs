using System.Collections.Generic;

public class EnemyPreset
{
    private readonly List<EnemyPieceData> _enemyPieces;

    public List<EnemyPieceData> EnemyPieces => _enemyPieces;

    public EnemyPreset(List<EnemyPieceData> enemyPieces)
    {
        _enemyPieces = enemyPieces;
    }

    public static EnemyPreset CreateEasySoldiers()
    {
        List<EnemyPieceData> enemyPieces = new List<EnemyPieceData>()
        {
            new EnemyPieceData(PieceType.King, new BoardPosition(3, 6)),
            new EnemyPieceData(PieceType.Soldier, new BoardPosition(2, 5)),
            new EnemyPieceData(PieceType.Soldier, new BoardPosition(4, 5))
        };

        return new EnemyPreset(enemyPieces);
    }

    public static EnemyPreset CreateBasicMixed()
    {
        List<EnemyPieceData> enemyPieces = new List<EnemyPieceData>()
        {
            new EnemyPieceData(PieceType.King, new BoardPosition(3, 6)),
            new EnemyPieceData(PieceType.Soldier, new BoardPosition(2, 5)),
            new EnemyPieceData(PieceType.Horse, new BoardPosition(4, 5))
        };

        return new EnemyPreset(enemyPieces);
    }

    public static EnemyPreset CreateChariotPressure()
    {
        List<EnemyPieceData> enemyPieces = new List<EnemyPieceData>()
        {
            new EnemyPieceData(PieceType.King, new BoardPosition(3, 6)),
            new EnemyPieceData(PieceType.Soldier, new BoardPosition(2, 5)),
            new EnemyPieceData(PieceType.Chariot, new BoardPosition(4, 5))
        };

        return new EnemyPreset(enemyPieces);
    }
}