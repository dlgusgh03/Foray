using System.Collections.Generic;

public class EnemyPreset
{
    private readonly List<EnemyPieceData> _enemyPieces;

    public List<EnemyPieceData> EnemyPieces => _enemyPieces;

    public EnemyPreset(List<EnemyPieceData> enemyPieces)
    {
        _enemyPieces = enemyPieces;
    }
}