using System.Collections.Generic;

public class EnemyAI
{
    private readonly Board _board;

    public EnemyAI(Board board)
    {
        _board = board;
    }

    public BattleAction DecideAction()
    {
        BattleAction action = FindCaptureKingAction();

        if (action != null)
        {
            return action;
        }

        action = FindAnyCaptureAction();

        if (action != null)
        {
            return action;
        }

        action = FindMoveTowardPlayerKingAction();

        if (action != null)
        {
            return action;
        }

        return null;
    }

    private List<Piece> GetAlivePieces(PieceOwner owner)
    {
        List<Piece> pieces = new List<Piece>();

        if (_board == null)
        {
            return pieces;
        }

        for (int x = 0; x < _board.Width; x++)
        {
            for (int y = 0; y < _board.Height; y++)
            {
                BoardPosition position = new BoardPosition(x, y);
                Piece piece = _board.GetPiece(position);

                if (piece == null)
                {
                    continue;
                }

                if (!piece.IsAlive)
                {
                    continue;
                }

                if (!piece.IsOwnedBy(owner))
                {
                    continue;
                }

                pieces.Add(piece);
            }
        }

        return pieces;
    }

    private Piece FindPlayerKing()
    {
        List<Piece> playerPieces = GetAlivePieces(PieceOwner.Player);

        foreach (Piece piece in playerPieces)
        {
            if (piece.Type == PieceType.King)
            {
                return piece;
            }
        }

        return null;
    }

    private BattleAction FindCaptureKingAction()
    {
        Piece playerKing = FindPlayerKing();

        if(playerKing == null)
        {
            return null;
        }

        List<Piece> enemyPieces = GetAlivePieces(PieceOwner.Enemy);

        foreach (Piece piece in enemyPieces)
        {
            List<BoardPosition> movablePositions = MovementCalculator.GetMovablePositions(_board, piece);

            foreach (BoardPosition position in movablePositions)
            {
                if(position.Equals(playerKing.Position))
                {
                    return new BattleAction(piece.Position, position);
                }
            }
        }

        return null;
    }

    private BattleAction FindAnyCaptureAction()
    {
        List<Piece> enemyPieces = GetAlivePieces(PieceOwner.Enemy);

        foreach (Piece piece in enemyPieces)
        {
            List<BoardPosition> movablePositions = MovementCalculator.GetMovablePositions(_board, piece);

            foreach (BoardPosition position in movablePositions)
            {
                Piece targetPiece = _board.GetPiece(position);

                if(targetPiece == null)
                {
                    continue;
                }

                if (!targetPiece.IsOwnedBy(PieceOwner.Player))
                {
                    continue;
                }

                return new BattleAction(piece.Position, position);
            }
        }

        return null;
    }

    private BattleAction FindMoveTowardPlayerKingAction()
    {
        Piece playerKing = FindPlayerKing();

        if (playerKing == null)
        {
            return null;
        }

        BattleAction bestAction = null;
        int bestDistance = int.MaxValue;

        List<Piece> enemyPieces = GetAlivePieces(PieceOwner.Enemy);

        foreach (Piece piece in enemyPieces)
        {
            List<BoardPosition> movablePositions = MovementCalculator.GetMovablePositions(_board, piece);

            foreach (BoardPosition position in movablePositions)
            {
                Piece targetPiece = _board.GetPiece(position);

                if(targetPiece != null)
                {
                    continue;
                }

                int distance = GetDistance(position, playerKing.Position);
                if(distance < bestDistance)
                {
                    bestDistance = distance;
                    bestAction = new BattleAction(piece.Position, position);
                }
            }
        }

        return bestAction;
    }

    private int GetDistance(BoardPosition a, BoardPosition b)
    {
        int xDistance = a.X - b.X;
        int yDistance = a.Y - b.Y;

        if (xDistance < 0)
        {
            xDistance = -xDistance;
        }

        if (yDistance < 0)
        {
            yDistance = -yDistance;
        }

        return xDistance + yDistance;
    }
}