using System.Collections.Generic;

public class CardChest
{
    private const int CursedCardChance = 10;

    private readonly CardChestType _chestType;
    private readonly CardChestSize _chestSize;
    private readonly int _cardsPerChest;
    private readonly int _selectableCardCount;

    public CardChestType ChestType => _chestType;
    public CardChestSize ChestSize => _chestSize;
    public int CardsPerChest => _cardsPerChest;
    public int SelectableCardCount => _selectableCardCount;

    public CardChest(CardChestType chestType, CardChestSize chestSize)
    {
        _chestType = chestType;
        _chestSize = chestSize;
        
        if(_chestSize == CardChestSize.Standard)
        {
            _cardsPerChest = 3;
            _selectableCardCount = 1;
        }
        
        else if(_chestSize == CardChestSize.Large)
        {
            _cardsPerChest = 5;
            _selectableCardCount = 2;
        }
    }

    public List<TacticalCard> GenerateCardChoices()
    {
        List<TacticalCard> cards = TacticalCardCatalog.GetAllCards();
        List<TacticalCard> availableCards = new List<TacticalCard>();
        List<TacticalCard> cardChoices = new List<TacticalCard>();

        if (_chestType == CardChestType.Supply)
        {
            foreach(TacticalCard card in cards)
            {
                if(card.CardType == CardType.Tactical)
                {
                    availableCards.Add(card);
                }
            }

            for (int i = 0; i < _cardsPerChest; ++i)
            {
                int randomIndex = UnityEngine.Random.Range(0, availableCards.Count);
                TacticalCard randomCard = availableCards[randomIndex];
                availableCards.RemoveAt(randomIndex);
                cardChoices.Add(randomCard);
            }
        }

        else if(_chestType == CardChestType.Sealed)
        {
            List<TacticalCard> tacticalCards = new List<TacticalCard>();
            List<TacticalCard> cursedCards = new List<TacticalCard>();

            foreach (TacticalCard card in cards)
            {
                if (card.CardType == CardType.Tactical)
                {
                    tacticalCards.Add(card);
                }

                else if (card.CardType == CardType.Cursed)
                {
                    cursedCards.Add(card);
                }
            }

            for (int i = 0; i < _cardsPerChest; ++i)
            {
                int randomValue = UnityEngine.Random.Range(0, 100);

                if (randomValue < CursedCardChance)
                {
                    int randomIndex = UnityEngine.Random.Range(0, cursedCards.Count);
                    TacticalCard randomCard = cursedCards[randomIndex];
                    cursedCards.RemoveAt(randomIndex);
                    cardChoices.Add(randomCard);
                }
                else
                {
                    int randomIndex = UnityEngine.Random.Range(0, tacticalCards.Count);
                    TacticalCard randomCard = tacticalCards[randomIndex];
                    tacticalCards.RemoveAt(randomIndex);
                    cardChoices.Add(randomCard);
                }
            }
        }

        else if(_chestType == CardChestType.Cursed)
        {
            foreach (TacticalCard card in cards)
            {
                if (card.CardType == CardType.Cursed)
                {
                    availableCards.Add(card);
                }
            }

            for (int i = 0; i < _cardsPerChest; ++i)
            {
                int randomIndex = UnityEngine.Random.Range(0, availableCards.Count);
                TacticalCard randomCard = availableCards[randomIndex];
                availableCards.RemoveAt(randomIndex);
                cardChoices.Add(randomCard);
            }
        }

        else
        {
            return null;
        }

        return cardChoices;
    }
}