using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoardCellUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _pieceText;
    [SerializeField] private Button _button;
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Color _playerPieceColor;
    [SerializeField] private Color _enemyPieceColor;
    [SerializeField] private Color _normalColor;
    [SerializeField] private Color _selectedColor;
    [SerializeField] private Color _movableColor;

    private BoardPosition _boardPosition;
    private BoardCell _boardCell;
    private RunController _runController;
    private BoardUI _boardUI;

    public BoardPosition BoardPosition => _boardPosition;

    public void Initialize(BoardPosition boardPosition, BoardCell boardCell, RunController runController, BoardUI boardUI)
    {
        _boardPosition = boardPosition;
        _boardCell = boardCell;
        _runController = runController;
        _boardUI = boardUI;

        _button.onClick.AddListener(OnClick);

        Refresh();
    }

    public void Refresh()
    {
        Piece piece = _boardCell.Piece;

        if (piece == null)
        {
            _pieceText.text = "";
            return;
        }

        _pieceText.text = GetPieceText(piece.Type);

        if (piece.Owner == PieceOwner.Player)
        {
            _pieceText.color = _playerPieceColor;
        }
        else
        {
            _pieceText.color = _enemyPieceColor;
        }
    }

    private string GetPieceText(PieceType pieceType)
    {
        switch (pieceType)
        {
            case PieceType.King:
                return "K";

            case PieceType.Soldier:
                return "S";

            case PieceType.Horse:
                return "H";

            case PieceType.Cannon:
                return "C";

            case PieceType.Chariot:
                return "R";

            default:
                return "";
        }
    }

    private void OnClick()
    {
        bool isSelected = _runController.OnCellClicked(_boardPosition);

        if (isSelected)
        {
            _boardUI.SelectCell(this);
        }
    }

    public void SetSelected(bool isSelected)
    {
        _backgroundImage.color = isSelected ? _selectedColor : _normalColor;
    }

    public void SetMovable(bool isMovable)
    {
        _backgroundImage.color = isMovable ? _movableColor : _normalColor;
    }
}
