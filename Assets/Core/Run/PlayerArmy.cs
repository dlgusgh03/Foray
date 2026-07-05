using NUnit.Framework.Internal;
using System.Collections.Generic;

public class PlayerArmy
{
    private readonly List<PieceType> _ownedPieceTypes;
    private int _maxPopulation;

    public int MaxPopulation => _maxPopulation;
    public int PieceCount => _ownedPieceTypes.Count;
    public int CurrentPopulation
    {
        get
        {
            int sum = 0;

            foreach (PieceType piece in _ownedPieceTypes)
            {
                sum += PieceCatalog.GetPopulationCost(piece);
            }

            return sum;
        }
    }

    public PlayerArmy()
    {
        _maxPopulation = 6;
        _ownedPieceTypes = new List<PieceType>();

        _ownedPieceTypes.Add(PieceType.King);
        _ownedPieceTypes.Add(PieceType.Soldier);
        _ownedPieceTypes.Add(PieceType.Soldier);
    }

    public bool CanAddPiece(PieceType type)
    {
        int cost = PieceCatalog.GetPopulationCost(type);

        return CurrentPopulation + cost <= _maxPopulation;
    }

    public bool AddPiece(PieceType type)
    {
        if (!CanAddPiece(type))
        {
            return false;
        }

        _ownedPieceTypes.Add(type);

        return true;
    }

    public bool RemovePieceAt(int index)
    {
        if (index < 0 || index >= _ownedPieceTypes.Count)
        {
            return false;
        }

        if (_ownedPieceTypes[index] == PieceType.King)
        {
            return false;
        }

        _ownedPieceTypes.RemoveAt(index);

        return true;
    }

    public void IncreaseMaxPopulation(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        _maxPopulation += amount;
    }

    public List<PieceType> GetOwnedPieceTypes()
    {
        return new List<PieceType>(_ownedPieceTypes);
    }
}