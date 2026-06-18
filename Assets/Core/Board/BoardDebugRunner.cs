using UnityEngine;

public class BoardDebugRunner : MonoBehaviour
{
    private void Start()
    {
        Board board = new Board(9, 10);

        BoardPosition attackerPosition = new BoardPosition(4, 0);
        BoardPosition targetPosition = new BoardPosition(4, 1);

        Piece playerKing = new Piece(PieceType.King, PieceOwner.Player, attackerPosition);
        //Piece enemySoldier = new Piece(PieceType.Soldier, PieceOwner.Enemy, targetPosition);
        Piece enemySoldier = new Piece(PieceType.Soldier, PieceOwner.Player, targetPosition);

        board.PlacePiece(playerKing, attackerPosition);
        board.PlacePiece(enemySoldier, targetPosition);

        Debug.Log($"Before capture attacker: {playerKing}");
        Debug.Log($"Before capture target: {enemySoldier}");

        Piece capturedPiece = board.CapturePiece(attackerPosition, targetPosition);

        Debug.Log($"Captured: {capturedPiece}");
        Debug.Log($"Attacker after capture: {playerKing}");
        Debug.Log($"Target occupied: {board.IsOccupied(targetPosition)}");
        Debug.Log($"Piece at target: {board.GetPiece(targetPosition)}");
        Debug.Log($"Captured piece alive: {enemySoldier.IsAlive}");
    }
}