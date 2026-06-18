using UnityEngine;

public class BoardDebugRunner : MonoBehaviour
{
    private void Start()
    {
        Board board = new Board(9, 10);

        BoardPosition start = new BoardPosition(4, 0);
        BoardPosition target = new BoardPosition(4, 1);

        Piece king = new Piece(PieceType.King, PieceOwner.Player, start);

        bool placed = board.PlacePiece(king, start);
        Debug.Log($"Placed: {placed}");
        Debug.Log($"Before move: {king}");

        bool moved = board.MovePiece(start, target);
        Debug.Log($"Moved: {moved}");

        Debug.Log($"Start occupied: {board.IsOccupied(start)}");
        Debug.Log($"Target occupied: {board.IsOccupied(target)}");
        Debug.Log($"Piece at target: {board.GetPiece(target)}");
        Debug.Log($"After move: {king}");
    }
}