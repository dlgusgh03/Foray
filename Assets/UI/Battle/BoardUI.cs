using UnityEngine;

public class BoardUI : MonoBehaviour
{
    [SerializeField] private GameObject _boardCellPrefab;
    [SerializeField] private Transform _boardPanel;
    [SerializeField] private RunController _runController;


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

                cellUI.Initialize(position, boardCell);
            }
        }
    }
}
