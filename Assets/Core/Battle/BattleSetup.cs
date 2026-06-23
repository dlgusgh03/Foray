public static class BattleSetup
{
    public static BattleManager CreateBasicBattle()
    {
        Board board = new Board(5, 5);
        Piece playerKing = new Piece(PieceType.King, PieceOwner.Player, new BoardPosition(2, 0));
        Piece playerSoldier1 = new Piece(PieceType.Soldier, PieceOwner.Player, new BoardPosition(1, 0));
        Piece playerSoldier2 = new Piece(PieceType.Soldier, PieceOwner.Player, new BoardPosition(3, 0));


        Piece enemyKing = new Piece(PieceType.King, PieceOwner.Enemy, new BoardPosition(2, 4));
        Piece enemySoldier1 = new Piece(PieceType.Soldier, PieceOwner.Enemy, new BoardPosition(1, 4));
        Piece enemySoldier2 = new Piece(PieceType.Soldier, PieceOwner.Enemy, new BoardPosition(3, 4));

        board.PlacePiece(playerKing, playerKing.Position);
        board.PlacePiece(playerSoldier1, playerSoldier1.Position);
        board.PlacePiece(playerSoldier2, playerSoldier2.Position);

        board.PlacePiece(enemyKing, enemyKing.Position);
        board.PlacePiece(enemySoldier1, enemySoldier1.Position);
        board.PlacePiece(enemySoldier2, enemySoldier2.Position);

        return new BattleManager(board);
    }
}