using System.Collections.Generic;

public static class TacticalCardCatalog
{
    public static List<TacticalCard> GetAllCards()
    {
        List<TacticalCard> cards = new List<TacticalCard>();

        cards.Add(new TacticalCard(
            0,
            "TacticalCard1",
            "1",
            CardType.Tactical,
            null,
            CardUseTiming.OnAcquire
        ));

        cards.Add(new TacticalCard(
            1,
            "TacticalCard2",
            "2",
            CardType.Tactical,
            null,
            CardUseTiming.BeforeBattle
        ));

        cards.Add(new TacticalCard(
            2,
            "TacticalCard3",
            "3",
            CardType.Tactical,
            null,
            CardUseTiming.BeforePlayerAction
        ));

        cards.Add(new TacticalCard(
            3,
            "TacticalCard4",
            "4",
            CardType.Tactical,
            null,
            CardUseTiming.Anytime
        ));

        cards.Add(new TacticalCard(
            4,
            "TacticalCard5",
            "5",
            CardType.Tactical,
            null,
            CardUseTiming.BeforeBattle
        ));

        cards.Add(new TacticalCard(
            5,
            "TacticalCard6",
            "6",
            CardType.Tactical,
            null,
            CardUseTiming.BeforePlayerAction
        ));

        cards.Add(new TacticalCard(
            6,
            "CursedCard1",
            "7",
            CardType.Cursed,
            null,
            CardUseTiming.OnAcquire
        ));

        cards.Add(new TacticalCard(
            7,
            "CursedCard2",
            "8",
            CardType.Cursed,
            null,
            CardUseTiming.BeforeBattle
        ));

        cards.Add(new TacticalCard(
            8,
            "CursedCard3",
            "9",
            CardType.Cursed,
            null,
            CardUseTiming.BeforePlayerAction
        ));

        cards.Add(new TacticalCard(
            9,
            "CursedCard4",
            "10",
            CardType.Cursed,
            null,
            CardUseTiming.Anytime
        ));

        cards.Add(new TacticalCard(
            10,
            "CursedCard5",
            "11",
            CardType.Cursed,
            null,
            CardUseTiming.BeforeBattle
        ));

        cards.Add(new TacticalCard(
            11,
            "CursedCard6",
            "12",
            CardType.Cursed,
            null,
            CardUseTiming.BeforePlayerAction
        ));

        return cards;
    }
}