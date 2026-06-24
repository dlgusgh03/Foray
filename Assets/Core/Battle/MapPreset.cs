using System.Collections.Generic;

public class MapPreset
{
    private readonly int _width;
    private readonly int _height;
    private readonly List<BoardPosition> _blockedPositions;
    private readonly List<BoardPosition> _playerDeployPositions;
    private readonly List<BoardPosition> _enemyDeployPositions;

    public int Width => _width;
    public int Height => _height;
    public List<BoardPosition> BlockedPositions => _blockedPositions;
    public List<BoardPosition> PlayerDeployPositions => _playerDeployPositions;
    public List<BoardPosition> EnemyDeployPositions => _enemyDeployPositions;

    public MapPreset(int width, int height, List<BoardPosition> blockedPositions, List<BoardPosition> playerDeployPositions, List<BoardPosition> enemyDeployPositions)
    {
        _width = width;
        _height = height;
        _blockedPositions = blockedPositions;
        _playerDeployPositions = playerDeployPositions;
        _enemyDeployPositions = enemyDeployPositions;
    }

    public static MapPreset CreateOpenField()
    {
        List<BoardPosition> blockedPositions = new List<BoardPosition>();

        List<BoardPosition> playerDeployPositions = new List<BoardPosition>()
        {
            new BoardPosition(2, 0),
            new BoardPosition(3, 0),
            new BoardPosition(4, 0)
        };

        List<BoardPosition> enemyDeployPositions = new List<BoardPosition>()
        {
            new BoardPosition(2, 6),
            new BoardPosition(3, 6),
            new BoardPosition(4, 6)
        };

        return new MapPreset(7, 7, blockedPositions, playerDeployPositions, enemyDeployPositions);
    }

    public static MapPreset CreateCenterBlock()
    {
        List<BoardPosition> blockedPositions = new List<BoardPosition>()
        {
            new BoardPosition(3, 3),
            new BoardPosition(2, 3),
            new BoardPosition(4, 3)
        };

        List<BoardPosition> playerDeployPositions = new List<BoardPosition>()
        {
            new BoardPosition(2, 0),
            new BoardPosition(3, 0),
            new BoardPosition(4, 0)
        };

        List<BoardPosition> enemyDeployPositions = new List<BoardPosition>()
        {
            new BoardPosition(2, 6),
            new BoardPosition(3, 6),
            new BoardPosition(4, 6)
        };

        return new MapPreset(7, 7, blockedPositions, playerDeployPositions, enemyDeployPositions);
    }
}