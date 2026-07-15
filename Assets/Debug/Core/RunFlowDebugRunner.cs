#if UNITY_EDITOR

using UnityEngine;

public class RunFlowDebugRunner : MonoBehaviour
{
    private int _passedCount;
    private int _failedCount;

    private void Start()
    {
        Debug.Log("===== Run Flow Integration Test Start =====");

        _passedCount = 0;
        _failedCount = 0;

        RunManager runManager = new RunManager();
        runManager.StartRun();

        if (!ValidateBattleState(runManager, 1, 1, "StartRun"))
        {
            PrintResult();
            return;
        }

        for (int stageIndex = 1; stageIndex <= 10; stageIndex++)
        {
            for (int roundIndex = 1; roundIndex <= 3; roundIndex++)
            {
                if (!ValidateBattleState(
                        runManager,
                        stageIndex,
                        roundIndex,
                        $"Before {stageIndex}-{roundIndex} Win"))
                {
                    PrintResult();
                    return;
                }

                ForcePlayerWinThroughBattleResult(runManager);

                if (stageIndex == 10 && roundIndex == 3)
                {
                    ValidateFinalClear(runManager);
                    ValidateProgressBlockedAfterClear(runManager);

                    PrintResult();
                    return;
                }

                if (roundIndex < 3)
                {
                    if (!ValidateNormalWinFlow(
                            runManager,
                            stageIndex,
                            roundIndex + 1))
                    {
                        PrintResult();
                        return;
                    }

                    runManager.LeaveShop();

                    if (!ValidateBattleState(
                            runManager,
                            stageIndex,
                            roundIndex + 1,
                            $"Start {stageIndex}-{roundIndex + 1}"))
                    {
                        PrintResult();
                        return;
                    }
                }
                else
                {
                    if (!ValidateBossWinFlow(
                            runManager,
                            stageIndex + 1,
                            stageIndex))
                    {
                        PrintResult();
                        return;
                    }

                    if (!CompleteAugmentFlow(runManager))
                    {
                        PrintResult();
                        return;
                    }

                    runManager.LeaveShop();

                    if (!ValidateBattleState(
                            runManager,
                            stageIndex + 1,
                            1,
                            $"Start {stageIndex + 1}-1"))
                    {
                        PrintResult();
                        return;
                    }
                }
            }
        }

        PrintResult();
    }

    private void ForcePlayerWinThroughBattleResult(RunManager runManager)
    {
        BattleManager battleManager =
            runManager.CurrentBattleManager;

        if (battleManager == null)
        {
            Debug.LogError(
                "[FAIL] Cannot force battle result: " +
                "CurrentBattleManager is null."
            );

            _failedCount++;
            return;
        }

        battleManager.DebugForceBattleResult(PieceOwner.Player);

        runManager.ResolveCurrentBattle();
    }

    private bool ValidateBattleState(
        RunManager runManager,
        int expectedStage,
        int expectedRound,
        string testName)
    {
        bool passed =
            runManager.CurrentState == RunState.Battle &&
            !runManager.IsRunOver &&
            runManager.StageIndex == expectedStage &&
            runManager.RoundIndex == expectedRound &&
            runManager.CurrentBoard != null &&
            runManager.CurrentBattleManager != null;

        LogResult(passed, testName, runManager);

        return passed;
    }

    private bool ValidateNormalWinFlow(
        RunManager runManager,
        int expectedStage,
        int expectedRound)
    {
        bool passed =
            runManager.CurrentState == RunState.Shop &&
            !runManager.IsRunOver &&
            runManager.StageIndex == expectedStage &&
            runManager.RoundIndex == expectedRound &&
            runManager.CurrentShop != null;

        LogResult(
            passed,
            $"Normal Win ¡æ Shop ¡æ {expectedStage}-{expectedRound}",
            runManager
        );

        return passed;
    }

    private bool ValidateBossWinFlow(
        RunManager runManager,
        int expectedStage,
        int expectedBossClearCount)
    {
        bool passed =
            runManager.CurrentState == RunState.AugmentSelection &&
            !runManager.IsRunOver &&
            runManager.StageIndex == expectedStage &&
            runManager.RoundIndex == 1 &&
            runManager.BossClearCount == expectedBossClearCount &&
            runManager.GetAugmentChoices().Count > 0;

        LogResult(
            passed,
            $"Boss Win ¡æ AugmentSelection ¡æ Stage {expectedStage}",
            runManager
        );

        return passed;
    }

    private bool CompleteAugmentFlow(RunManager runManager)
    {
        if (runManager.CurrentState != RunState.AugmentSelection)
        {
            LogResult(
                false,
                "Augment Flow Start",
                runManager
            );

            return false;
        }

        if (runManager.GetAugmentChoices().Count == 0)
        {
            LogResult(
                false,
                "Augment Choices Exist",
                runManager
            );

            return false;
        }

        runManager.SelectAugment(0);

        if (runManager.CurrentState == RunState.AugmentReplacement)
        {
            runManager.SkipAugment();
        }

        bool passed =
            runManager.CurrentState == RunState.Shop &&
            runManager.CurrentShop != null;

        LogResult(
            passed,
            "Augment Flow ¡æ Shop",
            runManager
        );

        return passed;
    }

    private void ValidateFinalClear(RunManager runManager)
    {
        bool passed =
            runManager.CurrentState == RunState.Clear &&
            runManager.IsRunOver &&
            runManager.StageIndex == 10 &&
            runManager.RoundIndex == 3 &&
            runManager.BossClearCount == 10 &&
            runManager.CurrentShop == null;

        LogResult(
            passed,
            "10-3 ¡æ Run Clear",
            runManager
        );
    }

    private void ValidateProgressBlockedAfterClear(
        RunManager runManager)
    {
        int stageBefore = runManager.StageIndex;
        int roundBefore = runManager.RoundIndex;
        RunState stateBefore = runManager.CurrentState;

        Board boardBefore = runManager.CurrentBoard;
        BattleManager battleBefore =
            runManager.CurrentBattleManager;

        runManager.StartNextBattle();

        bool passed =
            runManager.StageIndex == stageBefore &&
            runManager.RoundIndex == roundBefore &&
            runManager.CurrentState == stateBefore &&
            runManager.IsRunOver &&
            runManager.CurrentBoard == boardBefore &&
            runManager.CurrentBattleManager == battleBefore;

        LogResult(
            passed,
            "No Progress After Clear",
            runManager
        );
    }

    private void LogResult(
        bool passed,
        string testName,
        RunManager runManager)
    {
        if (passed)
        {
            _passedCount++;

            Debug.Log(
                $"[PASS] {testName} | " +
                $"Stage: {runManager.StageIndex}, " +
                $"Round: {runManager.RoundIndex}, " +
                $"State: {runManager.CurrentState}, " +
                $"BossClearCount: {runManager.BossClearCount}"
            );
        }
        else
        {
            _failedCount++;

            Debug.LogError(
                $"[FAIL] {testName} | " +
                $"Stage: {runManager.StageIndex}, " +
                $"Round: {runManager.RoundIndex}, " +
                $"State: {runManager.CurrentState}, " +
                $"IsRunOver: {runManager.IsRunOver}, " +
                $"BossClearCount: {runManager.BossClearCount}"
            );
        }
    }

    private void PrintResult()
    {
        Debug.Log(
            $"===== Run Flow Integration Test End | " +
            $"Passed: {_passedCount}, " +
            $"Failed: {_failedCount} ====="
        );
    }
}

#endif