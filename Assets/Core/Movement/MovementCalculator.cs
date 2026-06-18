using System.Collections.Generic;

public static class MovementCalculator
{
    public static List<BoardPosition> GetMovablePositions(Board board, Piece piece)
    {
        if (board == null || piece == null)
        {
            return new List<BoardPosition>();
        }

        if (!piece.IsAlive)
        {
            return new List<BoardPosition>();
        }

        switch (piece.Type)
        {
            case PieceType.King:
                return GetKingMovablePositions(board, piece);

            case PieceType.Soldier:
                return new List<BoardPosition>();

            case PieceType.Chariot:
                return new List<BoardPosition>();

            case PieceType.Horse:
                return new List<BoardPosition>();

            case PieceType.Cannon:
                return new List<BoardPosition>();

            default:
                return new List<BoardPosition>();
        }
    }

    private static List<BoardPosition> GetKingMovablePositions(Board board, Piece piece)
    {
        if(piece == null || board == null)
        {
            return new List<BoardPosition>();
        }
        List<BoardPosition> movablePositions = new List<BoardPosition>();
        BoardPosition position = piece.Position;
        int x = position.X;
        int y = position.Y;
        for(int i = x - 1; i <= x + 1; ++i)
        {
            for(int j = y - 1; j <= y + 1; ++j)
            {
                if(i == x && j == y)
                {
                    continue;
                }

                BoardPosition target = new BoardPosition(i, j);
                TryAddPosition(movablePositions, board, piece, target);
            }
        }
        return movablePositions;
    }

    private static void TryAddPosition(List<BoardPosition> positions, Board board, Piece piece, BoardPosition target)
    {
        if (CanMoveTo(board, piece, target))
        {
            positions.Add(target);
        }
    }

    private static bool CanMoveTo(Board board, Piece piece, BoardPosition target)
    {
        if (board == null || piece == null)
        {
            return false;
        }

        if (!board.IsInside(target))
        {
            return false;
        }

        if (!board.IsWalkable(target))
        {
            return false;
        }

        Piece targetPiece = board.GetPiece(target);

        if (targetPiece == null)
        {
            return true;
        }

        return targetPiece.Owner != piece.Owner;
    }
}