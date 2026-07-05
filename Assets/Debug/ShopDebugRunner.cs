using System.Collections.Generic;
using UnityEngine;

public class ShopDebugRunner : MonoBehaviour
{
    private void Start()
    {
        RunManager runManager = new RunManager();
        runManager.StartRun();

        Shop shop = new Shop(runManager);

        Debug.Log("===== SHOP DEBUG START =====");

        PrintState("Initial State", runManager);

        TestBuyWithoutGold(shop, runManager);
        TestBuyPopulation(shop, runManager);
        TestBuyPiece(shop, runManager);
        TestSellPiece(shop, runManager);
        TestInvalidSell(shop, runManager);
        TestKingSell(shop, runManager);
        TestBuyPopulationTwice(shop, runManager);

        Debug.Log("===== SHOP DEBUG END =====");
    }

    private void TestBuyWithoutGold(Shop shop, RunManager runManager)
    {
        Debug.Log("\n--- Test 1: Buy Piece Without Gold ---");

        bool result = shop.BuyPiece(PieceType.Soldier);

        Debug.Log($"Result: {result}");
        PrintState("After Buy Without Gold", runManager);
    }

    private void TestBuyPiece(Shop shop, RunManager runManager)
    {
        Debug.Log("\n--- Test 3: Buy Piece ---");

        bool result = shop.BuyPiece(PieceType.Soldier);

        Debug.Log($"Result: {result}");
        PrintState("After Buy Piece", runManager);
    }

    private void TestSellPiece(Shop shop, RunManager runManager)
    {
        Debug.Log("\n--- Test 4: Sell Piece ---");

        int sellIndex = runManager.PlayerArmy.PieceCount - 1;

        bool result = shop.SellPiece(sellIndex);

        Debug.Log($"Result: {result}");
        PrintState("After Sell Piece", runManager);
    }

    private void TestInvalidSell(Shop shop, RunManager runManager)
    {
        Debug.Log("\n--- Test 5: Sell Invalid Index ---");

        bool negativeResult = shop.SellPiece(-1);
        bool outOfRangeResult = shop.SellPiece(999);

        Debug.Log($"Negative Index Result: {negativeResult}");
        Debug.Log($"Out Of Range Result: {outOfRangeResult}");
        PrintState("After Invalid Sell", runManager);
    }

    private void TestKingSell(Shop shop, RunManager runManager)
    {
        Debug.Log("\n--- Test 6: Sell King ---");

        int kingIndex = FindPieceIndex(runManager.PlayerArmy, PieceType.King);

        bool result = shop.SellPiece(kingIndex);

        Debug.Log($"King Index: {kingIndex}");
        Debug.Log($"Result: {result}");
        PrintState("After Sell King Attempt", runManager);
    }

    private void TestBuyPopulation(Shop shop, RunManager runManager)
    {
        Debug.Log("\n--- Test 2: Buy Population ---");

        runManager.AddGold(100);

        int previousPrice = runManager.PopulationPrice;
        int previousMaxPopulation = runManager.PlayerArmy.MaxPopulation;

        bool result = shop.BuyPopulation();

        Debug.Log($"Result: {result}");
        Debug.Log($"Previous Price: {previousPrice}");
        Debug.Log($"Current Price: {runManager.PopulationPrice}");
        Debug.Log($"Previous Max Population: {previousMaxPopulation}");
        Debug.Log($"Current Max Population: {runManager.PlayerArmy.MaxPopulation}");

        PrintState("After Buy Population", runManager);
    }

    private void TestBuyPopulationTwice(Shop shop, RunManager runManager)
    {
        Debug.Log("\n--- Test 7: Buy Population Twice In Same Shop ---");

        int previousGold = runManager.Gold;
        int previousPrice = runManager.PopulationPrice;
        int previousMaxPopulation = runManager.PlayerArmy.MaxPopulation;

        bool result = shop.BuyPopulation();

        Debug.Log($"Result: {result}");
        Debug.Log($"Gold Changed: {previousGold} -> {runManager.Gold}");
        Debug.Log($"Price Changed: {previousPrice} -> {runManager.PopulationPrice}");
        Debug.Log(
            $"Max Population Changed: {previousMaxPopulation} -> {runManager.PlayerArmy.MaxPopulation}"
        );

        PrintState("After Second Population Buy Attempt", runManager);
    }

    private int FindPieceIndex(PlayerArmy playerArmy, PieceType type)
    {
        List<PieceType> pieces = playerArmy.GetOwnedPieceTypes();

        for (int i = 0; i < pieces.Count; i++)
        {
            if (pieces[i] == type)
            {
                return i;
            }
        }

        return -1;
    }

    private void PrintState(string title, RunManager runManager)
    {
        PlayerArmy playerArmy = runManager.PlayerArmy;
        List<PieceType> pieces = playerArmy.GetOwnedPieceTypes();

        Debug.Log(
            $"[{title}]\n" +
            $"Gold: {runManager.Gold}\n" +
            $"Population: {playerArmy.CurrentPopulation}/{playerArmy.MaxPopulation}\n" +
            $"Population Price: {runManager.PopulationPrice}\n" +
            $"Piece Count: {playerArmy.PieceCount}\n" +
            $"Pieces: {string.Join(", ", pieces)}"
        );
    }
}