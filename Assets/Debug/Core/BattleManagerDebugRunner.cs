using UnityEngine;

public class BattleManagerDebugRunner : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("===== BattleManager Debug Start =====");

        TestPlayerCapturesEnemyKing();
        TestEnemyCapturesPlayerKing();
        TestPlayerCannotActAfterBattleOver();
        TestEnemyCannotActAfterBattleOver();

        Debug.Log("===== BattleManager Debug End =====");
    }

    private void TestPlayerCapturesEnemyKing()
    {
        Debug.Log("[Test 1] Player captures enemy king");

        Board board = new Board(5, 5);

        Piece playerKing = new Piece(
            PieceType.King,
            PieceOwner.Player,
            new BoardPosition(0, 0)
        );

        Piece playerSoldier = new Piece(
            PieceType.Soldier,
            PieceOwner.Player,
            new BoardPosition(2, 2)
        );

        Piece enemyKing = new Piece(
            PieceType.King,
            PieceOwner.Enemy,
            new BoardPosition(2, 3)
        );

        board.PlacePiece(playerKing, playerKing.Position);
        board.PlacePiece(playerSoldier, playerSoldier.Position);
        board.PlacePiece(enemyKing, enemyKing.Position);

        BattleManager battleManager = new BattleManager(board);

        bool result = battleManager.PlayerAct(
            new BoardPosition(2, 2),
            new BoardPosition(2, 3)
        );

        PrintResult("PlayerAct should return true", result == true);
        PrintResult("Battle should be over", battleManager.IsBattleOver() == true);
        PrintResult("Winner should be Player", battleManager.GetWinner() == PieceOwner.Player);
        PrintResult("Enemy king should be dead", enemyKing.IsAlive == false);
    }

    private void TestEnemyCapturesPlayerKing()
    {
        Debug.Log("[Test 2] Enemy captures player king");

        Board board = new Board(5, 5);

        Piece playerKing = new Piece(
            PieceType.King,
            PieceOwner.Player,
            new BoardPosition(0, 0)
        );

        Piece playerSoldier = new Piece(
            PieceType.Soldier,
            PieceOwner.Player,
            new BoardPosition(2, 2)
        );

        Piece enemyKing = new Piece(
            PieceType.King,
            PieceOwner.Enemy,
            new BoardPosition(4, 4)
        );

        Piece enemySoldier = new Piece(
            PieceType.Soldier,
            PieceOwner.Enemy,
            new BoardPosition(0, 1)
        );

        board.PlacePiece(playerKing, playerKing.Position);
        board.PlacePiece(playerSoldier, playerSoldier.Position);
        board.PlacePiece(enemyKing, enemyKing.Position);
        board.PlacePiece(enemySoldier, enemySoldier.Position);

        BattleManager battleManager = new BattleManager(board);

        bool playerResult = battleManager.PlayerAct(
            new BoardPosition(2, 2),
            new BoardPosition(2, 3)
        );

        battleManager.EnemyAct();

        PrintResult("PlayerAct should return true", playerResult == true);
        PrintResult("Battle should be over", battleManager.IsBattleOver() == true);
        PrintResult("Winner should be Enemy", battleManager.GetWinner() == PieceOwner.Enemy);
        PrintResult("Player king should be dead", playerKing.IsAlive == false);
        PrintResult("Enemy soldier should move to player king position", enemySoldier.Position.Equals(new BoardPosition(0, 0)));
    }

    private void TestPlayerCannotActAfterBattleOver()
    {
        Debug.Log("[Test 3] Player cannot act after battle over");

        Board board = new Board(5, 5);

        Piece playerKing = new Piece(
            PieceType.King,
            PieceOwner.Player,
            new BoardPosition(0, 0)
        );

        Piece playerSoldier = new Piece(
            PieceType.Soldier,
            PieceOwner.Player,
            new BoardPosition(2, 2)
        );

        Piece enemyKing = new Piece(
            PieceType.King,
            PieceOwner.Enemy,
            new BoardPosition(2, 3)
        );

        board.PlacePiece(playerKing, playerKing.Position);
        board.PlacePiece(playerSoldier, playerSoldier.Position);
        board.PlacePiece(enemyKing, enemyKing.Position);

        BattleManager battleManager = new BattleManager(board);

        bool firstAction = battleManager.PlayerAct(
            new BoardPosition(2, 2),
            new BoardPosition(2, 3)
        );

        bool secondAction = battleManager.PlayerAct(
            new BoardPosition(0, 0),
            new BoardPosition(1, 0)
        );

        PrintResult("First PlayerAct should return true", firstAction == true);
        PrintResult("Battle should be over", battleManager.IsBattleOver() == true);
        PrintResult("Second PlayerAct should return false", secondAction == false);
        PrintResult("Player king should NOT move after battle over", playerKing.Position.Equals(new BoardPosition(0, 0)));
    }

    private void TestEnemyCannotActAfterBattleOver()
    {
        Debug.Log("[Test 4] Enemy cannot act after battle over");

        Board board = new Board(5, 5);

        Piece playerKing = new Piece(
            PieceType.King,
            PieceOwner.Player,
            new BoardPosition(0, 0)
        );

        Piece playerSoldier = new Piece(
            PieceType.Soldier,
            PieceOwner.Player,
            new BoardPosition(2, 2)
        );

        Piece enemyKing = new Piece(
            PieceType.King,
            PieceOwner.Enemy,
            new BoardPosition(2, 3)
        );

        Piece enemySoldier = new Piece(
            PieceType.Soldier,
            PieceOwner.Enemy,
            new BoardPosition(4, 4)
        );

        board.PlacePiece(playerKing, playerKing.Position);
        board.PlacePiece(playerSoldier, playerSoldier.Position);
        board.PlacePiece(enemyKing, enemyKing.Position);
        board.PlacePiece(enemySoldier, enemySoldier.Position);

        BattleManager battleManager = new BattleManager(board);

        bool playerAction = battleManager.PlayerAct(
            new BoardPosition(2, 2),
            new BoardPosition(2, 3)
        );

        battleManager.EnemyAct();

        PrintResult("PlayerAct should return true", playerAction == true);
        PrintResult("Battle should be over", battleManager.IsBattleOver() == true);
        PrintResult("Winner should be Player", battleManager.GetWinner() == PieceOwner.Player);
        PrintResult("Enemy soldier should NOT move after battle over", enemySoldier.Position.Equals(new BoardPosition(4, 4)));
    }

    private void PrintResult(string message, bool passed)
    {
        if (passed)
        {
            Debug.Log($"[PASS] {message}");
        }
        else
        {
            Debug.LogError($"[FAIL] {message}");
        }
    }
}