using System.Collections.Generic;

public static class BattleSetup
{
    public static BattleManager CreateBasicBattle()
    {
        MapPreset mapPreset = MapPreset.CreateOpenField();
        EnemyPreset enemyPreset = EnemyPreset.CreateEasySoldiers();
        PlayerArmy playerArmy = new PlayerArmy();

        return CreateBattle(mapPreset, enemyPreset, playerArmy);
    }

    public static BattleManager CreateBattle(
        MapPreset mapPreset,
        EnemyPreset enemyPreset,
        PlayerArmy playerArmy
    )
    {
        if (mapPreset == null || enemyPreset == null || playerArmy == null)
        {
            return null;
        }

        Board board = CreateBoardFromMapPreset(mapPreset);

        bool playerDeployed = DeployPlayerPieces(board, mapPreset, playerArmy);

        if (!playerDeployed)
        {
            return null;
        }

        bool enemyDeployed = DeployEnemyPieces(board, enemyPreset);

        if (!enemyDeployed)
        {
            return null;
        }

        return new BattleManager(board);
    }

    private static Board CreateBoardFromMapPreset(MapPreset mapPreset)
    {
        Board board = new Board(mapPreset.Width, mapPreset.Height);

        foreach (BoardPosition position in mapPreset.BlockedPositions)
        {
            BoardCell cell = board.GetCell(position);

            if (cell != null)
            {
                cell.SetBlocked(true);
            }
        }

        return board;
    }

    private static bool DeployPlayerPieces(Board board, MapPreset mapPreset, PlayerArmy playerArmy)
    {
        if (board == null || mapPreset == null || playerArmy == null)
        {
            return false;
        }

        List<PieceType> pieceTypes = playerArmy.GetOwnedPieceTypes();

        if (pieceTypes.Count > mapPreset.PlayerDeployPositions.Count)
        {
            return false;
        }

        for (int i = 0; i < pieceTypes.Count; i++)
        {
            PieceType type = pieceTypes[i];
            BoardPosition position = mapPreset.PlayerDeployPositions[i];

            Piece piece = new Piece(type, PieceOwner.Player, position);

            bool placed = board.PlacePiece(piece, position);

            if (!placed)
            {
                return false;
            }
        }

        return true;
    }

    private static bool DeployEnemyPieces(Board board, EnemyPreset enemyPreset)
    {
        if (board == null || enemyPreset == null)
        {
            return false;
        }

        foreach (EnemyPieceData enemyPieceData in enemyPreset.EnemyPieces)
        {
            Piece enemyPiece = new Piece(
                enemyPieceData.Type,
                PieceOwner.Enemy,
                enemyPieceData.Position
            );

            bool placed = board.PlacePiece(enemyPiece, enemyPiece.Position);

            if (!placed)
            {
                return false;
            }
        }

        return true;
    }
}