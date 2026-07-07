using System.Collections.Generic;
using UnityEngine;

public class RunManagerDebugRunner : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("===== Augment Selection Debug Start =====");

        RunManager runManager = new RunManager();
        runManager.StartRun();

        Debug.Log("===== Initial State =====");
        PrintRunStatus(runManager);
        PrintOwnedAugments(runManager);

        ValidateState(
            runManager,
            RunState.Battle,
            "StartRun State Test"
        );

        Debug.Log("===== SelectAugment Before Selection Test =====");

        int ownedCountBeforeInvalidSelection =
            runManager.GetCurrentAugments().Count;

        runManager.SelectAugment(0);

        int ownedCountAfterInvalidSelection =
            runManager.GetCurrentAugments().Count;

        if (ownedCountBeforeInvalidSelection == ownedCountAfterInvalidSelection)
        {
            Debug.Log("Select Before AugmentSelection Test: PASS");
        }
        else
        {
            Debug.LogError("Select Before AugmentSelection Test: FAIL");
        }

        Debug.Log("===== First Boss Test =====");

        AdvanceToNextBoss(runManager);

        PrintRunStatus(runManager);
        PrintAugmentChoices(runManager);
        PrintOwnedAugments(runManager);

        ValidateState(
            runManager,
            RunState.AugmentSelection,
            "First Boss State Test"
        );

        ValidateAugmentChoices(runManager);

        Debug.Log("===== Invalid Selection Test =====");

        runManager.SelectAugment(-1);

        ValidateState(
            runManager,
            RunState.AugmentSelection,
            "Negative Index Test"
        );

        runManager.SelectAugment(
            runManager.GetAugmentChoices().Count
        );

        ValidateState(
            runManager,
            RunState.AugmentSelection,
            "Out Of Range Index Test"
        );

        Debug.Log("===== First Augment Selection Test =====");

        List<Augment> firstChoices =
            runManager.GetAugmentChoices();

        int firstSelectedID = firstChoices[0].ID;

        runManager.SelectAugment(0);

        PrintRunStatus(runManager);
        PrintOwnedAugments(runManager);
        PrintAugmentChoices(runManager);

        ValidateState(
            runManager,
            RunState.Shop,
            "After First Selection State Test"
        );

        ValidateSelectedAugment(
            runManager,
            firstSelectedID
        );

        ValidateChoicesCleared(runManager);

        Debug.Log("===== Second Boss Test =====");

        AdvanceToNextBoss(runManager);

        PrintRunStatus(runManager);
        PrintAugmentChoices(runManager);
        PrintOwnedAugments(runManager);

        ValidateState(
            runManager,
            RunState.AugmentSelection,
            "Second Boss State Test"
        );

        ValidateAugmentChoices(runManager);

        Debug.Log("===== Second Augment Selection Test =====");

        List<Augment> secondChoices =
            runManager.GetAugmentChoices();

        int secondSelectedID = secondChoices[0].ID;

        runManager.SelectAugment(0);

        PrintRunStatus(runManager);
        PrintOwnedAugments(runManager);

        ValidateState(
            runManager,
            RunState.Shop,
            "After Second Selection State Test"
        );

        ValidateSelectedAugment(
            runManager,
            secondSelectedID
        );

        ValidateChoicesCleared(runManager);

        Debug.Log("===== Augment Selection Debug End =====");
    }

    private void PrintShopItems(Shop shop)
    {
        Debug.Log("===== Shop Items =====");

        if (shop == null)
        {
            Debug.Log("Shop is null");
            return;
        }

        List<ShopItem> items = shop.GetItems();

        for (int i = 0; i < items.Count; i++)
        {
            ShopItem item = items[i];

            Debug.Log($"[{i}] {item.PieceType} / Price: {item.Price} / IsSold: {item.IsSold}");
        }
    }

    private void ForcePlayerWin(RunManager runManager)
    {
        BattleManager battleManager = runManager.CurrentBattleManager;

        if (battleManager == null)
        {
            Debug.LogError("Cannot force win: BattleManager is null");
            return;
        }

        Board board = battleManager.Board;

        Piece enemyKing = board.GetPiece(new BoardPosition(3, 6));

        if (enemyKing == null)
        {
            Debug.LogError("Cannot force win: Enemy king not found at (3, 6)");
            return;
        }

        enemyKing.Kill();

        Debug.Log("Enemy king killed directly for debug.");
    }

    private void PrintRunStatus(RunManager runManager)
    {
        Debug.Log("===== Run Status =====");
        Debug.Log($"Current State: {runManager.CurrentState}");
        Debug.Log($"Is Run Over: {runManager.IsRunOver}");
        Debug.Log($"Stage Index: {runManager.StageIndex}");
        Debug.Log($"Gold: {runManager.Gold}");
        Debug.Log($"Population Price: {runManager.PopulationPrice}");
        Debug.Log($"Player Army Current Population: {runManager.PlayerArmy.CurrentPopulation}");
        Debug.Log($"Player Army Max Population: {runManager.PlayerArmy.MaxPopulation}");
    }

    private void PrintShopStatus(RunManager runManager)
    {
        Debug.Log("===== Shop Status =====");

        if (runManager.CurrentShop == null)
        {
            Debug.Log("CurrentShop: null");
            return;
        }

        Debug.Log("CurrentShop: exists");
        Debug.Log($"Has Bought Population: {runManager.CurrentShop.HasBoughtPopulation}");
    }

    private void PrintUnlockedPieces(RunManager runManager)
    {
        Debug.Log("===== Unlocked Pieces =====");

        List<PieceType> unlockedPieceTypes =
            runManager.GetUnlockedPieceTypes();

        for (int i = 0; i < unlockedPieceTypes.Count; i++)
        {
            Debug.Log($"[{i}] {unlockedPieceTypes[i]}");
        }

        Debug.Log($"Unlocked Piece Count: {unlockedPieceTypes.Count}");
    }

    private void ValidateUnlockState(
    RunManager runManager,
    bool expectCannon,
    bool expectChariot
)
    {
        List<PieceType> unlockedPieceTypes =
            runManager.GetUnlockedPieceTypes();

        bool hasSoldier =
            unlockedPieceTypes.Contains(PieceType.Soldier);

        bool hasHorse =
            unlockedPieceTypes.Contains(PieceType.Horse);

        bool hasCannon =
            unlockedPieceTypes.Contains(PieceType.Cannon);

        bool hasChariot =
            unlockedPieceTypes.Contains(PieceType.Chariot);

        bool passed =
            hasSoldier &&
            hasHorse &&
            hasCannon == expectCannon &&
            hasChariot == expectChariot;

        if (passed)
        {
            Debug.Log("Unlock State Test: PASS");
        }
        else
        {
            Debug.LogError(
                $"Unlock State Test: FAIL / " +
                $"Soldier: {hasSoldier}, " +
                $"Horse: {hasHorse}, " +
                $"Cannon: {hasCannon}, " +
                $"Chariot: {hasChariot}"
            );
        }
    }

    private void ValidateShopItems(RunManager runManager)
    {
        Shop shop = runManager.CurrentShop;

        if (shop == null)
        {
            Debug.LogError("Shop Items Test: FAIL / CurrentShop is null");
            return;
        }

        List<PieceType> unlockedPieceTypes =
            runManager.GetUnlockedPieceTypes();

        List<ShopItem> items = shop.GetItems();

        if (items.Count != unlockedPieceTypes.Count)
        {
            Debug.LogError(
                $"Shop Items Test: FAIL / " +
                $"Unlocked Count: {unlockedPieceTypes.Count}, " +
                $"Shop Item Count: {items.Count}"
            );

            return;
        }

        for (int i = 0; i < unlockedPieceTypes.Count; i++)
        {
            if (items[i].PieceType != unlockedPieceTypes[i])
            {
                Debug.LogError(
                    $"Shop Items Test: FAIL / " +
                    $"Index {i}, " +
                    $"Expected: {unlockedPieceTypes[i]}, " +
                    $"Actual: {items[i].PieceType}"
                );

                return;
            }
        }

        Debug.Log("Shop Items Test: PASS");
    }

    private void PrintArmy(PlayerArmy playerArmy)
    {
        Debug.Log("===== Player Army =====");

        var pieces = playerArmy.GetOwnedPieceTypes();

        for (int i = 0; i < pieces.Count; i++)
        {
            Debug.Log($"[{i}] {pieces[i]}");
        }

        Debug.Log($"Piece Count: {playerArmy.PieceCount}");
        Debug.Log($"Current Population: {playerArmy.CurrentPopulation}");
        Debug.Log($"Max Population: {playerArmy.MaxPopulation}");
    }

    private void PrintBattleStatus(BattleManager battleManager)
    {
        Debug.Log("===== Battle Status =====");

        if (battleManager == null)
        {
            Debug.LogError("BattleManager is null");
            return;
        }

        PieceOwner? winner = battleManager.GetWinner();
        string winnerText = winner.HasValue ? winner.Value.ToString() : "None";

        Debug.Log($"Current Turn: {battleManager.TurnManager.CurrentTurnOwner}");
        Debug.Log($"Is Battle Over: {battleManager.IsBattleOver()}");
        Debug.Log($"Winner: {winnerText}");
    }

    private void PrintBoard(Board board)
    {
        Debug.Log("===== Board State =====");

        for (int y = board.Height - 1; y >= 0; y--)
        {
            string line = "";

            for (int x = 0; x < board.Width; x++)
            {
                BoardPosition position = new BoardPosition(x, y);
                BoardCell cell = board.GetCell(position);

                if (cell == null)
                {
                    line += "? ";
                    continue;
                }

                if (cell.IsBlocked)
                {
                    line += "X ";
                    continue;
                }

                Piece piece = cell.Piece;

                if (piece == null)
                {
                    line += ". ";
                    continue;
                }

                line += GetPieceSymbol(piece) + " ";
            }

            Debug.Log(line);
        }

        Debug.Log("Legend: PK=Player King, PS=Player Soldier, PC=Player Chariot, PH=Player Horse, PN=Player Cannon");
        Debug.Log("Legend: EK=Enemy King, ES=Enemy Soldier, EC=Enemy Chariot, EH=Enemy Horse, EN=Enemy Cannon, X=Blocked, .=Empty");
    }

    private void PrintPieces(Board board)
    {
        Debug.Log("===== Piece List =====");

        int playerPieceCount = 0;
        int enemyPieceCount = 0;

        for (int x = 0; x < board.Width; x++)
        {
            for (int y = 0; y < board.Height; y++)
            {
                BoardPosition position = new BoardPosition(x, y);
                Piece piece = board.GetPiece(position);

                if (piece == null)
                {
                    continue;
                }

                if (piece.Owner == PieceOwner.Player)
                {
                    playerPieceCount++;
                }
                else if (piece.Owner == PieceOwner.Enemy)
                {
                    enemyPieceCount++;
                }

                Debug.Log(piece.ToString());
            }
        }

        Debug.Log($"Player Piece Count: {playerPieceCount}");
        Debug.Log($"Enemy Piece Count: {enemyPieceCount}");
    }

    private string GetPieceSymbol(Piece piece)
    {
        if (piece.Owner == PieceOwner.Player)
        {
            switch (piece.Type)
            {
                case PieceType.King:
                    return "PK";
                case PieceType.Soldier:
                    return "PS";
                case PieceType.Chariot:
                    return "PC";
                case PieceType.Horse:
                    return "PH";
                case PieceType.Cannon:
                    return "PN";
            }
        }

        if (piece.Owner == PieceOwner.Enemy)
        {
            switch (piece.Type)
            {
                case PieceType.King:
                    return "EK";
                case PieceType.Soldier:
                    return "ES";
                case PieceType.Chariot:
                    return "EC";
                case PieceType.Horse:
                    return "EH";
                case PieceType.Cannon:
                    return "EN";
            }
        }

        return "??";
    }

    private void AdvanceToNextBoss(RunManager runManager)
    {
        if (runManager.CurrentState == RunState.Shop)
        {
            runManager.StartNextBattle();
        }

        int safetyCount = 0;

        while (
            runManager.CurrentState == RunState.Battle &&
            safetyCount < 10
        )
        {
            Debug.Log(
                $"Force Win / " +
                $"Stage: {runManager.StageIndex}, " +
                $"Round: {runManager.RoundIndex}"
            );

            runManager.DebugForceBattleWin();

            PrintRunStatus(runManager);

            if (runManager.CurrentState == RunState.Shop)
            {
                runManager.StartNextBattle();
            }

            safetyCount++;
        }

        if (safetyCount >= 10)
        {
            Debug.LogError(
                "AdvanceToNextBoss Test: FAIL / Safety limit reached"
            );
        }
    }

    private void PrintAugmentChoices(RunManager runManager)
    {
        Debug.Log("===== Augment Choices =====");

        List<Augment> choices =
            runManager.GetAugmentChoices();

        if (choices.Count == 0)
        {
            Debug.Log("No Augment Choices");
            return;
        }

        for (int i = 0; i < choices.Count; i++)
        {
            Augment augment = choices[i];

            Debug.Log(
                $"[{i}] " +
                $"ID: {augment.ID} / " +
                $"Name: {augment.Name} / " +
                $"Rarity: {augment.Rarity}"
            );
        }

        Debug.Log($"Choice Count: {choices.Count}");
    }

    private void PrintOwnedAugments(RunManager runManager)
    {
        Debug.Log("===== Owned Augments =====");

        List<Augment> augments =
            runManager.GetCurrentAugments();

        if (augments.Count == 0)
        {
            Debug.Log("No Owned Augments");
            return;
        }

        for (int i = 0; i < augments.Count; i++)
        {
            Augment augment = augments[i];

            Debug.Log(
                $"[{i}] " +
                $"ID: {augment.ID} / " +
                $"Name: {augment.Name} / " +
                $"Rarity: {augment.Rarity}"
            );
        }

        Debug.Log($"Owned Augment Count: {augments.Count}");
    }

    private void ValidateState(
    RunManager runManager,
    RunState expectedState,
    string testName
)
    {
        if (runManager.CurrentState == expectedState)
        {
            Debug.Log($"{testName}: PASS");
        }
        else
        {
            Debug.LogError(
                $"{testName}: FAIL / " +
                $"Expected: {expectedState}, " +
                $"Actual: {runManager.CurrentState}"
            );
        }
    }

    private void ValidateAugmentChoices(RunManager runManager)
    {
        List<Augment> choices =
            runManager.GetAugmentChoices();

        List<Augment> ownedAugments =
            runManager.GetCurrentAugments();

        HashSet<int> choiceIDs = new HashSet<int>();

        foreach (Augment choice in choices)
        {
            if (!choiceIDs.Add(choice.ID))
            {
                Debug.LogError(
                    $"Augment Choices Test: FAIL / " +
                    $"Duplicate Choice ID: {choice.ID}"
                );

                return;
            }

            foreach (Augment ownedAugment in ownedAugments)
            {
                if (choice.ID == ownedAugment.ID)
                {
                    Debug.LogError(
                        $"Augment Choices Test: FAIL / " +
                        $"Already Owned Augment Appeared: {choice.Name}"
                    );

                    return;
                }
            }
        }

        if (choices.Count == 0)
        {
            Debug.LogError(
                "Augment Choices Test: FAIL / No choices generated"
            );

            return;
        }

        Debug.Log("Augment Choices Test: PASS");
    }

    private void ValidateSelectedAugment(
    RunManager runManager,
    int selectedID
)
    {
        List<Augment> ownedAugments =
            runManager.GetCurrentAugments();

        foreach (Augment augment in ownedAugments)
        {
            if (augment.ID == selectedID)
            {
                Debug.Log("Selected Augment Test: PASS");
                return;
            }
        }

        Debug.LogError(
            $"Selected Augment Test: FAIL / " +
            $"Selected ID {selectedID} not found"
        );
    }

    private void ValidateChoicesCleared(RunManager runManager)
    {
        if (runManager.GetAugmentChoices().Count == 0)
        {
            Debug.Log("Augment Choices Clear Test: PASS");
        }
        else
        {
            Debug.LogError("Augment Choices Clear Test: FAIL");
        }
    }
}