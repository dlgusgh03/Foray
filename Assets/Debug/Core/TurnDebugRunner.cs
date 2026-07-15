using UnityEngine;

public class TurnDebugRunner : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("===== Turn Debug Start =====");

        TestPlayerMoveSuccess();
        TestCannotMoveEnemyPieceOnPlayerTurn();
        TestCannotMoveToInvalidPosition();
        TestCaptureEnemyPiece();
        TestCaptureEnemyKingEndsBattle();

        Debug.Log("===== Turn Debug End =====");
    }

    private void TestPlayerMoveSuccess()
    {
        Debug.Log("[Test 1] Player piece can move and turn changes to Enemy");

        Board board = new Board(5, 5);

        Piece playerSoldier = new Piece(
            PieceType.Soldier,
            PieceOwner.Player,
            new BoardPosition(2, 2)
        );

        board.PlacePiece(playerSoldier, playerSoldier.Position);

        TurnManager turnManager = new TurnManager(board);

        bool result = turnManager.TryAct(
            new BoardPosition(2, 2),
            new BoardPosition(2, 3)
        );

        bool positionChanged = playerSoldier.Position.Equals(new BoardPosition(2, 3));
        bool turnChanged = turnManager.CurrentTurnOwner == PieceOwner.Enemy;

        PrintResult(
            "Expected: success true, soldier at (2, 3), current turn Enemy",
            result && positionChanged && turnChanged
        );
    }

    private void TestCannotMoveEnemyPieceOnPlayerTurn()
    {
        Debug.Log("[Test 2] Cannot move Enemy piece on Player turn");

        Board board = new Board(5, 5);

        Piece enemySoldier = new Piece(
            PieceType.Soldier,
            PieceOwner.Enemy,
            new BoardPosition(2, 2)
        );

        board.PlacePiece(enemySoldier, enemySoldier.Position);

        TurnManager turnManager = new TurnManager(board);

        bool result = turnManager.TryAct(
            new BoardPosition(2, 2),
            new BoardPosition(2, 1)
        );

        bool positionNotChanged = enemySoldier.Position.Equals(new BoardPosition(2, 2));
        bool turnNotChanged = turnManager.CurrentTurnOwner == PieceOwner.Player;

        PrintResult(
            "Expected: success false, enemy soldier stays at (2, 2), current turn still Player",
            !result && positionNotChanged && turnNotChanged
        );
    }

    private void TestCannotMoveToInvalidPosition()
    {
        Debug.Log("[Test 3] Cannot move to invalid position");

        Board board = new Board(5, 5);

        Piece playerSoldier = new Piece(
            PieceType.Soldier,
            PieceOwner.Player,
            new BoardPosition(2, 2)
        );

        board.PlacePiece(playerSoldier, playerSoldier.Position);

        TurnManager turnManager = new TurnManager(board);

        // Player Soldier는 뒤로 이동할 수 없으므로 (2, 1)은 실패해야 함
        bool result = turnManager.TryAct(
            new BoardPosition(2, 2),
            new BoardPosition(2, 1)
        );

        bool positionNotChanged = playerSoldier.Position.Equals(new BoardPosition(2, 2));
        bool turnNotChanged = turnManager.CurrentTurnOwner == PieceOwner.Player;

        PrintResult(
            "Expected: success false, soldier stays at (2, 2), current turn still Player",
            !result && positionNotChanged && turnNotChanged
        );
    }

    private void TestCaptureEnemyPiece()
    {
        Debug.Log("[Test 4] Player piece can capture Enemy piece and turn changes");

        Board board = new Board(5, 5);

        Piece playerSoldier = new Piece(
            PieceType.Soldier,
            PieceOwner.Player,
            new BoardPosition(2, 2)
        );

        Piece enemySoldier = new Piece(
            PieceType.Soldier,
            PieceOwner.Enemy,
            new BoardPosition(2, 3)
        );

        board.PlacePiece(playerSoldier, playerSoldier.Position);
        board.PlacePiece(enemySoldier, enemySoldier.Position);

        TurnManager turnManager = new TurnManager(board);

        bool result = turnManager.TryAct(
            new BoardPosition(2, 2),
            new BoardPosition(2, 3)
        );

        Piece pieceAtTarget = board.GetPiece(new BoardPosition(2, 3));

        bool captured = !enemySoldier.IsAlive;
        bool attackerMoved = pieceAtTarget == playerSoldier;
        bool turnChanged = turnManager.CurrentTurnOwner == PieceOwner.Enemy;
        bool battleNotOver = !turnManager.IsBattleOver;

        PrintResult(
            "Expected: success true, enemy soldier dead, player soldier at (2, 3), current turn Enemy, battle not over",
            result && captured && attackerMoved && turnChanged && battleNotOver
        );
    }

    private void TestCaptureEnemyKingEndsBattle()
    {
        Debug.Log("[Test 5] Capturing Enemy King ends battle with Player winner");

        Board board = new Board(5, 5);

        Piece playerKing = new Piece(
            PieceType.King,
            PieceOwner.Player,
            new BoardPosition(2, 2)
        );

        Piece enemyKing = new Piece(
            PieceType.King,
            PieceOwner.Enemy,
            new BoardPosition(2, 3)
        );

        board.PlacePiece(playerKing, playerKing.Position);
        board.PlacePiece(enemyKing, enemyKing.Position);

        TurnManager turnManager = new TurnManager(board);

        bool result = turnManager.TryAct(
            new BoardPosition(2, 2),
            new BoardPosition(2, 3)
        );

        Piece pieceAtTarget = board.GetPiece(new BoardPosition(2, 3));

        bool capturedKing = !enemyKing.IsAlive;
        bool attackerMoved = pieceAtTarget == playerKing;
        bool battleOver = turnManager.IsBattleOver;
        bool playerWon = turnManager.Winner == PieceOwner.Player;
        bool turnNotChanged = turnManager.CurrentTurnOwner == PieceOwner.Player;

        PrintResult(
            "Expected: success true, enemy king dead, battle over, winner Player, turn not changed",
            result && capturedKing && attackerMoved && battleOver && playerWon && turnNotChanged
        );
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