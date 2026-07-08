public class Card
{
    private readonly int _id;
    private readonly string _name;
    private readonly string _description;
    private readonly CardType _cardType;
    private readonly TacticalCardEffect _effect;
    private readonly CardUseTiming _useTiming;

    public int ID => _id;
    public string Name => _name;
    public string Description => _description;
    public CardType CardType => _cardType;
    public TacticalCardEffect Effect => _effect;
    public CardUseTiming UseTiming => _useTiming;

    public Card()
    {
        _id = -1;
        _name = "";
        _description = "";
        _cardType = CardType.None;
        _effect = null;
        _useTiming = CardUseTiming.None;
    }

    public Card(int id, string name, string description, CardType cardType, TacticalCardEffect effect, CardUseTiming useTiming)
    {
        _id = id;
        _name = name;
        _description = description;
        _cardType = cardType;
        _effect = effect;
        _useTiming = useTiming;
    }

    public override bool Equals(object obj)
    {
        if (this == obj)
        {
            return true;
        }

        if (obj is not Card card)
        {
            return false;
        }

        return _id == card._id;
    }

    public override int GetHashCode()
    {
        return _id.GetHashCode();
    }
}