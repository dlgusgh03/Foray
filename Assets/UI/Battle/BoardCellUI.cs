using UnityEngine;

public class BoardCellUI : MonoBehaviour
{
    private BoardPosition _boardPosition;
    private BoardCell _boardCell;

    public BoardPosition BoardPosition => _boardPosition;

    public void Initialize(BoardPosition boardPosition, BoardCell boardCell)
    {
        _boardPosition = boardPosition;
        _boardCell = boardCell;
    }
}
