using System.Collections.Generic;

public class RunManager
{
    private PlayerArmy _playerArmy;
    private int _stageIndex;
    private int _gold;

    private Board _currentBoard;
    private BattleManager _currentBattleManager;

    public PlayerArmy PlayerArmy => _playerArmy;
    public int StageIndex => _stageIndex;
    public int Gold => _gold;
    public Board CurrentBoard => _currentBoard;
    public BattleManager CurrentBattleManager => _currentBattleManager;

    public RunManager()
    {
        _playerArmy = new PlayerArmy();
        _stageIndex = 1;
        _gold = 0;
    }

    public void StartRun()
    {
        StartBattle();
    }

    private void StartBattle()
    {
        MapPreset mapPreset = SelectMapPreset();

        _currentBoard = CreateBoardFromMapPreset(mapPreset);

        DeployPlayerPieces(_currentBoard, mapPreset);
        DeployEnemyPieces(_currentBoard, mapPreset);

        _currentBattleManager = new BattleManager(_currentBoard);
    }

    private MapPreset SelectMapPreset()
    {
        return MapPreset.CreateOpenField();
    }

    private Board CreateBoardFromMapPreset(MapPreset mapPreset)
    {
        Board board = new Board(mapPreset.Width, mapPreset.Height);

        foreach (BoardPosition position in mapPreset.BlockedPositions)
        {
            BoardCell cell = board.GetCell(position);

            if (cell != null)
            {
                cell.SetBlocked(true);
            }
        }

        return board;
    }

    private void DeployPlayerPieces(Board board, MapPreset mapPreset)
    {
        List<PieceType> pieceTypes = _playerArmy.GetOwnedPieceTypes();

        for (int i = 0; i < pieceTypes.Count; i++)
        {
            PieceType type = pieceTypes[i];
            BoardPosition position = mapPreset.PlayerDeployPositions[i];

            Piece piece = new Piece(type, PieceOwner.Player, position);
            board.PlacePiece(piece, position);
        }
    }

    private void DeployEnemyPieces(Board board, MapPreset mapPreset)
    {
        List<PieceType> enemyPieceTypes = new List<PieceType>()
        {
            PieceType.King,
            PieceType.Soldier,
            PieceType.Soldier
        };

        for (int i = 0; i < enemyPieceTypes.Count; i++)
        {
            PieceType type = enemyPieceTypes[i];
            BoardPosition position = mapPreset.EnemyDeployPositions[i];

            Piece piece = new Piece(type, PieceOwner.Enemy, position);
            board.PlacePiece(piece, position);
        }
    }
}