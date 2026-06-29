public static class BattleSetup
{
    public static BattleManager CreateBasicBattle()
    {
        EnemyPreset enemyPreset = EnemyPreset.CreateEasySoldiers();

        return CreateBattle(enemyPreset);
    }

    public static BattleManager CreateBattle(EnemyPreset enemyPreset)
    {
        Board board = new Board(7, 7);

        Piece playerKing = new Piece(PieceType.King, PieceOwner.Player, new BoardPosition(3, 0));
        Piece playerSoldier1 = new Piece(PieceType.Soldier, PieceOwner.Player, new BoardPosition(2, 0));
        Piece playerSoldier2 = new Piece(PieceType.Soldier, PieceOwner.Player, new BoardPosition(4, 0));

        board.PlacePiece(playerKing, playerKing.Position);
        board.PlacePiece(playerSoldier1, playerSoldier1.Position);
        board.PlacePiece(playerSoldier2, playerSoldier2.Position);

        foreach (EnemyPieceData enemyPieceData in enemyPreset.EnemyPieces)
        {
            Piece enemyPiece = new Piece(enemyPieceData.Type, PieceOwner.Enemy, enemyPieceData.Position);
            board.PlacePiece(enemyPiece, enemyPiece.Position);
        }

        return new BattleManager(board);
    }
}