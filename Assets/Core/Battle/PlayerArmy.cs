using System.Collections.Generic;

public class PlayerArmy
{
    private readonly List<PieceType> _ownedPieceTypes;
    private int _maxSlots;

    public int MaxSlots => _maxSlots;
    public int Count => _ownedPieceTypes.Count;

    public PlayerArmy()
    {
        _maxSlots = 3;
        _ownedPieceTypes = new List<PieceType>();

        _ownedPieceTypes.Add(PieceType.King);
        _ownedPieceTypes.Add(PieceType.Soldier);
        _ownedPieceTypes.Add(PieceType.Soldier);
    }

    public bool CanAddPiece()
    {
        return _ownedPieceTypes.Count < _maxSlots;
    }

    public bool AddPiece(PieceType type)
    {
        if (!CanAddPiece())
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

    public void IncreaseMaxSlots(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        _maxSlots += amount;
    }

    public List<PieceType> GetOwnedPieceTypes()
    {
        return new List<PieceType>(_ownedPieceTypes);
    }
}