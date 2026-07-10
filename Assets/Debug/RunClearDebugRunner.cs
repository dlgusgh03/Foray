#if UNITY_EDITOR

using UnityEngine;

public class RunClearDebugRunner : MonoBehaviour
{
    private int _passedCount;
    private int _failedCount;

    private void Start()
    {
        Debug.Log("===== Run Clear Debug Start =====");

        _passedCount = 0;
        _failedCount = 0;

        RunManager runManager = new RunManager();
        runManager.StartRun();

        if (!ValidateBattleState(
                runManager,
                expectedStage: 1,
                expectedRound: 1,
                "Initial Battle Test"))
        {
            PrintResult();
            return;
        }

        for (int stageIndex = 1; stageIndex <= 10; stageIndex++)
        {
            for (int roundIndex = 1; roundIndex <= 3; roundIndex++)
            {
                Debug.Log(
                    $"===== Force Win {stageIndex}-{roundIndex} ====="
                );

                if (!ValidateBattleState(
                        runManager,
                        stageIndex,
                        roundIndex,
                        $"{stageIndex}-{roundIndex} Before Win Test"))
                {
                    PrintResult();
                    return;
                }

                int goldBeforeWin = runManager.Gold;

                runManager.DebugForceBattleWin();

                if (stageIndex == 10 && roundIndex == 3)
                {
                    ValidateFinalClear(
                        runManager,
                        goldBeforeWin
                    );

                    ValidateProgressBlockedAfterClear(runManager);

                    PrintResult();
                    return;
                }

                if (roundIndex < 3)
                {
                    if (!ValidateNormalBattleWin(
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
                            $"{stageIndex}-{roundIndex + 1} Start Test"))
                    {
                        PrintResult();
                        return;
                    }
                }
                else
                {
                    if (!ValidateBossBattleWin(
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
                            $"{stageIndex + 1}-1 Start Test"))
                    {
                        PrintResult();
                        return;
                    }
                }
            }
        }

        PrintResult();
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
            runManager.CurrentBattleManager != null &&
            runManager.CurrentBoard != null;

        LogResult(
            passed,
            testName,
            runManager
        );

        return passed;
    }

    private bool ValidateNormalBattleWin(
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
            $"After Normal Win ¡æ {expectedStage}-{expectedRound}",
            runManager
        );

        return passed;
    }

    private bool ValidateBossBattleWin(
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
            $"After Boss Win ¡æ Stage {expectedStage}",
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
                "Augment Flow Start Test",
                runManager
            );

            return false;
        }

        if (runManager.GetAugmentChoices().Count == 0)
        {
            LogResult(
                false,
                "Augment Choice Exists Test",
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
            "Complete Augment Flow Test",
            runManager
        );

        return passed;
    }

    private void ValidateFinalClear(
        RunManager runManager,
        int goldBeforeWin)
    {
        bool passed =
            runManager.CurrentState == RunState.Clear &&
            runManager.IsRunOver &&
            runManager.StageIndex == 10 &&
            runManager.RoundIndex == 3 &&
            runManager.BossClearCount == 10 &&
            runManager.Gold > goldBeforeWin &&
            runManager.CurrentShop == null;

        LogResult(
            passed,
            "10-3 Final Clear Test",
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
            "Progress Blocked After Clear Test",
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
                $"IsRunOver: {runManager.IsRunOver}"
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
            $"===== Run Clear Debug End | " +
            $"Passed: {_passedCount}, " +
            $"Failed: {_failedCount} ====="
        );
    }
}

#endif