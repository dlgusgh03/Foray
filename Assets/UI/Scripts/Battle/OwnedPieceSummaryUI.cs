using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OwnedPieceSummaryUI : MonoBehaviour
{
    [SerializeField] private Transform pieceListRoot;
    [SerializeField] private PieceSummaryItemUI pieceSummaryItemPrefab;
    [SerializeField] private TMP_Text populationText;

    private readonly List<PieceSummaryItemUI> _spawnedItems =
        new List<PieceSummaryItemUI>();

    public void Refresh(PlayerArmy playerArmy)
    {
        ClearItems();

        if (playerArmy == null)
        {
            populationText.text = string.Empty;
            return;
        }

        AddPieceItem(playerArmy, PieceType.King);
        AddPieceItem(playerArmy, PieceType.Soldier);
        AddPieceItem(playerArmy, PieceType.Horse);
        AddPieceItem(playerArmy, PieceType.Cannon);
        AddPieceItem(playerArmy, PieceType.Chariot);

        populationText.text = $"Population {FormatPopulation(playerArmy.CurrentPopulation)}" + $" / {FormatPopulation(playerArmy.MaxPopulation)}";
    }

    private void AddPieceItem(
        PlayerArmy playerArmy,
        PieceType pieceType)
    {
        int count = playerArmy.GetCountByType(pieceType);

        if (count <= 0)
        {
            return;
        }

        PieceSummaryItemUI item =
            Instantiate(pieceSummaryItemPrefab, pieceListRoot);

        item.Setup(pieceType, count);
        _spawnedItems.Add(item);
    }

    private void ClearItems()
    {
        foreach (PieceSummaryItemUI item in _spawnedItems)
        {
            if (item != null)
            {
                Destroy(item.gameObject);
            }
        }

        _spawnedItems.Clear();
    }

    private string FormatPopulation(int population)
    {
        if (population % 2 == 0)
        {
            return (population / 2).ToString();
        }

        return $"{population / 2}.5";
    }
}