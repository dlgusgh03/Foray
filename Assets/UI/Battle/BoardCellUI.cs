using TMPro;
using UnityEngine;

public class BoardCellUI : MonoBehaviour
{
    private BoardPosition _boardPosition;
    private BoardCell _boardCell;

    [SerializeField] private TMP_Text _pieceText;
    [SerializeField] private Color _playerPieceColor;
    [SerializeField] private Color _enemyPieceColor;

    public BoardPosition BoardPosition => _boardPosition;

    public void Initialize(BoardPosition boardPosition, BoardCell boardCell)
    {
        _boardPosition = boardPosition;
        _boardCell = boardCell;

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
