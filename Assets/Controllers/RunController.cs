using UnityEngine;
using System.Collections.Generic;

public class RunController : MonoBehaviour
{
    [SerializeField] private BoardUI _boardUI;

    private RunManager _runManager;
    private Piece _selectedPiece;
    private BoardPosition _selectedPosition;

    public RunManager RunManager => _runManager;

    private void Awake()
    {
        _runManager = new RunManager();
        _runManager.StartRun();
    }

    public bool OnCellClicked(BoardPosition position)
    {
        Board board = _runManager.CurrentBoard;
        Piece piece = board.GetPiece(position);

        if (piece == null)
        {
            Debug.Log("Empty Cell");
            return false;
        }

        if (piece.Owner == PieceOwner.Enemy)
        {
            Debug.Log($"Enemy Piece: {piece.Type}");
            return false;
        }

        _selectedPiece = piece;
        _selectedPosition = position;
        List<BoardPosition> movablePositions = MovementCalculator.GetMovablePositions(board, _selectedPiece);

        Debug.Log($"Selected Player {_selectedPiece.Type} at {_selectedPosition}");

        _boardUI.HighlightMovableCells(movablePositions);

        return true;
    }
}