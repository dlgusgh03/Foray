using UnityEngine;

public class EnemyAIDebugRunner : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("===== Enemy AI Debug Start =====");

        TestCapturePlayerKingFirst();
        TestCaptureAnyPlayerPiece();
        TestMoveTowardPlayerKing();
        TestReturnNullWhenNoEnemyPiece();
        TestEnemyActionCanBeExecutedByTurnManager();

        Debug.Log("===== Enemy AI Debug End =====");
    }

    private void TestCapturePlayerKingFirst()
    {
        Debug.Log("[Test 1] EnemyAI captures Player King first");

        Board board = new Board(5, 5);

        Piece playerKing = new Piece(
            PieceType.King,
            PieceOwner.Player,
            new BoardPosition(2, 4),
            0
        );

        Piece enemyChariot = new Piece(
            PieceType.Chariot,
            PieceOwner.Enemy,
            new BoardPosition(2, 0),
            null
        );

        board.PlacePiece(playerKing, playerKing.Position);
        board.PlacePiece(enemyChariot, enemyChariot.Position);

        EnemyAI enemyAI = new EnemyAI(board);
        BattleAction action = enemyAI.DecideAction();

        bool hasAction = action != null;
        bool correctFrom = hasAction && action.From.Equals(new BoardPosition(2, 0));
        bool correctTo = hasAction && action.To.Equals(new BoardPosition(2, 4));

        PrintAction(action);
        PrintResult(
            "Expected: Enemy Chariot moves from (2, 0) to Player King at (2, 4)",
            hasAction && correctFrom && correctTo
        );
    }

    private void TestCaptureAnyPlayerPiece()
    {
        Debug.Log("[Test 2] EnemyAI captures any Player piece when King cannot be captured");

        Board board = new Board(5, 5);

        Piece playerKing = new Piece(
            PieceType.King,
            PieceOwner.Player,
            new BoardPosition(4, 4),
            0
        );

        Piece playerSoldier = new Piece(
            PieceType.Soldier,
            PieceOwner.Player,
            new BoardPosition(2, 3),
            0
        );

        Piece enemySoldier = new Piece(
            PieceType.Soldier,
            PieceOwner.Enemy,
            new BoardPosition(2, 2),
            null
        );

        board.PlacePiece(playerKing, playerKing.Position);
        board.PlacePiece(playerSoldier, playerSoldier.Position);
        board.PlacePiece(enemySoldier, enemySoldier.Position);

        EnemyAI enemyAI = new EnemyAI(board);
        BattleAction action = enemyAI.DecideAction();

        bool hasAction = action != null;
        bool correctFrom = hasAction && action.From.Equals(new BoardPosition(2, 2));
        bool correctTo = hasAction && action.To.Equals(new BoardPosition(2, 3));

        PrintAction(action);
        PrintResult(
            "Expected: Enemy Soldier captures Player Soldier at (2, 3)",
            hasAction && correctFrom && correctTo
        );
    }

    private void TestMoveTowardPlayerKing()
    {
        Debug.Log("[Test 3] EnemyAI moves toward Player King when no capture is possible");

        Board board = new Board(5, 5);

        Piece playerKing = new Piece(
            PieceType.King,
            PieceOwner.Player,
            new BoardPosition(4, 4),
            0
        );

        Piece enemySoldier = new Piece(
            PieceType.Soldier,
            PieceOwner.Enemy,
            new BoardPosition(1, 1),
            null
        );

        board.PlacePiece(playerKing, playerKing.Position);
        board.PlacePiece(enemySoldier, enemySoldier.Position);

        EnemyAI enemyAI = new EnemyAI(board);
        BattleAction action = enemyAI.DecideAction();

        bool hasAction = action != null;
        bool correctFrom = hasAction && action.From.Equals(new BoardPosition(1, 1));

        // Enemy Soldier는 Owner가 Enemy이므로 전진 방향이 y - 1.
        // 따라서 (1, 0), (0, 1), (2, 1) 중 Player King (4, 4)에 가장 가까운 곳은 (2, 1).
        bool correctTo = hasAction && action.To.Equals(new BoardPosition(2, 1));

        PrintAction(action);
        PrintResult(
            "Expected: Enemy Soldier moves from (1, 1) to (2, 1), closer to Player King",
            hasAction && correctFrom && correctTo
        );
    }

    private void TestReturnNullWhenNoEnemyPiece()
    {
        Debug.Log("[Test 4] EnemyAI returns null when there is no Enemy piece");

        Board board = new Board(5, 5);

        Piece playerKing = new Piece(
            PieceType.King,
            PieceOwner.Player,
            new BoardPosition(2, 2),
            0
        );

        board.PlacePiece(playerKing, playerKing.Position);

        EnemyAI enemyAI = new EnemyAI(board);
        BattleAction action = enemyAI.DecideAction();

        PrintAction(action);
        PrintResult(
            "Expected: action is null",
            action == null
        );
    }

    private void TestEnemyActionCanBeExecutedByTurnManager()
    {
        Debug.Log("[Test 5] EnemyAI action can be executed by TurnManager");

        Board board = new Board(5, 5);

        Piece playerKing = new Piece(
            PieceType.King,
            PieceOwner.Player,
            new BoardPosition(4, 4),
            0
        );

        Piece enemySoldier = new Piece(
            PieceType.Soldier,
            PieceOwner.Enemy,
            new BoardPosition(1, 1),
            null
        );

        board.PlacePiece(playerKing, playerKing.Position);
        board.PlacePiece(enemySoldier, enemySoldier.Position);

        TurnManager turnManager = new TurnManager(board);

        // TurnManager는 기본 시작 턴이 Player라고 가정.
        // Enemy 행동을 테스트하기 위해 Player가 먼저 합법적인 행동을 해서 Enemy 턴으로 넘김.
        Piece playerSoldier = new Piece(
            PieceType.Soldier,
            PieceOwner.Player,
            new BoardPosition(0, 0),
            0
        );

        board.PlacePiece(playerSoldier, playerSoldier.Position);

        bool playerActed = turnManager.TryAct(
            new BoardPosition(0, 0),
            new BoardPosition(0, 1)
        );

        EnemyAI enemyAI = new EnemyAI(board);
        BattleAction enemyAction = enemyAI.DecideAction();

        bool enemyActed = false;

        if (enemyAction != null)
        {
            enemyActed = turnManager.TryAct(enemyAction.From, enemyAction.To);
        }

        bool turnBackToPlayer = turnManager.CurrentTurnOwner == PieceOwner.Player;

        PrintAction(enemyAction);
        PrintResult(
            "Expected: Player acts, EnemyAI selects action, TurnManager executes it, turn returns to Player",
            playerActed && enemyAction != null && enemyActed && turnBackToPlayer
        );
    }

    private void PrintAction(BattleAction action)
    {
        if (action == null)
        {
            Debug.Log("Selected Action: null");
            return;
        }

        Debug.Log($"Selected Action: {action}");
    }

    private void PrintResult(string expected, bool isPassed)
    {
        Debug.Log(expected);

        if (isPassed)
        {
            Debug.Log("Result: PASS");
        }
        else
        {
            Debug.LogError("Result: FAIL");
        }
    }
}