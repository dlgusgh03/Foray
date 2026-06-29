using UnityEngine;

public class RunManagerDebugRunner : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("===== RunManager Debug Start =====");

        RunManager runManager = new RunManager();

        Debug.Log($"RunManager Created");
        Debug.Log($"Stage Index: {runManager.StageIndex}");
        Debug.Log($"Gold: {runManager.Gold}");
        Debug.Log($"Player Army Count: {runManager.PlayerArmy.Count}");
        Debug.Log($"Player Army Max Slots: {runManager.PlayerArmy.MaxSlots}");

        runManager.StartRun();

        Debug.Log("StartRun() Called");

        if (runManager.CurrentBoard == null)
        {
            Debug.LogError("CurrentBoard is null");
            return;
        }

        if (runManager.CurrentBattleManager == null)
        {
            Debug.LogError("CurrentBattleManager is null");
            return;
        }

        Debug.Log("CurrentBoard Created");
        Debug.Log("CurrentBattleManager Created");

        PrintBoard(runManager.CurrentBoard);
        PrintPieces(runManager.CurrentBoard);

        Debug.Log($"Current Turn: {runManager.CurrentBattleManager.TurnManager.CurrentTurnOwner}");
        Debug.Log($"Is Battle Over: {runManager.CurrentBattleManager.IsBattleOver()}");
        Debug.Log($"Winner: {runManager.CurrentBattleManager.GetWinner()}");

        Debug.Log("===== RunManager Debug End =====");
    }

    private void PrintBoard(Board board)
    {
        Debug.Log("===== Board State =====");

        for (int y = board.Height - 1; y >= 0; y--)
        {
            string line = "";

            for (int x = 0; x < board.Width; x++)
            {
                BoardPosition position = new BoardPosition(x, y);
                BoardCell cell = board.GetCell(position);

                if (cell == null)
                {
                    line += "? ";
                    continue;
                }

                if (cell.IsBlocked)
                {
                    line += "X ";
                    continue;
                }

                Piece piece = cell.Piece;

                if (piece == null)
                {
                    line += ". ";
                    continue;
                }

                line += GetPieceSymbol(piece) + " ";
            }

            Debug.Log(line);
        }

        Debug.Log("Legend: PK=Player King, PS=Player Soldier, EK=Enemy King, ES=Enemy Soldier, X=Blocked, .=Empty");
    }

    private void PrintPieces(Board board)
    {
        Debug.Log("===== Piece List =====");

        int playerPieceCount = 0;
        int enemyPieceCount = 0;

        for (int x = 0; x < board.Width; x++)
        {
            for (int y = 0; y < board.Height; y++)
            {
                BoardPosition position = new BoardPosition(x, y);
                Piece piece = board.GetPiece(position);

                if (piece == null)
                {
                    continue;
                }

                if (piece.Owner == PieceOwner.Player)
                {
                    playerPieceCount++;
                }
                else if (piece.Owner == PieceOwner.Enemy)
                {
                    enemyPieceCount++;
                }

                Debug.Log(piece.ToString());
            }
        }

        Debug.Log($"Player Piece Count: {playerPieceCount}");
        Debug.Log($"Enemy Piece Count: {enemyPieceCount}");
    }

    private string GetPieceSymbol(Piece piece)
    {
        if (piece.Owner == PieceOwner.Player)
        {
            switch (piece.Type)
            {
                case PieceType.King:
                    return "PK";
                case PieceType.Soldier:
                    return "PS";
                case PieceType.Chariot:
                    return "PC";
                case PieceType.Horse:
                    return "PH";
                case PieceType.Cannon:
                    return "PN";
            }
        }

        if (piece.Owner == PieceOwner.Enemy)
        {
            switch (piece.Type)
            {
                case PieceType.King:
                    return "EK";
                case PieceType.Soldier:
                    return "ES";
                case PieceType.Chariot:
                    return "EC";
                case PieceType.Horse:
                    return "EH";
                case PieceType.Cannon:
                    return "EN";
            }
        }

        return "??";
    }
}