using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoardCellUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _pieceText;
    [SerializeField] private Color _playerPieceColor;
    [SerializeField] private Color _enemyPieceColor;
    [SerializeField] private Button _button;

    private BoardPosition _boardPosition;
    private BoardCell _boardCell;

    public BoardPosition BoardPosition => _boardPosition;

    public void Initialize(BoardPosition boardPosition, BoardCell boardCell)
    {
        _boardPosition = boardPosition;
        _boardCell = boardCell;

        _button.onClick.AddListener(OnClick);

        Piece piece = boardCell.Piece;

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

    private void OnClick()
    {
        Debug.Log($"Clicked cell: {_boardPosition}");
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
}
