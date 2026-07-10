using System.Collections.Generic;
using UnityEngine;

public class EnemyPresetDebugRunner : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("===== Enemy Preset Test Start =====");

        int passedCount = 0;
        int failedCount = 0;

        for (int stageIndex = 1; stageIndex <= 10; stageIndex++)
        {
            for (int roundIndex = 1; roundIndex <= 3; roundIndex++)
            {
                bool passed = TestPreset(stageIndex, roundIndex);

                if (passed)
                {
                    passedCount++;
                }
                else
                {
                    failedCount++;
                }
            }
        }

        TestInvalidInputs();

        Debug.Log(
            $"===== Enemy Preset Test End | " +
            $"Passed: {passedCount}, Failed: {failedCount} ====="
        );
    }

    private bool TestPreset(int stageIndex, int roundIndex)
    {
        int mapLength = MapPreset.GetMapLengthForStage(stageIndex);

        MapPreset mapPreset = MapPreset.CreateSquare(mapLength);
        EnemyPreset enemyPreset =
            EnemyPresetCatalog.Create(stageIndex, roundIndex);

        if (enemyPreset == null)
        {
            Debug.LogError(
                $"[FAIL] {stageIndex}-{roundIndex} | " +
                $"EnemyPreset is null."
            );

            return false;
        }

        bool passed = true;

        passed &= ValidatePopulationCost(
            stageIndex,
            roundIndex,
            enemyPreset
        );

        passed &= ValidateKingCount(
            stageIndex,
            roundIndex,
            enemyPreset
        );

        passed &= ValidateDeployPositions(
            stageIndex,
            roundIndex,
            mapPreset,
            enemyPreset
        );

        passed &= ValidateBattleCreation(
            stageIndex,
            roundIndex,
            mapPreset,
            enemyPreset
        );

        if (passed)
        {
            int populationCost =
                CalculatePopulationCost(enemyPreset);

            Debug.Log(
                $"[PASS] {stageIndex}-{roundIndex} | " +
                $"Map: {mapLength}x{mapLength} | " +
                $"Pieces: {enemyPreset.EnemyPieces.Count} | " +
                $"PopulationCost: {populationCost}"
            );
        }

        return passed;
    }

    private bool ValidatePopulationCost(
        int stageIndex,
        int roundIndex,
        EnemyPreset enemyPreset)
    {
        int actualCost = CalculatePopulationCost(enemyPreset);

        int targetCost =
            EnemyScaling.GetTargetPopulationCost(
                stageIndex,
                roundIndex
            );

        if (actualCost != targetCost)
        {
            Debug.LogError(
                $"[FAIL] {stageIndex}-{roundIndex} | " +
                $"PopulationCost mismatch | " +
                $"Target: {targetCost}, Actual: {actualCost}"
            );

            return false;
        }

        return true;
    }

    private int CalculatePopulationCost(
        EnemyPreset enemyPreset)
    {
        int totalCost = 0;

        foreach (EnemyPieceData enemyPieceData
                 in enemyPreset.EnemyPieces)
        {
            totalCost +=
                PieceCatalog.GetPopulationCost(
                    enemyPieceData.Type
                );
        }

        return totalCost;
    }

    private bool ValidateKingCount(
        int stageIndex,
        int roundIndex,
        EnemyPreset enemyPreset)
    {
        int kingCount = 0;

        foreach (EnemyPieceData enemyPieceData
                 in enemyPreset.EnemyPieces)
        {
            if (enemyPieceData.Type == PieceType.King)
            {
                kingCount++;
            }
        }

        if (kingCount != 1)
        {
            Debug.LogError(
                $"[FAIL] {stageIndex}-{roundIndex} | " +
                $"King count: {kingCount}"
            );

            return false;
        }

        return true;
    }

    private bool ValidateDeployPositions(
        int stageIndex,
        int roundIndex,
        MapPreset mapPreset,
        EnemyPreset enemyPreset)
    {
        HashSet<BoardPosition> usedPositions =
            new HashSet<BoardPosition>();

        bool passed = true;

        foreach (EnemyPieceData enemyPieceData
                 in enemyPreset.EnemyPieces)
        {
            BoardPosition position =
                enemyPieceData.DeployPosition;

            bool isInsideDeployArea =
                position.X >= 0 &&
                position.X < mapPreset.Width &&
                position.Y >= 0 &&
                position.Y < mapPreset.DeployRows;

            if (!isInsideDeployArea)
            {
                Debug.LogError(
                    $"[FAIL] {stageIndex}-{roundIndex} | " +
                    $"{enemyPieceData.Type} deploy position " +
                    $"{position} is outside enemy deploy area."
                );

                passed = false;
            }

            if (!usedPositions.Add(position))
            {
                Debug.LogError(
                    $"[FAIL] {stageIndex}-{roundIndex} | " +
                    $"Duplicate deploy position: {position}"
                );

                passed = false;
            }
        }

        return passed;
    }

    private bool ValidateBattleCreation(
        int stageIndex,
        int roundIndex,
        MapPreset mapPreset,
        EnemyPreset enemyPreset)
    {
        PlayerArmy playerArmy = new PlayerArmy();

        BattleManager battleManager =
            BattleSetup.CreateBattle(
                mapPreset,
                enemyPreset,
                playerArmy
            );

        if (battleManager == null)
        {
            Debug.LogError(
                $"[FAIL] {stageIndex}-{roundIndex} | " +
                $"Battle creation failed."
            );

            return false;
        }

        Board board = battleManager.Board;

        foreach (EnemyPieceData enemyPieceData
                 in enemyPreset.EnemyPieces)
        {
            BoardPosition deployPosition =
                enemyPieceData.DeployPosition;

            int actualX = deployPosition.X;
            int actualY =
                mapPreset.Height
                - mapPreset.DeployRows
                + deployPosition.Y;

            BoardPosition actualPosition =
                new BoardPosition(actualX, actualY);

            Piece piece = board.GetPiece(actualPosition);

            if (piece == null)
            {
                Debug.LogError(
                    $"[FAIL] {stageIndex}-{roundIndex} | " +
                    $"No piece at actual position " +
                    $"{actualPosition}."
                );

                return false;
            }

            if (piece.Owner != PieceOwner.Enemy)
            {
                Debug.LogError(
                    $"[FAIL] {stageIndex}-{roundIndex} | " +
                    $"Piece at {actualPosition} is not enemy."
                );

                return false;
            }

            if (piece.Type != enemyPieceData.Type)
            {
                Debug.LogError(
                    $"[FAIL] {stageIndex}-{roundIndex} | " +
                    $"Piece type mismatch at {actualPosition} | " +
                    $"Expected: {enemyPieceData.Type}, " +
                    $"Actual: {piece.Type}"
                );

                return false;
            }
        }

        return true;
    }

    private void TestInvalidInputs()
    {
        bool passed =
            EnemyPresetCatalog.Create(0, 1) == null &&
            EnemyPresetCatalog.Create(11, 1) == null &&
            EnemyPresetCatalog.Create(1, 0) == null &&
            EnemyPresetCatalog.Create(1, 4) == null;

        if (passed)
        {
            Debug.Log("[PASS] Invalid input test");
        }
        else
        {
            Debug.LogError("[FAIL] Invalid input test");
        }
    }
}