using System.Collections.Generic;
using UnityEngine;

public class RunManagerDebugRunner : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("===== RunManager Debug Start =====");

        RunManager runManager = new RunManager();

        Debug.Log("RunManager Created");
        PrintRunStatus(runManager);
        PrintShopStatus(runManager);

        runManager.StartRun();

        Debug.Log("StartRun() Called");
        PrintRunStatus(runManager);
        PrintShopStatus(runManager);

        if (runManager.CurrentBoard == null)
        {
            Debug.LogError("CurrentBoard is null");
            return;
        }

        if (runManager.CurrentBattleManager == null)
        {
            Debug.LogError("CurrentBattleManager is null");
            return;
        }

        PrintBoard(runManager.CurrentBoard);
        PrintPieces(runManager.CurrentBoard);
        PrintBattleStatus(runManager.CurrentBattleManager);

        Debug.Log("===== ResolveCurrentBattle Before Battle Over Test =====");

        runManager.ResolveCurrentBattle();

        Debug.Log("ResolveCurrentBattle() Called Before Battle Over");
        PrintRunStatus(runManager);
        PrintShopStatus(runManager);
        PrintBattleStatus(runManager.CurrentBattleManager);

        Debug.Log("===== Force Win Battle Test =====");

        runManager.DebugForceBattleWin();

        Debug.Log("DebugForceBattleWin() Called");
        PrintRunStatus(runManager);
        PrintShopStatus(runManager);
        PrintShopItems(runManager.CurrentShop);

        Debug.Log("===== Shop Test =====");

        if (runManager.CurrentShop == null)
        {
            Debug.LogError("CurrentShop is null after battle win");
            return;
        }

        Debug.Log("AddGold(20) for Shop Debug");
        runManager.AddGold(20);
        PrintRunStatus(runManager);
        PrintShopStatus(runManager);
        PrintShopItems(runManager.CurrentShop);

        bool boughtItem0 = runManager.CurrentShop.BuyItem(0);
        Debug.Log($"BuyItem(0): {boughtItem0}");
        PrintRunStatus(runManager);
        PrintArmy(runManager.PlayerArmy);
        PrintShopItems(runManager.CurrentShop);

        bool boughtItem0Again = runManager.CurrentShop.BuyItem(0);
        Debug.Log($"BuyItem(0) again should be false: {boughtItem0Again}");
        PrintRunStatus(runManager);
        PrintArmy(runManager.PlayerArmy);
        PrintShopItems(runManager.CurrentShop);

        bool boughtItem1 = runManager.CurrentShop.BuyItem(1);
        Debug.Log($"BuyItem(1): {boughtItem1}");
        PrintRunStatus(runManager);
        PrintArmy(runManager.PlayerArmy);
        PrintShopItems(runManager.CurrentShop);

        bool boughtInvalidItem = runManager.CurrentShop.BuyItem(999);
        Debug.Log($"BuyItem(999) should be false: {boughtInvalidItem}");

        bool soldKing = runManager.CurrentShop.SellPiece(0);
        Debug.Log($"SellPiece(0 / King) should be false: {soldKing}");
        PrintRunStatus(runManager);
        PrintArmy(runManager.PlayerArmy);

        bool soldPiece = runManager.CurrentShop.SellPiece(1);
        Debug.Log($"SellPiece(1) should be true: {soldPiece}");
        PrintRunStatus(runManager);
        PrintArmy(runManager.PlayerArmy);

        bool boughtPopulationFirst = runManager.CurrentShop.BuyPopulation();
        Debug.Log($"BuyPopulation() first should be true: {boughtPopulationFirst}");
        PrintRunStatus(runManager);
        PrintShopStatus(runManager);

        bool boughtPopulationSecond = runManager.CurrentShop.BuyPopulation();
        Debug.Log($"BuyPopulation() second should be false: {boughtPopulationSecond}");
        PrintRunStatus(runManager);
        PrintShopStatus(runManager);

        Debug.Log("===== Start Next Battle Test =====");

        runManager.StartNextBattle();

        Debug.Log("StartNextBattle() Called");
        PrintRunStatus(runManager);
        PrintShopStatus(runManager);

        if (runManager.CurrentBoard == null)
        {
            Debug.LogError("CurrentBoard is null after StartNextBattle");
            return;
        }

        if (runManager.CurrentBattleManager == null)
        {
            Debug.LogError("CurrentBattleManager is null after StartNextBattle");
            return;
        }

        PrintBoard(runManager.CurrentBoard);
        PrintPieces(runManager.CurrentBoard);
        PrintBattleStatus(runManager.CurrentBattleManager);

        Debug.Log("===== RunManager Debug End =====");
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