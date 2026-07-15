using System.Collections.Generic;
using UnityEngine;

public class CardSelectionDebugRunner : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("===== CARD SELECTION DEBUG START =====");

        TestSelectNormalCard();
        TestSelectOnAcquireCard();
        TestSkipCardSelection();
        TestSelectNormalCardAtMaxCapacity();

        TestSellTacticalCard();
        TestSellCursedCard();
        TestSellCardWithInvalidIndex();
        TestSellCardDuringSelectionAndContinue();

        Debug.Log("===== CARD SELECTION DEBUG END =====");
    }

    private void TestSelectNormalCard()
    {
        Debug.Log("\n--- Test 1: Select Normal Card ---");

        RunManager runManager = new RunManager();
        runManager.StartRun();

        TacticalCard card =
            FindCardByTiming(CardUseTiming.BeforeBattle);

        if (card == null)
        {
            Debug.LogError("Normal card was not found.");
            return;
        }

        List<TacticalCard> choices = new List<TacticalCard>()
        {
            card
        };

        runManager.StartCardSelection(choices, 1);

        int previousOwnedCount =
            runManager.GetOwnedCards().Count;

        runManager.SelectCard(0);

        int currentOwnedCount =
            runManager.GetOwnedCards().Count;

        int currentChoiceCount =
            runManager.GetCurrentCardChoices().Count;

        bool result =
            currentOwnedCount == previousOwnedCount + 1 &&
            currentChoiceCount == 0 &&
            runManager.RemainingCardSelections == 0 &&
            runManager.CurrentState == RunState.Shop;

        Debug.Log($"Selected Card: {card.Name}");

        Debug.Log(
            $"Owned Card Count: " +
            $"{previousOwnedCount} -> {currentOwnedCount}"
        );

        Debug.Log(
            $"Choice Count: {currentChoiceCount} " +
            $"(Expected: 0)"
        );

        Debug.Log(
            $"Remaining Selections: " +
            $"{runManager.RemainingCardSelections} " +
            $"(Expected: 0)"
        );

        Debug.Log(
            $"Run State: {runManager.CurrentState} " +
            $"(Expected: Shop)"
        );

        Debug.Log($"Result: {result}");
    }

    private void TestSelectOnAcquireCard()
    {
        Debug.Log("\n--- Test 2: Select OnAcquire Card ---");

        RunManager runManager = new RunManager();
        runManager.StartRun();

        TacticalCard card =
            FindCardByTiming(CardUseTiming.OnAcquire);

        if (card == null)
        {
            Debug.LogError("OnAcquire card was not found.");
            return;
        }

        List<TacticalCard> choices = new List<TacticalCard>()
        {
            card
        };

        runManager.StartCardSelection(choices, 1);

        int previousOwnedCount =
            runManager.GetOwnedCards().Count;

        runManager.SelectCard(0);

        int currentOwnedCount =
            runManager.GetOwnedCards().Count;

        bool result =
            currentOwnedCount == previousOwnedCount &&
            runManager.GetCurrentCardChoices().Count == 0 &&
            runManager.RemainingCardSelections == 0 &&
            runManager.CurrentState == RunState.Shop;

        Debug.Log($"Selected Card: {card.Name}");

        Debug.Log(
            $"Owned Card Count: " +
            $"{previousOwnedCount} -> {currentOwnedCount} " +
            $"(Expected: No Change)"
        );

        Debug.Log(
            $"Remaining Selections: " +
            $"{runManager.RemainingCardSelections} " +
            $"(Expected: 0)"
        );

        Debug.Log(
            $"Run State: {runManager.CurrentState} " +
            $"(Expected: Shop)"
        );

        Debug.Log($"Result: {result}");
    }

    private void TestSkipCardSelection()
    {
        Debug.Log("\n--- Test 3: Skip Card Selection ---");

        RunManager runManager = new RunManager();
        runManager.StartRun();

        List<TacticalCard> cards =
            TacticalCardCatalog.GetAllCards();

        List<TacticalCard> choices = new List<TacticalCard>()
        {
            cards[0],
            cards[1],
            cards[2]
        };

        runManager.StartCardSelection(choices, 2);

        Debug.Log(
            $"Before Skip: " +
            $"Choices={runManager.GetCurrentCardChoices().Count}, " +
            $"Remaining={runManager.RemainingCardSelections}, " +
            $"State={runManager.CurrentState}"
        );

        runManager.SkipCardSelection();

        bool result =
            runManager.GetCurrentCardChoices().Count == 0 &&
            runManager.RemainingCardSelections == 0 &&
            runManager.CurrentState == RunState.Shop;

        Debug.Log(
            $"After Skip: " +
            $"Choices={runManager.GetCurrentCardChoices().Count}, " +
            $"Remaining={runManager.RemainingCardSelections}, " +
            $"State={runManager.CurrentState}"
        );

        Debug.Log($"Result: {result}");
    }

    private void TestSelectNormalCardAtMaxCapacity()
    {
        Debug.Log(
            "\n--- Test 4: Select Normal Card At Max Capacity ---"
        );

        RunManager runManager = new RunManager();
        runManager.StartRun();

        TacticalCard ownedCard =
            FindCardByTiming(CardUseTiming.BeforeBattle);

        TacticalCard extraCard =
            FindCardByTiming(CardUseTiming.BeforePlayerAction);

        if (ownedCard == null || extraCard == null)
        {
            Debug.LogError("Required test cards were not found.");
            return;
        }

        AddOwnedCardForDebug(runManager, ownedCard);
        AddOwnedCardForDebug(runManager, ownedCard);
        AddOwnedCardForDebug(runManager, ownedCard);

        List<TacticalCard> choices = new List<TacticalCard>()
        {
            extraCard
        };

        runManager.StartCardSelection(choices, 1);

        int previousOwnedCount =
            runManager.GetOwnedCards().Count;

        int previousChoiceCount =
            runManager.GetCurrentCardChoices().Count;

        int previousRemainingSelections =
            runManager.RemainingCardSelections;

        runManager.SelectCard(0);

        int currentOwnedCount =
            runManager.GetOwnedCards().Count;

        int currentChoiceCount =
            runManager.GetCurrentCardChoices().Count;

        int currentRemainingSelections =
            runManager.RemainingCardSelections;

        bool result =
            currentOwnedCount == previousOwnedCount &&
            currentChoiceCount == previousChoiceCount &&
            currentRemainingSelections ==
                previousRemainingSelections &&
            runManager.CurrentState ==
                RunState.CardSelection;

        Debug.Log(
            $"Owned Card Count: " +
            $"{previousOwnedCount} -> {currentOwnedCount} " +
            $"(Expected: No Change)"
        );

        Debug.Log(
            $"Choice Count: " +
            $"{previousChoiceCount} -> {currentChoiceCount} " +
            $"(Expected: No Change)"
        );

        Debug.Log(
            $"Remaining Selections: " +
            $"{previousRemainingSelections} -> " +
            $"{currentRemainingSelections} " +
            $"(Expected: No Change)"
        );

        Debug.Log(
            $"Run State: {runManager.CurrentState} " +
            $"(Expected: CardSelection)"
        );

        Debug.Log($"Result: {result}");

        runManager.SkipCardSelection();
    }

    private void TestSellTacticalCard()
    {
        Debug.Log("\n--- Test 5: Sell Tactical Card ---");

        RunManager runManager = new RunManager();
        runManager.StartRun();

        TacticalCard card =
            FindStorableCardByType(CardType.Tactical);

        if (card == null)
        {
            Debug.LogError("Storable tactical card was not found.");
            return;
        }

        AddOwnedCardForDebug(runManager, card);

        int? expectedPrice =
            TacticalCardCatalog.GetSellPrice(card.CardType);

        int previousOwnedCount =
            runManager.GetOwnedCards().Count;

        int previousGold = runManager.Gold;

        bool sellResult = runManager.SellCard(0);

        int currentOwnedCount =
            runManager.GetOwnedCards().Count;

        int currentGold = runManager.Gold;

        bool result =
            expectedPrice != null &&
            sellResult &&
            currentOwnedCount == previousOwnedCount - 1 &&
            currentGold == previousGold + expectedPrice.Value;

        Debug.Log($"Sold Card: {card.Name}");
        Debug.Log($"Card Type: {card.CardType}");
        Debug.Log($"Sell Result: {sellResult}");

        Debug.Log(
            $"Owned Card Count: " +
            $"{previousOwnedCount} -> {currentOwnedCount}"
        );

        Debug.Log(
            $"Gold: {previousGold} -> {currentGold} " +
            $"(Expected: {previousGold + expectedPrice})"
        );

        Debug.Log($"Result: {result}");
    }

    private void TestSellCursedCard()
    {
        Debug.Log("\n--- Test 6: Sell Cursed Card ---");

        RunManager runManager = new RunManager();
        runManager.StartRun();

        TacticalCard card =
            FindStorableCardByType(CardType.Cursed);

        if (card == null)
        {
            Debug.LogError("Storable cursed card was not found.");
            return;
        }

        AddOwnedCardForDebug(runManager, card);

        int? expectedPrice =
            TacticalCardCatalog.GetSellPrice(card.CardType);

        int previousOwnedCount =
            runManager.GetOwnedCards().Count;

        int previousGold = runManager.Gold;

        bool sellResult = runManager.SellCard(0);

        int currentOwnedCount =
            runManager.GetOwnedCards().Count;

        int currentGold = runManager.Gold;

        bool result =
            expectedPrice != null &&
            sellResult &&
            currentOwnedCount == previousOwnedCount - 1 &&
            currentGold == previousGold + expectedPrice.Value;

        Debug.Log($"Sold Card: {card.Name}");
        Debug.Log($"Card Type: {card.CardType}");
        Debug.Log($"Sell Result: {sellResult}");

        Debug.Log(
            $"Owned Card Count: " +
            $"{previousOwnedCount} -> {currentOwnedCount}"
        );

        Debug.Log(
            $"Gold: {previousGold} -> {currentGold} " +
            $"(Expected: {previousGold + expectedPrice})"
        );

        Debug.Log($"Result: {result}");
    }

    private void TestSellCardWithInvalidIndex()
    {
        Debug.Log("\n--- Test 7: Sell Card With Invalid Index ---");

        RunManager runManager = new RunManager();
        runManager.StartRun();

        TacticalCard card =
            FindStorableCardByType(CardType.Tactical);

        if (card == null)
        {
            Debug.LogError("Storable tactical card was not found.");
            return;
        }

        AddOwnedCardForDebug(runManager, card);

        int previousOwnedCount =
            runManager.GetOwnedCards().Count;

        int previousGold = runManager.Gold;

        bool negativeResult =
            runManager.SellCard(-1);

        bool outOfRangeResult =
            runManager.SellCard(999);

        bool result =
            !negativeResult &&
            !outOfRangeResult &&
            runManager.GetOwnedCards().Count ==
                previousOwnedCount &&
            runManager.Gold == previousGold;

        Debug.Log($"Negative Index Result: {negativeResult}");
        Debug.Log($"Out Of Range Result: {outOfRangeResult}");

        Debug.Log(
            $"Owned Card Count: " +
            $"{previousOwnedCount} -> " +
            $"{runManager.GetOwnedCards().Count} " +
            $"(Expected: No Change)"
        );

        Debug.Log(
            $"Gold: {previousGold} -> {runManager.Gold} " +
            $"(Expected: No Change)"
        );

        Debug.Log($"Result: {result}");
    }

    private void TestSellCardDuringSelectionAndContinue()
    {
        Debug.Log(
            "\n--- Test 8: Sell Card During Selection And Continue ---"
        );

        RunManager runManager = new RunManager();
        runManager.StartRun();

        TacticalCard ownedCard =
            FindStorableCardByType(CardType.Tactical);

        TacticalCard extraCard =
            FindCardByTiming(CardUseTiming.BeforePlayerAction);

        if (ownedCard == null || extraCard == null)
        {
            Debug.LogError("Required test cards were not found.");
            return;
        }

        AddOwnedCardForDebug(runManager, ownedCard);
        AddOwnedCardForDebug(runManager, ownedCard);
        AddOwnedCardForDebug(runManager, ownedCard);

        List<TacticalCard> choices = new List<TacticalCard>()
        {
            extraCard
        };

        runManager.StartCardSelection(choices, 1);

        runManager.SelectCard(0);

        bool selectionBlocked =
            runManager.GetOwnedCards().Count == 3 &&
            runManager.GetCurrentCardChoices().Count == 1 &&
            runManager.RemainingCardSelections == 1 &&
            runManager.CurrentState == RunState.CardSelection;

        int previousGold = runManager.Gold;

        bool sellResult = runManager.SellCard(0);

        bool slotCreated =
            sellResult &&
            runManager.GetOwnedCards().Count == 2 &&
            runManager.CurrentState == RunState.CardSelection;

        runManager.SelectCard(0);

        bool selectionCompleted =
            runManager.GetOwnedCards().Count == 3 &&
            runManager.GetCurrentCardChoices().Count == 0 &&
            runManager.RemainingCardSelections == 0 &&
            runManager.CurrentState == RunState.Shop;

        bool result =
            selectionBlocked &&
            slotCreated &&
            selectionCompleted &&
            runManager.Gold > previousGold;

        Debug.Log(
            $"Selection Blocked At Capacity: {selectionBlocked}"
        );

        Debug.Log($"Sell Result: {sellResult}");

        Debug.Log(
            $"Slot Created During Selection: {slotCreated}"
        );

        Debug.Log(
            $"Selection Completed After Selling: " +
            $"{selectionCompleted}"
        );

        Debug.Log(
            $"Final Owned Card Count: " +
            $"{runManager.GetOwnedCards().Count} " +
            $"(Expected: 3)"
        );

        Debug.Log(
            $"Final Run State: {runManager.CurrentState} " +
            $"(Expected: Shop)"
        );

        Debug.Log($"Result: {result}");
    }

    private void AddOwnedCardForDebug(
        RunManager runManager,
        TacticalCard card)
    {
        List<TacticalCard> choices = new List<TacticalCard>()
        {
            card
        };

        runManager.StartCardSelection(choices, 1);
        runManager.SelectCard(0);
    }

    private TacticalCard FindCardByTiming(
        CardUseTiming timing)
    {
        List<TacticalCard> cards =
            TacticalCardCatalog.GetAllCards();

        foreach (TacticalCard card in cards)
        {
            if (card.UseTiming == timing)
            {
                return card;
            }
        }

        return null;
    }

    private TacticalCard FindStorableCardByType(
        CardType cardType)
    {
        List<TacticalCard> cards =
            TacticalCardCatalog.GetAllCards();

        foreach (TacticalCard card in cards)
        {
            if (card.CardType != cardType)
            {
                continue;
            }

            if (card.UseTiming == CardUseTiming.OnAcquire)
            {
                continue;
            }

            return card;
        }

        return null;
    }
}