using UnityEngine;

public class BoardDebugRunner : MonoBehaviour
{
    private void Start()
    {
        Board board = new Board(9, 10);

        Debug.Log($"Board created. Width: {board.Width}, Height: {board.Height}");

        BoardPosition validPosition = new BoardPosition(0, 0);
        BoardPosition invalidPosition = new BoardPosition(10, 10);

        Debug.Log($"(0, 0) inside? {board.IsInside(validPosition)}");
        Debug.Log($"(10, 10) inside? {board.IsInside(invalidPosition)}");

        BoardCell cell = board.GetCell(validPosition);

        if (cell != null)
        {
            Debug.Log($"Cell found at {cell.Position}");
        }
    }
}