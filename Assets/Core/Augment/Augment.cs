public class Augment
{
    private readonly int _id;
    private readonly string _name;
    private readonly string _description;
    private readonly AugmentRarity _rarity;
    private readonly AugmentEffect _effect;

    public int ID => _id;
    public string Name => _name;
    public string Description => _description;
    public AugmentRarity Rarity => _rarity;
    public AugmentEffect Effect => _effect;

    public Augment()
    {
        _id = -1;
        _name = "";
        _description = "";
        _rarity = AugmentRarity.None;
        _effect = null;
    }

    public Augment(int id, string name, string description, AugmentRarity rarity, AugmentEffect effect)
    {
        _id = id;
        _name = name;
        _description = description;
        _rarity = rarity;
        _effect = effect;
    }

    public override bool Equals(object obj)
    {
        if (this == obj)
        {
            return true;
        }

        if (obj is not Augment augment)
        {
            return false;
        }

        return _id == augment._id;
    }

    public override int GetHashCode()
    {
        return _id.GetHashCode();
    }
}