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

    public static MapPreset CreateSquare(int length)
    {
        List<BoardPosition> blockedPositions = new List<BoardPosition>();
        List<BoardPosition> playerDeployPositions = new List<BoardPosition>();
        List<BoardPosition> enemyDeployPositions = new List<BoardPosition>();

        int deployRows = 2;

        for (int y = 0; y < deployRows; y++)
        {
            for (int x = 0; x < length; x++)
            {
                playerDeployPositions.Add(new BoardPosition(x, y));
            }
        }

        for (int y = length - deployRows; y < length; y++)
        {
            for (int x = 0; x < length; x++)
            {
                enemyDeployPositions.Add(new BoardPosition(x, y));
            }
        }

        return new MapPreset(length, length, blockedPositions, playerDeployPositions, enemyDeployPositions);
    }
}