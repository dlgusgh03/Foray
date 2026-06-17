using UnityEngine;

public class BoardDebugRunner : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("=== Board Debug Started ===");

        TestBoardPosition();
        TestBoardCell();

        Debug.Log("=== Board Debug Completed ===");
    }

    private void TestBoardPosition()
    {
        Debug.Log("--- BoardPosition Test ---");

        BoardPosition positionA = new BoardPosition(4, 5);
        BoardPosition positionB = positionA.Add(1, 0);
        BoardPosition positionC = new BoardPosition(4, 5);

        Debug.Log($"Position A: {positionA}");
        Debug.Log($"Position B: {positionB}");
        Debug.Log($"Position C: {positionC}");

        Debug.Log($"A equals B: {positionA.Equals(positionB)}");
        Debug.Log($"A equals C: {positionA.Equals(positionC)}");
    }

    private void TestBoardCell()
    {
        Debug.Log("--- BoardCell Test ---");

        BoardPosition position = new BoardPosition(4, 5);
        BoardCell cell = new BoardCell(position);

        Debug.Log($"Cell created: {cell}");
        Debug.Log($"Cell position: {cell.Position}");
        Debug.Log($"Cell blocked: {cell.IsBlocked}");
        Debug.Log($"Cell walkable: {cell.IsWalkable()}");

        cell.SetBlocked(true);

        Debug.Log("Cell blocked state changed to true.");

        Debug.Log($"Cell after blocked: {cell}");
        Debug.Log($"Cell blocked: {cell.IsBlocked}");
        Debug.Log($"Cell walkable: {cell.IsWalkable()}");
    }
}