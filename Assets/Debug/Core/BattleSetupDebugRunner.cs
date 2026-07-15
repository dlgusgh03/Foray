using UnityEngine;

public class BattleSetupDebugRunner : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("===== BattleSetup Debug Start =====");

        TestCreateBasicBattle();
        TestPlayerCanActInBasicBattle();
        TestInvalidPlayerActBlocked();
        TestEnemyCanActAfterPlayerAct();
        TestEnemyActDoesNotCrash();

        Debug.Log("===== BattleSetup Debug End =====");
    }

    private void TestCreateBasicBattle()
    {
        Debug.Log("[Test 1] Create basic battle");

        BattleManager battleManager = BattleSetup.CreateBasicBattle();

        PrintResult("BattleManager should not be null", battleManager != null);
        PrintResult("Battle should not be over at start", battleManager.IsBattleOver() == false);
        PrintResult("Winner should be null at start", battleManager.GetWinner() == null);
        PrintResult("Current turn should be Player", battleManager.TurnManager.CurrentTurnOwner == PieceOwner.Player);
    }

    private void TestPlayerCanActInBasicBattle()
    {
        Debug.Log("[Test 2] Player can act in basic battle");

        BattleManager battleManager = BattleSetup.CreateBasicBattle();

        bool result = battleManager.PlayerAct(
            new BoardPosition(1, 0),
            new BoardPosition(1, 1)
        );

        PrintResult("PlayerAct should return true", result == true);
        PrintResult("Battle should not be over", battleManager.IsBattleOver() == false);
        PrintResult("Current turn should be Enemy", battleManager.TurnManager.CurrentTurnOwner == PieceOwner.Enemy);
    }

    private void TestInvalidPlayerActBlocked()
    {
        Debug.Log("[Test 3] Invalid player act should be blocked");

        BattleManager battleManager = BattleSetup.CreateBasicBattle();

        bool result = battleManager.PlayerAct(
            new BoardPosition(1, 0),
            new BoardPosition(1, 2)
        );

        PrintResult("Invalid PlayerAct should return false", result == false);
        PrintResult("Battle should not be over", battleManager.IsBattleOver() == false);
        PrintResult("Current turn should still be Player", battleManager.TurnManager.CurrentTurnOwner == PieceOwner.Player);
    }

    private void TestEnemyCanActAfterPlayerAct()
    {
        Debug.Log("[Test 4] Enemy can act after player act");

        BattleManager battleManager = BattleSetup.CreateBasicBattle();

        bool playerResult = battleManager.PlayerAct(
            new BoardPosition(1, 0),
            new BoardPosition(1, 1)
        );

        battleManager.EnemyAct();

        PrintResult("PlayerAct should return true", playerResult == true);
        PrintResult("Battle should not be over", battleManager.IsBattleOver() == false);
        PrintResult("Current turn should return to Player", battleManager.TurnManager.CurrentTurnOwner == PieceOwner.Player);
    }

    private void TestEnemyActDoesNotCrash()
    {
        Debug.Log("[Test 5] EnemyAct does not crash after player act");

        BattleManager battleManager = BattleSetup.CreateBasicBattle();

        bool playerResult = battleManager.PlayerAct(
            new BoardPosition(1, 0),
            new BoardPosition(1, 1)
        );

        battleManager.EnemyAct();

        PrintResult("PlayerAct should return true", playerResult == true);
        PrintResult("BattleManager should still exist", battleManager != null);
        PrintResult("Winner should be null or battle should be over naturally",
            battleManager.GetWinner() == null || battleManager.IsBattleOver());
    }

    private void PrintResult(string message, bool passed)
    {
        if (passed)
        {
            Debug.Log($"[PASS] {message}");
        }
        else
        {
            Debug.LogError($"[FAIL] {message}");
        }
    }
}