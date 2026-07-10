using System.Collections.Generic;

public static class EnemyPresetCatalog
{
    private static readonly EnemyPieceData[][] PresetData =
{
    // Stage 1
    // 1-1: 6
    new[]
    {
        new EnemyPieceData(PieceType.King, new BoardPosition(2, 1)),
        new EnemyPieceData(PieceType.Soldier, new BoardPosition(1, 0)),
        new EnemyPieceData(PieceType.Soldier, new BoardPosition(3, 0))
    },

    // 1-2: 8
    new[]
    {
        new EnemyPieceData(PieceType.King, new BoardPosition(2, 1)),
        new EnemyPieceData(PieceType.Soldier, new BoardPosition(0, 0)),
        new EnemyPieceData(PieceType.Soldier, new BoardPosition(2, 0)),
        new EnemyPieceData(PieceType.Soldier, new BoardPosition(4, 0))
    },

    // 1-3: 8
    new[]
    {
        new EnemyPieceData(PieceType.King, new BoardPosition(2, 1)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(0, 0)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(4, 0))
    },

    // Stage 2
    // 2-1: 10
    new[]
    {
        new EnemyPieceData(PieceType.King, new BoardPosition(3, 2)),
        new EnemyPieceData(PieceType.Soldier, new BoardPosition(3, 0)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(1, 0)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(5, 0))
    },

    // 2-2: 10
    new[]
    {
        new EnemyPieceData(PieceType.King, new BoardPosition(3, 2)),
        new EnemyPieceData(PieceType.Soldier, new BoardPosition(2, 0)),
        new EnemyPieceData(PieceType.Soldier, new BoardPosition(4, 0)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(3, 1))
    },

    // 2-3: 12
    new[]
    {
        new EnemyPieceData(PieceType.King, new BoardPosition(3, 2)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(1, 0)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(5, 0)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(3, 1))
    },

    // Stage 3
    // 3-1: 12
    new[]
    {
        new EnemyPieceData(PieceType.King, new BoardPosition(3, 2)),
        new EnemyPieceData(PieceType.Soldier, new BoardPosition(1, 0)),
        new EnemyPieceData(PieceType.Soldier, new BoardPosition(3, 0)),
        new EnemyPieceData(PieceType.Soldier, new BoardPosition(5, 0)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(3, 1))
    },

    // 3-2: 14
    new[]
    {
        new EnemyPieceData(PieceType.King, new BoardPosition(3, 2)),
        new EnemyPieceData(PieceType.Soldier, new BoardPosition(3, 0)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(1, 0)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(5, 0)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(3, 1))
    },

    // 3-3: 14
    new[]
    {
        new EnemyPieceData(PieceType.King, new BoardPosition(3, 2)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(1, 0)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(5, 0)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(3, 1))
    },

    // Stage 4
    // 4-1: 18
    new[]
    {
        new EnemyPieceData(PieceType.King, new BoardPosition(3, 2)),
        new EnemyPieceData(PieceType.Soldier, new BoardPosition(2, 0)),
        new EnemyPieceData(PieceType.Soldier, new BoardPosition(4, 0)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(0, 0)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(6, 0)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(3, 1))
    },

    // 4-2: 18
    new[]
    {
        new EnemyPieceData(PieceType.King, new BoardPosition(3, 2)),
        new EnemyPieceData(PieceType.Soldier, new BoardPosition(3, 1)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(1, 0)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(5, 0)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(3, 0))
    },

    // 4-3: 20
    new[]
    {
        new EnemyPieceData(PieceType.King, new BoardPosition(3, 2)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(1, 0)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(5, 0)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(0, 1)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(3, 1)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(6, 1))
    },

    // Stage 5
    // 5-1: 22
    new[]
    {
        new EnemyPieceData(PieceType.King, new BoardPosition(4, 3)),
        new EnemyPieceData(PieceType.Soldier, new BoardPosition(4, 2)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(2, 0)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(6, 0)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(1, 1)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(7, 1))
    },

    // 5-2: 24
    new[]
    {
        new EnemyPieceData(PieceType.King, new BoardPosition(4, 3)),
        new EnemyPieceData(PieceType.Soldier, new BoardPosition(4, 2)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(2, 1)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(6, 1)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(1, 0)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(7, 0))
    },

    // 5-3: 24
    new[]
    {
        new EnemyPieceData(PieceType.King, new BoardPosition(4, 3)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(2, 0)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(6, 0)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(4, 2)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(1, 1)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(7, 1))
    },

    // Stage 6
    // 6-1: 28
    new[]
    {
        new EnemyPieceData(PieceType.King, new BoardPosition(4, 3)),
        new EnemyPieceData(PieceType.Soldier, new BoardPosition(3, 1)),
        new EnemyPieceData(PieceType.Soldier, new BoardPosition(5, 1)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(2, 0)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(6, 0)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(4, 2)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(0, 1)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(8, 1))
    },

    // 6-2: 28
    new[]
    {
        new EnemyPieceData(PieceType.King, new BoardPosition(4, 3)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(2, 0)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(6, 0)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(3, 2)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(5, 2)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(1, 1)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(7, 1))
    },

    // 6-3: 32
    new[]
    {
        new EnemyPieceData(PieceType.King, new BoardPosition(4, 3)),
        new EnemyPieceData(PieceType.Soldier, new BoardPosition(3, 2)),
        new EnemyPieceData(PieceType.Soldier, new BoardPosition(5, 2)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(2, 0)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(6, 0)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(1, 1)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(7, 1)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(0, 1)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(8, 1))
    },

    // Stage 7
    // 7-1: 32
    new[]
    {
        new EnemyPieceData(PieceType.King, new BoardPosition(4, 3)),
        new EnemyPieceData(PieceType.Soldier, new BoardPosition(4, 2)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(1, 0)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(7, 0)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(2, 1)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(6, 1)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(4, 1)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(0, 2)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(8, 2))
    },

    // 7-2: 36
    new[]
    {
        new EnemyPieceData(PieceType.King, new BoardPosition(4, 3)),
        new EnemyPieceData(PieceType.Soldier, new BoardPosition(3, 2)),
        new EnemyPieceData(PieceType.Soldier, new BoardPosition(5, 2)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(2, 0)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(6, 0)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(4, 1)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(1, 1)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(7, 1)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(0, 2)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(8, 2))
    },

    // 7-3: 36
    new[]
    {
        new EnemyPieceData(PieceType.King, new BoardPosition(4, 3)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(2, 0)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(6, 0)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(3, 2)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(5, 2)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(1, 1)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(7, 1)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(0, 2)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(8, 2))
    },

    // Stage 8
    // 8-1: 40
    new[]
    {
        new EnemyPieceData(PieceType.King, new BoardPosition(5, 4)),
        new EnemyPieceData(PieceType.Soldier, new BoardPosition(4, 3)),
        new EnemyPieceData(PieceType.Soldier, new BoardPosition(6, 3)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(3, 0)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(7, 0)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(2, 1)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(8, 1)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(3, 2)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(7, 2)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(1, 2)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(9, 2))
    },

    // 8-2: 40
    new[]
    {
        new EnemyPieceData(PieceType.King, new BoardPosition(5, 4)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(2, 0)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(8, 0)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(3, 1)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(7, 1)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(4, 3)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(6, 3)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(1, 2)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(9, 2)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(5, 2))
    },

    // 8-3: 44
    new[]
    {
        new EnemyPieceData(PieceType.King, new BoardPosition(5, 4)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(3, 0)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(7, 0)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(4, 3)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(6, 3)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(5, 2)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(1, 1)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(9, 1)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(2, 2)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(8, 2))
    },

    // Stage 9
    // 9-1: 46
    new[]
    {
        new EnemyPieceData(PieceType.King, new BoardPosition(5, 4)),
        new EnemyPieceData(PieceType.Soldier, new BoardPosition(4, 3)),
        new EnemyPieceData(PieceType.Soldier, new BoardPosition(6, 3)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(2, 0)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(8, 0)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(3, 1)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(7, 1)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(1, 1)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(9, 1)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(2, 2)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(8, 2)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(0, 2)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(10, 2))
    },

    // 9-2: 48
    new[]
    {
        new EnemyPieceData(PieceType.King, new BoardPosition(5, 4)),
        new EnemyPieceData(PieceType.Soldier, new BoardPosition(5, 2)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(3, 0)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(7, 0)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(4, 3)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(6, 3)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(5, 1)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(2, 1)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(8, 1)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(1, 2)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(9, 2)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(5, 3))
    },

    // 9-3: 50
    new[]
    {
        new EnemyPieceData(PieceType.King, new BoardPosition(5, 4)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(2, 0)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(8, 0)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(3, 1)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(7, 1)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(5, 2)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(1, 2)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(9, 2)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(4, 3)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(6, 3)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(0, 2)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(10, 2))
    },

    // Stage 10
    // 10-1: 54
    new[]
    {
        new EnemyPieceData(PieceType.King, new BoardPosition(5, 4)),
        new EnemyPieceData(PieceType.Soldier, new BoardPosition(4, 3)),
        new EnemyPieceData(PieceType.Soldier, new BoardPosition(6, 3)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(2, 0)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(8, 0)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(3, 1)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(7, 1)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(1, 1)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(9, 1)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(2, 2)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(8, 2)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(4, 2)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(6, 2)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(0, 3)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(10, 3))
    },

    // 10-2: 56
    new[]
    {
        new EnemyPieceData(PieceType.King, new BoardPosition(5, 4)),
        new EnemyPieceData(PieceType.Soldier, new BoardPosition(4, 3)),
        new EnemyPieceData(PieceType.Soldier, new BoardPosition(6, 3)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(3, 0)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(7, 0)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(5, 2)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(2, 1)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(8, 1)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(3, 2)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(7, 2)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(0, 2)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(10, 2)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(1, 3)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(9, 3))
    },

    // 10-3: 60
    new[]
    {
        new EnemyPieceData(PieceType.King, new BoardPosition(5, 4)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(2, 0)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(8, 0)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(3, 1)),
        new EnemyPieceData(PieceType.Horse, new BoardPosition(7, 1)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(2, 2)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(8, 2)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(3, 2)),
        new EnemyPieceData(PieceType.Cannon, new BoardPosition(7, 2)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(4, 3)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(6, 3)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(0, 2)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(10, 2)),
        new EnemyPieceData(PieceType.Chariot, new BoardPosition(5, 3))
    }
};

    public static EnemyPreset Create(int stageIndex, int roundIndex)
    {
        if (stageIndex < 1 || stageIndex > 10)
        {
            return null;
        }

        if (roundIndex < 1 || roundIndex > 3)
        {
            return null;
        }

        int battleIndex = (stageIndex - 1) * 3 + (roundIndex - 1);
        EnemyPieceData[] presetData = PresetData[battleIndex];

        return new EnemyPreset(new List<EnemyPieceData> (presetData));
    }
}