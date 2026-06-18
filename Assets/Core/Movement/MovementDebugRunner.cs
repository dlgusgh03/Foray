using System.Collections.Generic;
using UnityEngine;

public class MovementDebugRunner : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("===== Movement Debug Start =====");

        TestKingMoveFromCenter();
        TestKingMoveFromCorner();
        TestKingBlockedByAlly();
        TestKingCanCaptureEnemy();
        TestKingBlockedByObstacle();

        Debug.Log("===== Movement Debug End =====");
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
}