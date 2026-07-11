using UnityEngine;
using System.Collections.Generic;

public class BoardUI : MonoBehaviour
{
    [SerializeField] private GameObject _boardCellPrefab;
    [SerializeField] private Transform _boardPanel;
    [SerializeField] private RunController _runController;

    private BoardCellUI _selectedCellUI;
    private Dictionary<BoardPosition, BoardCellUI> _cellUIs = new Dictionary<BoardPosition, BoardCellUI>();
    private List<BoardCellUI> _highlightedCells = new List<BoardCellUI>();

    void Start()
    {
        Board board = _runController.RunManager.CurrentBoard;
        CreateBoardUI(board);
    }

    private void CreateBoardUI(Board board)
    {
        for (int y = board.Height - 1; y >= 0; y--)
        {
            for (int x = 0; x < board.Width; x++)
            {
                BoardPosition position = new BoardPosition(x, y);
                BoardCell boardCell = board.GetCell(position);

                GameObject cellObject = Instantiate(_boardCellPrefab, _boardPanel);
                BoardCellUI cellUI = cellObject.GetComponent<BoardCellUI>();

                cellUI.Initialize(position, boardCell, _runController, this);
                _cellUIs.Add(position, cellUI);
            }
        }
    }

    public void SelectCell(BoardCellUI cellUI)
    {
        if (_selectedCellUI != null)
        {
            _selectedCellUI.SetSelected(false);
        }

        _selectedCellUI = cellUI;
        _selectedCellUI.SetSelected(true);
    }

    public BoardCellUI GetCellUI(BoardPosition position)
    {
        if (_cellUIs.TryGetValue(position, out BoardCellUI cellUI))
        {
            return cellUI;
        }

        return null;
    }

    public void HighlightMovableCells(List<BoardPosition> movablePositions)
    {
        foreach (BoardCellUI movableCellUI in _highlightedCells)
        {
            if (movableCellUI != null)
            {
                movableCellUI.SetMovable(false);
            }
        }

        _highlightedCells.Clear();

        foreach (BoardPosition position in movablePositions)
        {
            if (_cellUIs.TryGetValue(position, out BoardCellUI cellUI))
            {
                _highlightedCells.Add(cellUI);
                cellUI.SetMovable(true);
            }
        }
    }
}
