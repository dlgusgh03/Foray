using System.Collections.Generic;
using UnityEngine;

public class CardDebugRunner : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("===== Card Chest Debug Start =====");

        TestSupplyChest(
            CardChestSize.Standard,
            3,
            "Standard Supply Chest"
        );

        TestSupplyChest(
            CardChestSize.Large,
            5,
            "Large Supply Chest"
        );

        TestCursedChest(
            CardChestSize.Standard,
            3,
            "Standard Cursed Chest"
        );

        TestCursedChest(
            CardChestSize.Large,
            5,
            "Large Cursed Chest"
        );

        TestSealedChest(
            CardChestSize.Standard,
            3,
            "Standard Sealed Chest"
        );

        TestSealedChest(
            CardChestSize.Large,
            5,
            "Large Sealed Chest"
        );

        TestSealedChestProbability();

        Debug.Log("===== Card Chest Debug End =====");
    }

    private void TestSupplyChest(
        CardChestSize chestSize,
        int expectedCardCount,
        string testName
    )
    {
        Debug.Log($"===== {testName} =====");

        CardChest chest = new CardChest(
            CardChestType.Supply,
            chestSize
        );

        List<TacticalCard> choices =
            chest.GenerateCardChoices();

        PrintCardChoices(choices);

        ValidateCardCount(
            choices,
            expectedCardCount,
            testName
        );

        ValidateCardType(
            choices,
            CardType.Tactical,
            testName
        );

        ValidateNoDuplicateCards(
            choices,
            testName
        );
    }

    private void TestCursedChest(
        CardChestSize chestSize,
        int expectedCardCount,
        string testName
    )
    {
        Debug.Log($"===== {testName} =====");

        CardChest chest = new CardChest(
            CardChestType.Cursed,
            chestSize
        );

        List<TacticalCard> choices =
            chest.GenerateCardChoices();

        PrintCardChoices(choices);

        ValidateCardCount(
            choices,
            expectedCardCount,
            testName
        );

        ValidateCardType(
            choices,
            CardType.Cursed,
            testName
        );

        ValidateNoDuplicateCards(
            choices,
            testName
        );
    }

    private void TestSealedChest(
        CardChestSize chestSize,
        int expectedCardCount,
        string testName
    )
    {
        Debug.Log($"===== {testName} =====");

        CardChest chest = new CardChest(
            CardChestType.Sealed,
            chestSize
        );

        List<TacticalCard> choices =
            chest.GenerateCardChoices();

        PrintCardChoices(choices);

        ValidateCardCount(
            choices,
            expectedCardCount,
            testName
        );

        ValidateSealedCardTypes(
            choices,
            testName
        );

        ValidateNoDuplicateCards(
            choices,
            testName
        );
    }

    private void TestSealedChestProbability()
    {
        Debug.Log("===== Sealed Chest Probability Test =====");

        const int testCount = 100;

        int totalCardCount = 0;
        int cursedCardCount = 0;

        for (int i = 0; i < testCount; i++)
        {
            CardChest chest = new CardChest(
                CardChestType.Sealed,
                CardChestSize.Large
            );

            List<TacticalCard> choices =
                chest.GenerateCardChoices();

            foreach (TacticalCard card in choices)
            {
                totalCardCount++;

                if (card.CardType == CardType.Cursed)
                {
                    cursedCardCount++;
                }
            }
        }

        float cursedRate =
            (float)cursedCardCount /
            totalCardCount *
            100f;

        Debug.Log(
            $"Total Cards: {totalCardCount} / " +
            $"Cursed Cards: {cursedCardCount} / " +
            $"Cursed Rate: {cursedRate:F2}%"
        );
    }

    private void PrintCardChoices(
        List<TacticalCard> choices
    )
    {
        Debug.Log("===== Card Choices =====");

        if (choices == null)
        {
            Debug.LogError("Card Choices are null");
            return;
        }

        for (int i = 0; i < choices.Count; i++)
        {
            TacticalCard card = choices[i];

            Debug.Log(
                $"[{i}] " +
                $"ID: {card.ID} / " +
                $"Name: {card.Name} / " +
                $"Type: {card.CardType} / " +
                $"UseTiming: {card.UseTiming}"
            );
        }
    }

    private void ValidateCardCount(
        List<TacticalCard> choices,
        int expectedCount,
        string testName
    )
    {
        if (
            choices != null &&
            choices.Count == expectedCount
        )
        {
            Debug.Log(
                $"{testName} Count Test: PASS"
            );
        }
        else
        {
            int actualCount =
                choices == null ? -1 : choices.Count;

            Debug.LogError(
                $"{testName} Count Test: FAIL / " +
                $"Expected: {expectedCount}, " +
                $"Actual: {actualCount}"
            );
        }
    }

    private void ValidateCardType(
        List<TacticalCard> choices,
        CardType expectedType,
        string testName
    )
    {
        if (choices == null)
        {
            Debug.LogError(
                $"{testName} Type Test: FAIL / " +
                $"Choices are null"
            );

            return;
        }

        foreach (TacticalCard card in choices)
        {
            if (card.CardType != expectedType)
            {
                Debug.LogError(
                    $"{testName} Type Test: FAIL / " +
                    $"Card: {card.Name}, " +
                    $"Expected: {expectedType}, " +
                    $"Actual: {card.CardType}"
                );

                return;
            }
        }

        Debug.Log(
            $"{testName} Type Test: PASS"
        );
    }

    private void ValidateSealedCardTypes(
        List<TacticalCard> choices,
        string testName
    )
    {
        if (choices == null)
        {
            Debug.LogError(
                $"{testName} Type Test: FAIL / " +
                $"Choices are null"
            );

            return;
        }

        foreach (TacticalCard card in choices)
        {
            if (
                card.CardType != CardType.Tactical &&
                card.CardType != CardType.Cursed
            )
            {
                Debug.LogError(
                    $"{testName} Type Test: FAIL / " +
                    $"Invalid Type: {card.CardType}"
                );

                return;
            }
        }

        Debug.Log(
            $"{testName} Type Test: PASS"
        );
    }

    private void ValidateNoDuplicateCards(
        List<TacticalCard> choices,
        string testName
    )
    {
        if (choices == null)
        {
            Debug.LogError(
                $"{testName} Duplicate Test: FAIL / " +
                $"Choices are null"
            );

            return;
        }

        HashSet<int> cardIDs = new HashSet<int>();

        foreach (TacticalCard card in choices)
        {
            if (!cardIDs.Add(card.ID))
            {
                Debug.LogError(
                    $"{testName} Duplicate Test: FAIL / " +
                    $"Duplicate ID: {card.ID}"
                );

                return;
            }
        }

        Debug.Log(
            $"{testName} Duplicate Test: PASS"
        );
    }
}