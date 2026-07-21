using System.Collections.Generic;
using UnityEngine;

public class PieceInstanceIdDebugRunner : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("===== Piece Instance ID Debug Start =====");

        TestInitialIds();
        TestBoardPieceIdConnection();
        TestRemovePieceById();
        TestKingCannotBeRemoved();
        TestRemovedIdIsNotReused();

        Debug.Log("===== Piece Instance ID Debug End =====");
    }

    private void TestInitialIds()
    {
        Debug.Log("[Test 1] Initial army pieces have unique IDs");

        PlayerArmy army = new PlayerArmy();
        List<ArmyPiece> pieces = army.GetOwnedPieces();

        PrintArmyPieces(pieces);

        bool correctCount = pieces.Count == 3;

        bool correctKing =
            pieces.Count >= 1 &&
            pieces[0].Type == PieceType.King &&
            pieces[0].InstanceId == 0;

        bool correctFirstSoldier =
            pieces.Count >= 2 &&
            pieces[1].Type == PieceType.Soldier &&
            pieces[1].InstanceId == 1;

        bool correctSecondSoldier =
            pieces.Count >= 3 &&
            pieces[2].Type == PieceType.Soldier &&
            pieces[2].InstanceId == 2;

        bool idsAreUnique = HasUniqueIds(pieces);

        PrintResult("Initial piece count should be 3", correctCount);
        PrintResult("King should have ID 0", correctKing);
        PrintResult("First Soldier should have ID 1", correctFirstSoldier);
        PrintResult("Second Soldier should have ID 2", correctSecondSoldier);
        PrintResult("All initial IDs should be unique", idsAreUnique);
    }

    private void TestBoardPieceIdConnection()
    {
        Debug.Log("[Test 2] Board pieces are connected to PlayerArmy IDs");

        PlayerArmy army = new PlayerArmy();

        int length = MapPreset.GetMapLengthForStage(1);
        MapPreset mapPreset = MapPreset.CreateSquare(length);
        EnemyPreset enemyPreset = EnemyPresetCatalog.Create(1, 1);

        BattleManager battleManager =
            BattleSetup.CreateBattle(mapPreset, enemyPreset, army);

        if (battleManager == null)
        {
            PrintResult("Battle should be created", false);
            return;
        }

        Board board = battleManager.Board;
        bool allPlayerPiecesConnected = true;
        bool allEnemyPiecesHaveNullId = true;
        int playerPieceCount = 0;
        int enemyPieceCount = 0;

        for (int y = 0; y < board.Height; y++)
        {
            for (int x = 0; x < board.Width; x++)
            {
                BoardPosition position = new BoardPosition(x, y);
                Piece boardPiece = board.GetPiece(position);

                if (boardPiece == null)
                {
                    continue;
                }

                Debug.Log(
                    $"Board Piece | " +
                    $"Type: {boardPiece.Type}, " +
                    $"Owner: {boardPiece.Owner}, " +
                    $"Position: {boardPiece.Position}, " +
                    $"ArmyPieceId: {FormatId(boardPiece.ArmyPieceId)}"
                );

                if (boardPiece.Owner == PieceOwner.Player)
                {
                    playerPieceCount++;

                    if (!boardPiece.ArmyPieceId.HasValue)
                    {
                        allPlayerPiecesConnected = false;
                        continue;
                    }

                    ArmyPiece armyPiece =
                        army.GetPieceById(boardPiece.ArmyPieceId.Value);

                    if (armyPiece == null ||
                        armyPiece.Type != boardPiece.Type)
                    {
                        allPlayerPiecesConnected = false;
                    }
                }
                else if (boardPiece.Owner == PieceOwner.Enemy)
                {
                    enemyPieceCount++;

                    if (boardPiece.ArmyPieceId.HasValue)
                    {
                        allEnemyPiecesHaveNullId = false;
                    }
                }
            }
        }

        PrintResult(
            "Board should contain every PlayerArmy piece",
            playerPieceCount == army.PieceCount
        );

        PrintResult(
            "Every player Board piece should match an ArmyPiece",
            allPlayerPiecesConnected
        );

        PrintResult(
            "Enemy pieces should exist",
            enemyPieceCount > 0
        );

        PrintResult(
            "Every enemy piece should have a null ArmyPieceId",
            allEnemyPiecesHaveNullId
        );
    }

    private void TestRemovePieceById()
    {
        Debug.Log("[Test 3] Remove only the requested piece ID");

        PlayerArmy army = new PlayerArmy();

        ArmyPiece firstSoldier = army.GetPieceById(1);
        ArmyPiece secondSoldier = army.GetPieceById(2);

        int populationBefore = army.CurrentPopulation;

        bool removed = army.RemovePieceById(1);

        ArmyPiece removedPiece = army.GetPieceById(1);
        ArmyPiece remainingPiece = army.GetPieceById(2);

        Debug.Log("Army after removing ID 1:");
        PrintArmyPieces(army.GetOwnedPieces());

        PrintResult("Removing Soldier ID 1 should succeed", removed);
        PrintResult("ID 1 should no longer exist", removedPiece == null);
        PrintResult("ID 2 Soldier should still exist", remainingPiece != null);
        PrintResult(
            "The two Soldiers should originally be different pieces",
            firstSoldier != null &&
            secondSoldier != null &&
            firstSoldier.InstanceId != secondSoldier.InstanceId
        );

        int soldierCost =
            PieceCatalog.GetPopulationCost(PieceType.Soldier);

        PrintResult(
            "Population should decrease by one Soldier cost",
            army.CurrentPopulation == populationBefore - soldierCost
        );
    }

    private void TestKingCannotBeRemoved()
    {
        Debug.Log("[Test 4] King ID 0 cannot be removed");

        PlayerArmy army = new PlayerArmy();

        int countBefore = army.PieceCount;
        int populationBefore = army.CurrentPopulation;

        bool removed = army.RemovePieceById(0);
        ArmyPiece king = army.GetPieceById(0);

        PrintResult("Removing King ID 0 should fail", !removed);
        PrintResult("King ID 0 should still exist", king != null);
        PrintResult(
            "Army piece count should not change",
            army.PieceCount == countBefore
        );
        PrintResult(
            "Population should not change",
            army.CurrentPopulation == populationBefore
        );
    }

    private void TestRemovedIdIsNotReused()
    {
        Debug.Log("[Test 5] Removed IDs are not reused");

        PlayerArmy army = new PlayerArmy();
        army.IncreaseMaxPopulation(10);

        // 초기 ID: King 0, Soldier 1, Soldier 2
        ArmyPiece firstAddedPiece = army.AddPiece(PieceType.Soldier);

        if (firstAddedPiece == null)
        {
            PrintResult(
                "First additional piece should be created",
                false
            );
            return;
        }

        int removedId = firstAddedPiece.InstanceId;
        bool removed = army.RemovePieceById(removedId);

        ArmyPiece secondAddedPiece = army.AddPiece(PieceType.Soldier);

        Debug.Log($"Removed ID: {removedId}");
        Debug.Log(
            $"New ID: " +
            $"{(secondAddedPiece == null ? "null" : secondAddedPiece.InstanceId.ToString())}"
        );

        PrintResult("First added piece should be removed", removed);
        PrintResult(
            "Second additional piece should be created",
            secondAddedPiece != null
        );

        PrintResult(
            "Removed ID should not be reused",
            secondAddedPiece != null &&
            secondAddedPiece.InstanceId > removedId
        );
    }

    private bool HasUniqueIds(List<ArmyPiece> pieces)
    {
        HashSet<int> ids = new HashSet<int>();

        foreach (ArmyPiece piece in pieces)
        {
            if (!ids.Add(piece.InstanceId))
            {
                return false;
            }
        }

        return true;
    }

    private void PrintArmyPieces(List<ArmyPiece> pieces)
    {
        foreach (ArmyPiece piece in pieces)
        {
            Debug.Log(
                $"ArmyPiece | " +
                $"ID: {piece.InstanceId}, " +
                $"Type: {piece.Type}"
            );
        }
    }

    private string FormatId(int? instanceId)
    {
        return instanceId.HasValue
            ? instanceId.Value.ToString()
            : "null";
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