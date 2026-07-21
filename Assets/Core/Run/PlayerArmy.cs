using System.Collections.Generic;

public class PlayerArmy
{
    private int _nextInstanceId;
    private readonly List<ArmyPiece> _ownedPieces;
    private int _maxPopulation;

    public int MaxPopulation => _maxPopulation;
    public int PieceCount => _ownedPieces.Count;

    public int CurrentPopulation
    {
        get
        {
            int sum = 0;

            foreach (ArmyPiece piece in _ownedPieces)
            {
                sum += PieceCatalog.GetPopulationCost(piece.Type);
            }

            return sum;
        }
    }

    public PlayerArmy()
    {
        _maxPopulation = 6;
        _nextInstanceId = 0;
        _ownedPieces = new List<ArmyPiece>();

        AddInitialPiece(PieceType.King);
        AddInitialPiece(PieceType.Soldier);
        AddInitialPiece(PieceType.Soldier);
    }

    public bool CanAddPiece(PieceType type)
    {
        int cost = PieceCatalog.GetPopulationCost(type);
        return CurrentPopulation + cost <= _maxPopulation;
    }

    public ArmyPiece AddPiece(PieceType type)
    {
        if (!CanAddPiece(type))
        {
            return null;
        }

        ArmyPiece piece = CreatePiece(type);
        _ownedPieces.Add(piece);

        return piece;
    }

    public bool RemovePieceById(int instanceId)
    {
        ArmyPiece piece = GetPieceById(instanceId);

        if (piece == null || piece.Type == PieceType.King)
        {
            return false;
        }

        return _ownedPieces.Remove(piece);
    }

    public ArmyPiece GetPieceById(int instanceId)
    {
        foreach (ArmyPiece piece in _ownedPieces)
        {
            if (piece.InstanceId == instanceId)
            {
                return piece;
            }
        }

        return null;
    }

    public List<ArmyPiece> GetOwnedPieces()
    {
        return new List<ArmyPiece>(_ownedPieces);
    }

    public List<PieceType> GetOwnedPieceTypes()
    {
        List<PieceType> types = new List<PieceType>();

        foreach (ArmyPiece piece in _ownedPieces)
        {
            types.Add(piece.Type);
        }

        return types;
    }

    public int GetCountByType(PieceType type)
    {
        int count = 0;

        foreach (ArmyPiece piece in _ownedPieces)
        {
            if (piece.Type == type)
            {
                count++;
            }
        }

        return count;
    }

    public void IncreaseMaxPopulation(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        _maxPopulation += amount;
    }

    private ArmyPiece CreatePiece(PieceType type)
    {
        ArmyPiece piece = new ArmyPiece(type, _nextInstanceId);
        _nextInstanceId++;

        return piece;
    }

    private void AddInitialPiece(PieceType type)
    {
        _ownedPieces.Add(CreatePiece(type));
    }
}