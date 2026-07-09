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
}