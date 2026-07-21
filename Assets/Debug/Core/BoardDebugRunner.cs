using UnityEngine;

public class BoardDebugRunner : MonoBehaviour
{
    private void Start()
    {
        TestMovePiece();
        TestCaptureEnemyPiece();
        TestCannotCaptureAllyPiece();
    }

    private void TestMovePiece()
    {
        Debug.Log("=== Move Piece Test ===");

        Board board = new Board(9, 10);

        BoardPosition start = new BoardPosition(4, 0);
        BoardPosition target = new BoardPosition(4, 1);

        Piece king = new Piece(PieceType.King, PieceOwner.Player, start, 0);

        board.PlacePiece(king, start);

        bool moved = board.MovePiece(start, target);

        Debug.Log($"Moved: {moved}");
        Debug.Log($"Start occupied: {board.IsOccupied(start)}");
        Debug.Log($"Target occupied: {board.IsOccupied(target)}");
        Debug.Log($"Piece at target: {board.GetPiece(target)}");
    }

    private void TestCaptureEnemyPiece()
    {
        Debug.Log("=== Capture Enemy Piece Test ===");

        Board board = new Board(9, 10);

        BoardPosition attackerPosition = new BoardPosition(4, 0);
        BoardPosition targetPosition = new BoardPosition(4, 1);

        Piece playerKing = new Piece(PieceType.King, PieceOwner.Player, attackerPosition, 0);
        Piece enemySoldier = new Piece(PieceType.Soldier, PieceOwner.Enemy, targetPosition, null);

        board.PlacePiece(playerKing, attackerPosition);
        board.PlacePiece(enemySoldier, targetPosition);

        Piece capturedPiece = board.CapturePiece(attackerPosition, targetPosition);

        Debug.Log($"Captured: {(capturedPiece == null ? "null" : capturedPiece.ToString())}");
        Debug.Log($"Attacker after capture: {playerKing}");
        Debug.Log($"Target occupied: {board.IsOccupied(targetPosition)}");
        Debug.Log($"Piece at target: {board.GetPiece(targetPosition)}");
        Debug.Log($"Captured piece alive: {enemySoldier.IsAlive}");
    }

    private void TestCannotCaptureAllyPiece()
    {
        Debug.Log("=== Cannot Capture Ally Piece Test ===");

        Board board = new Board(9, 10);

        BoardPosition attackerPosition = new BoardPosition(4, 0);
        BoardPosition targetPosition = new BoardPosition(4, 1);

        Piece playerKing = new Piece(PieceType.King, PieceOwner.Player, attackerPosition, 0);
        Piece playerSoldier = new Piece(PieceType.Soldier, PieceOwner.Player, targetPosition, 0);

        board.PlacePiece(playerKing, attackerPosition);
        board.PlacePiece(playerSoldier, targetPosition);

        Piece capturedPiece = board.CapturePiece(attackerPosition, targetPosition);

        Debug.Log($"Captured: {(capturedPiece == null ? "null" : capturedPiece.ToString())}");
        Debug.Log($"Attacker after failed capture: {playerKing}");
        Debug.Log($"Target occupied: {board.IsOccupied(targetPosition)}");
        Debug.Log($"Piece at target: {board.GetPiece(targetPosition)}");
        Debug.Log($"Ally piece alive: {playerSoldier.IsAlive}");
    }
}