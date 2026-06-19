using System.Collections.Generic;
using UnityEngine;

public class MovementDebugRunner : MonoBehaviour
{

    private void Start()
    {
        Debug.Log("===== Movement Debug Start =====");

        RunKingTests();
        RunSoldierTests();
        RunChariotTests();
        RunHorseTests();

        Debug.Log("===== Movement Debug End =====");
    }
    private void RunKingTests()
    {
        TestKingMoveFromCenter();
        TestKingMoveFromCorner();
        TestKingBlockedByAlly();
        TestKingCanCaptureEnemy();
        TestKingBlockedByObstacle();
    }

    private void RunSoldierTests()
    {
        TestPlayerSoldierMoveFromCenter();
        TestEnemySoldierMoveFromCenter();
        TestSoldierMoveFromEdge();
        TestSoldierBlockedByAlly();
        TestSoldierCanCaptureEnemy();
        TestSoldierBlockedByObstacle();
    }

    private void RunChariotTests()
    {
        TestChariotMoveFromCenter();
        TestChariotMoveFromCorner();
        TestChariotBlockedByAlly();
        TestChariotCanCaptureEnemy();
        TestChariotBlockedByObstacle();
    }

    private void RunHorseTests()
    {
        TestHorseMoveFromCenter();
        TestHorseMoveFromCorner();
        TestHorseBlockedByAllyOnBlockPosition();
        TestHorseBlockedByEnemyOnBlockPosition();
        TestHorseBlockedByObstacleOnBlockPosition();
        TestHorseCanCaptureEnemyOnTarget();
        TestHorseBlockedByAllyOnTarget();
    }

    private void TestKingMoveFromCenter()
    {
        Debug.Log("[Test 1] King move from center");

        Board board = new Board(5, 5);

        Piece king = new Piece(
            PieceType.King,
            PieceOwner.Player,
            new BoardPosition(2, 2)
        );

        board.PlacePiece(king, king.Position);

        List<BoardPosition> positions =
            MovementCalculator.GetMovablePositions(board, king);

        PrintPositions("Expected count: 8", positions);
    }

    private void TestKingMoveFromCorner()
    {
        Debug.Log("[Test 2] King move from corner");

        Board board = new Board(5, 5);

        Piece king = new Piece(
            PieceType.King,
            PieceOwner.Player,
            new BoardPosition(0, 0)
        );

        board.PlacePiece(king, king.Position);

        List<BoardPosition> positions =
            MovementCalculator.GetMovablePositions(board, king);

        PrintPositions("Expected count: 3", positions);
    }

    private void TestKingBlockedByAlly()
    {
        Debug.Log("[Test 3] King blocked by ally");

        Board board = new Board(5, 5);

        Piece king = new Piece(
            PieceType.King,
            PieceOwner.Player,
            new BoardPosition(2, 2)
        );

        Piece allySoldier = new Piece(
            PieceType.Soldier,
            PieceOwner.Player,
            new BoardPosition(2, 3)
        );

        board.PlacePiece(king, king.Position);
        board.PlacePiece(allySoldier, allySoldier.Position);

        List<BoardPosition> positions =
            MovementCalculator.GetMovablePositions(board, king);

        PrintPositions("Expected count: 7, should NOT include (2, 3)", positions);
    }

    private void TestKingCanCaptureEnemy()
    {
        Debug.Log("[Test 4] King can capture enemy");

        Board board = new Board(5, 5);

        Piece king = new Piece(
            PieceType.King,
            PieceOwner.Player,
            new BoardPosition(2, 2)
        );

        Piece enemySoldier = new Piece(
            PieceType.Soldier,
            PieceOwner.Enemy,
            new BoardPosition(2, 3)
        );

        board.PlacePiece(king, king.Position);
        board.PlacePiece(enemySoldier, enemySoldier.Position);

        List<BoardPosition> positions =
            MovementCalculator.GetMovablePositions(board, king);

        PrintPositions("Expected count: 8, should include (2, 3)", positions);
    }

    private void TestKingBlockedByObstacle()
    {
        Debug.Log("[Test 5] King blocked by obstacle");

        Board board = new Board(5, 5);

        Piece king = new Piece(
            PieceType.King,
            PieceOwner.Player,
            new BoardPosition(2, 2)
        );

        board.PlacePiece(king, king.Position);

        BoardCell blockedCell = board.GetCell(new BoardPosition(2, 3));
        blockedCell.SetBlocked(true);

        List<BoardPosition> positions =
            MovementCalculator.GetMovablePositions(board, king);

        PrintPositions("Expected count: 7, should NOT include (2, 3)", positions);
    }

    private void PrintPositions(string message, List<BoardPosition> positions)
    {
        Debug.Log(message);
        Debug.Log($"Actual count: {positions.Count}");

        for (int i = 0; i < positions.Count; i++)
        {
            Debug.Log($"[{i}] {positions[i]}");
        }
    }

    private void TestPlayerSoldierMoveFromCenter()
    {
        Debug.Log("[Test 6] Player Soldier move from center");

        Board board = new Board(5, 5);

        Piece soldier = new Piece(
            PieceType.Soldier,
            PieceOwner.Player,
            new BoardPosition(2, 2)
        );

        board.PlacePiece(soldier, soldier.Position);

        List<BoardPosition> positions =
            MovementCalculator.GetMovablePositions(board, soldier);

        PrintPositions("Expected count: 3, should include (2, 3), (1, 2), (3, 2)", positions);
    }

    private void TestEnemySoldierMoveFromCenter()
    {
        Debug.Log("[Test 7] Enemy Soldier move from center");

        Board board = new Board(5, 5);

        Piece soldier = new Piece(
            PieceType.Soldier,
            PieceOwner.Enemy,
            new BoardPosition(2, 2)
        );

        board.PlacePiece(soldier, soldier.Position);

        List<BoardPosition> positions =
            MovementCalculator.GetMovablePositions(board, soldier);

        PrintPositions("Expected count: 3, should include (2, 1), (1, 2), (3, 2)", positions);
    }

    private void TestSoldierMoveFromEdge()
    {
        Debug.Log("[Test 8] Player Soldier move from edge");

        Board board = new Board(5, 5);

        Piece soldier = new Piece(
            PieceType.Soldier,
            PieceOwner.Player,
            new BoardPosition(0, 0)
        );

        board.PlacePiece(soldier, soldier.Position);

        List<BoardPosition> positions =
            MovementCalculator.GetMovablePositions(board, soldier);

        PrintPositions("Expected count: 2, should include (0, 1), (1, 0)", positions);
    }

    private void TestSoldierBlockedByAlly()
    {
        Debug.Log("[Test 9] Soldier blocked by ally");

        Board board = new Board(5, 5);

        Piece soldier = new Piece(
            PieceType.Soldier,
            PieceOwner.Player,
            new BoardPosition(2, 2)
        );

        Piece ally = new Piece(
            PieceType.Soldier,
            PieceOwner.Player,
            new BoardPosition(2, 3)
        );

        board.PlacePiece(soldier, soldier.Position);
        board.PlacePiece(ally, ally.Position);

        List<BoardPosition> positions =
            MovementCalculator.GetMovablePositions(board, soldier);

        PrintPositions("Expected count: 2, should NOT include (2, 3)", positions);
    }

    private void TestSoldierCanCaptureEnemy()
    {
        Debug.Log("[Test 10] Soldier can capture enemy");

        Board board = new Board(5, 5);

        Piece soldier = new Piece(
            PieceType.Soldier,
            PieceOwner.Player,
            new BoardPosition(2, 2)
        );

        Piece enemy = new Piece(
            PieceType.Soldier,
            PieceOwner.Enemy,
            new BoardPosition(2, 3)
        );

        board.PlacePiece(soldier, soldier.Position);
        board.PlacePiece(enemy, enemy.Position);

        List<BoardPosition> positions =
            MovementCalculator.GetMovablePositions(board, soldier);

        PrintPositions("Expected count: 3, should include (2, 3)", positions);
    }

    private void TestSoldierBlockedByObstacle()
    {
        Debug.Log("[Test 11] Soldier blocked by obstacle");

        Board board = new Board(5, 5);

        Piece soldier = new Piece(
            PieceType.Soldier,
            PieceOwner.Player,
            new BoardPosition(2, 2)
        );

        board.PlacePiece(soldier, soldier.Position);

        BoardCell blockedCell = board.GetCell(new BoardPosition(2, 3));
        blockedCell.SetBlocked(true);

        List<BoardPosition> positions =
            MovementCalculator.GetMovablePositions(board, soldier);

        PrintPositions("Expected count: 2, should NOT include (2, 3)", positions);
    }

    private void TestChariotMoveFromCenter()
    {
        Debug.Log("[Test 12] Chariot move from center");

        Board board = new Board(5, 5);

        Piece chariot = new Piece(
            PieceType.Chariot,
            PieceOwner.Player,
            new BoardPosition(2, 2)
        );

        board.PlacePiece(chariot, chariot.Position);

        List<BoardPosition> positions =
            MovementCalculator.GetMovablePositions(board, chariot);

        PrintPositions(
            "Expected count: 8, should include (2, 0), (2, 1), (2, 3), (2, 4), (0, 2), (1, 2), (3, 2), (4, 2)",
            positions
        );
    }

    private void TestChariotMoveFromCorner()
    {
        Debug.Log("[Test 13] Chariot move from corner");

        Board board = new Board(5, 5);

        Piece chariot = new Piece(
            PieceType.Chariot,
            PieceOwner.Player,
            new BoardPosition(0, 0)
        );

        board.PlacePiece(chariot, chariot.Position);

        List<BoardPosition> positions =
            MovementCalculator.GetMovablePositions(board, chariot);

        PrintPositions(
            "Expected count: 8, should include (0, 1), (0, 2), (0, 3), (0, 4), (1, 0), (2, 0), (3, 0), (4, 0)",
            positions
        );
    }

    private void TestChariotBlockedByAlly()
    {
        Debug.Log("[Test 14] Chariot blocked by ally");

        Board board = new Board(5, 5);

        Piece chariot = new Piece(
            PieceType.Chariot,
            PieceOwner.Player,
            new BoardPosition(2, 2)
        );

        Piece ally = new Piece(
            PieceType.Soldier,
            PieceOwner.Player,
            new BoardPosition(2, 3)
        );

        board.PlacePiece(chariot, chariot.Position);
        board.PlacePiece(ally, ally.Position);

        List<BoardPosition> positions =
            MovementCalculator.GetMovablePositions(board, chariot);

        PrintPositions(
            "Expected count: 6, should NOT include (2, 3), should NOT include (2, 4)",
            positions
        );
    }

    private void TestChariotCanCaptureEnemy()
    {
        Debug.Log("[Test 15] Chariot can capture enemy");

        Board board = new Board(5, 5);

        Piece chariot = new Piece(
            PieceType.Chariot,
            PieceOwner.Player,
            new BoardPosition(2, 2)
        );

        Piece enemy = new Piece(
            PieceType.Soldier,
            PieceOwner.Enemy,
            new BoardPosition(2, 3)
        );

        board.PlacePiece(chariot, chariot.Position);
        board.PlacePiece(enemy, enemy.Position);

        List<BoardPosition> positions =
            MovementCalculator.GetMovablePositions(board, chariot);

        PrintPositions(
            "Expected count: 7, should include (2, 3), should NOT include (2, 4)",
            positions
        );
    }

    private void TestChariotBlockedByObstacle()
    {
        Debug.Log("[Test 16] Chariot blocked by obstacle");

        Board board = new Board(5, 5);

        Piece chariot = new Piece(
            PieceType.Chariot,
            PieceOwner.Player,
            new BoardPosition(2, 2)
        );

        board.PlacePiece(chariot, chariot.Position);

        BoardCell blockedCell = board.GetCell(new BoardPosition(2, 3));
        blockedCell.SetBlocked(true);

        List<BoardPosition> positions =
            MovementCalculator.GetMovablePositions(board, chariot);

        PrintPositions(
            "Expected count: 6, should NOT include (2, 3), should NOT include (2, 4)",
            positions
        );
    }

    private void TestHorseMoveFromCenter()
    {
        Debug.Log("[Test 17] Horse move from center");

        Board board = new Board(5, 5);

        Piece horse = new Piece(
            PieceType.Horse,
            PieceOwner.Player,
            new BoardPosition(2, 2)
        );

        board.PlacePiece(horse, horse.Position);

        List<BoardPosition> positions =
            MovementCalculator.GetMovablePositions(board, horse);

        PrintPositions(
            "Expected count: 8, should include (1, 4), (3, 4), (1, 0), (3, 0), (0, 3), (0, 1), (4, 3), (4, 1)",
            positions
        );
    }

    private void TestHorseMoveFromCorner()
    {
        Debug.Log("[Test 18] Horse move from corner");

        Board board = new Board(5, 5);

        Piece horse = new Piece(
            PieceType.Horse,
            PieceOwner.Player,
            new BoardPosition(0, 0)
        );

        board.PlacePiece(horse, horse.Position);

        List<BoardPosition> positions =
            MovementCalculator.GetMovablePositions(board, horse);

        PrintPositions(
            "Expected count: 2, should include (1, 2), (2, 1)",
            positions
        );
    }

    private void TestHorseBlockedByAllyOnBlockPosition()
    {
        Debug.Log("[Test 19] Horse blocked by ally on block position");

        Board board = new Board(5, 5);

        Piece horse = new Piece(
            PieceType.Horse,
            PieceOwner.Player,
            new BoardPosition(2, 2)
        );

        Piece ally = new Piece(
            PieceType.Soldier,
            PieceOwner.Player,
            new BoardPosition(2, 3)
        );

        board.PlacePiece(horse, horse.Position);
        board.PlacePiece(ally, ally.Position);

        List<BoardPosition> positions =
            MovementCalculator.GetMovablePositions(board, horse);

        PrintPositions(
            "Expected count: 6, should NOT include (1, 4), should NOT include (3, 4)",
            positions
        );
    }

    private void TestHorseBlockedByEnemyOnBlockPosition()
    {
        Debug.Log("[Test 20] Horse blocked by enemy on block position");

        Board board = new Board(5, 5);

        Piece horse = new Piece(
            PieceType.Horse,
            PieceOwner.Player,
            new BoardPosition(2, 2)
        );

        Piece enemy = new Piece(
            PieceType.Soldier,
            PieceOwner.Enemy,
            new BoardPosition(2, 3)
        );

        board.PlacePiece(horse, horse.Position);
        board.PlacePiece(enemy, enemy.Position);

        List<BoardPosition> positions =
            MovementCalculator.GetMovablePositions(board, horse);

        PrintPositions(
            "Expected count: 6, should NOT include (1, 4), should NOT include (3, 4)",
            positions
        );
    }

    private void TestHorseBlockedByObstacleOnBlockPosition()
    {
        Debug.Log("[Test 21] Horse blocked by obstacle on block position");

        Board board = new Board(5, 5);

        Piece horse = new Piece(
            PieceType.Horse,
            PieceOwner.Player,
            new BoardPosition(2, 2)
        );

        board.PlacePiece(horse, horse.Position);

        BoardCell blockedCell = board.GetCell(new BoardPosition(2, 3));
        blockedCell.SetBlocked(true);

        List<BoardPosition> positions =
            MovementCalculator.GetMovablePositions(board, horse);

        PrintPositions(
            "Expected count: 6, should NOT include (1, 4), should NOT include (3, 4)",
            positions
        );
    }

    private void TestHorseCanCaptureEnemyOnTarget()
    {
        Debug.Log("[Test 22] Horse can capture enemy on target");

        Board board = new Board(5, 5);

        Piece horse = new Piece(
            PieceType.Horse,
            PieceOwner.Player,
            new BoardPosition(2, 2)
        );

        Piece enemy = new Piece(
            PieceType.Soldier,
            PieceOwner.Enemy,
            new BoardPosition(1, 4)
        );

        board.PlacePiece(horse, horse.Position);
        board.PlacePiece(enemy, enemy.Position);

        List<BoardPosition> positions =
            MovementCalculator.GetMovablePositions(board, horse);

        PrintPositions(
            "Expected count: 8, should include (1, 4)",
            positions
        );
    }

    private void TestHorseBlockedByAllyOnTarget()
    {
        Debug.Log("[Test 23] Horse blocked by ally on target");

        Board board = new Board(5, 5);

        Piece horse = new Piece(
            PieceType.Horse,
            PieceOwner.Player,
            new BoardPosition(2, 2)
        );

        Piece ally = new Piece(
            PieceType.Soldier,
            PieceOwner.Player,
            new BoardPosition(1, 4)
        );

        board.PlacePiece(horse, horse.Position);
        board.PlacePiece(ally, ally.Position);

        List<BoardPosition> positions =
            MovementCalculator.GetMovablePositions(board, horse);

        PrintPositions(
            "Expected count: 7, should NOT include (1, 4)",
            positions
        );
    }
}