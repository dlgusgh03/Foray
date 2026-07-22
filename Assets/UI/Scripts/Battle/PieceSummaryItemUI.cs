using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PieceSummaryItemUI : MonoBehaviour
{
    [SerializeField] private Image pieceIcon;
    [SerializeField] private TMP_Text countText;

    private PieceType _pieceType;

    public PieceType PieceType => _pieceType;

    public void Setup(PieceType pieceType, int count, Sprite icon = null)
    {
        _pieceType = pieceType;
        countText.text = count.ToString();

        if (icon != null)
        {
            pieceIcon.sprite = icon;
            pieceIcon.enabled = true;
        }
        else
        {
            pieceIcon.sprite = null;
            pieceIcon.enabled = true;
        }
    }
}