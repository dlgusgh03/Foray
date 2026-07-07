using System.Collections.Generic;
using UnityEngine;

public class RunManagerDebugRunner : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("===== RunManager Unlock Debug Start =====");

        RunManager runManager = new RunManager();

        Debug.Log("===== Before StartRun =====");
        PrintRunStatus(runManager);
        PrintUnlockedPieces(runManager);

        runManager.StartRun();

        Debug.Log("===== After StartRun =====");
        PrintRunStatus(runManager);
        PrintUnlockedPieces(runManager);

        Debug.Log("Expected: Soldier, Horse");
        ValidateUnlockState(
            runManager,
            expectCannon: false,
            expectChariot: false
        );

        for (int stage = 1; stage <= 6; stage++)
        {
            Debug.Log($"===== Stage {stage} Force Win =====");

            runManager.DebugForceBattleWin();

            PrintRunStatus(runManager);
            PrintUnlockedPieces(runManager);
            PrintShopItems(runManager.CurrentShop);

            if (stage < 3)
            {
                Debug.Log("Expected Unlocks: Soldier, Horse");

                ValidateUnlockState(
                    runManager,
                    expectCannon: false,
                    expectChariot: false
                );
            }
            else if (stage < 6)
            {
                Debug.Log("Expected Unlocks: Soldier, Horse, Cannon");

                ValidateUnlockState(
                    runManager,
                    expectCannon: true,
                    expectChariot: false
                );
            }
            else
            {
                Debug.Log("Expected Unlocks: Soldier, Horse, Cannon, Chariot");

                ValidateUnlockState(
                    runManager,
                    expectCannon: true,
                    expectChariot: true
                );
            }

            ValidateShopItems(runManager);

            if (stage < 6)
            {
                runManager.StartNextBattle();

                Debug.Log("StartNextBattle() Called");
                PrintRunStatus(runManager);
            }
        }

        Debug.Log("===== RunManager Unlock Debug End =====");
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
}