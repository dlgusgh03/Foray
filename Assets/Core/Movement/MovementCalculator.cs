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
                return GetSoldierMovablePositions(board, piece);

            case PieceType.Chariot:
                return GetChariotMovablePositions(board, piece);

            case PieceType.Horse:
                return GetHorseMovablePositions(board, piece);

            case PieceType.Cannon:
                return new List<BoardPosition>();

            default:
                return new List<BoardPosition>();
        }
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

    private static List<BoardPosition> GetSoldierMovablePositions(Board board, Piece piece)
    {
        List<BoardPosition> movablePositions = new List<BoardPosition>();

        if (board == null || piece == null)
        {
            return movablePositions;
        }

        BoardPosition position = piece.Position;

        int forwardY = piece.Owner == PieceOwner.Player ? 1 : -1;

        BoardPosition forward = position.Add(0, forwardY);
        BoardPosition left = position.Add(-1, 0);
        BoardPosition right = position.Add(1, 0);

        TryAddPosition(movablePositions, board, piece, forward);
        TryAddPosition(movablePositions, board, piece, left);
        TryAddPosition(movablePositions, board, piece, right);

        return movablePositions;
    }

    private static List<BoardPosition> GetChariotMovablePositions(Board board, Piece piece)
    {
        List<BoardPosition> movablePositions = new List<BoardPosition>();

        if (board == null || piece == null)
        {
            return movablePositions;
        }

        BoardPosition position = piece.Position;

        AddStraightMovablePositions(movablePositions, board, piece, position, 0, 1);
        AddStraightMovablePositions(movablePositions, board, piece, position, 0, -1);
        AddStraightMovablePositions(movablePositions, board, piece, position, -1, 0);
        AddStraightMovablePositions(movablePositions, board, piece, position, 1, 0);

        return movablePositions;
    }

    private static void AddStraightMovablePositions(List<BoardPosition> positions, Board board, Piece piece, BoardPosition start, int xOffset, int yOffset)
    {
        BoardPosition target = start.Add(xOffset, yOffset);

        while (board.IsInside(target))
        {
            if (!board.IsWalkable(target))
            {
                break;
            }

            TryAddPosition(positions, board, piece, target);

            if (board.IsOccupied(target))
            {
                break;
            }

            target = target.Add(xOffset, yOffset);
        }
    }

    private static List<BoardPosition> GetHorseMovablePositions(Board board, Piece piece)
    {
        List<BoardPosition> movablePositions = new List<BoardPosition>();

        if (board == null || piece == null)
        {
            return movablePositions;
        }

        BoardPosition position = piece.Position;

        AddHorseMoveIfAvailable(movablePositions, board, piece, position, 0, 1, -1, 2);
        AddHorseMoveIfAvailable(movablePositions, board, piece, position, 0, 1, 1, 2);
        AddHorseMoveIfAvailable(movablePositions, board, piece, position, 0, -1, -1, -2);
        AddHorseMoveIfAvailable(movablePositions, board, piece, position, 0, -1, 1, -2);
        AddHorseMoveIfAvailable(movablePositions, board, piece, position, -1, 0, -2, 1);
        AddHorseMoveIfAvailable(movablePositions, board, piece, position, -1, 0, -2, -1);
        AddHorseMoveIfAvailable(movablePositions, board, piece, position, 1, 0, 2, 1);
        AddHorseMoveIfAvailable(movablePositions, board, piece, position, 1, 0, 2, -1);

        return movablePositions;
    }

    private static void AddHorseMoveIfAvailable(List<BoardPosition> positions, Board board, Piece piece, BoardPosition start, int blockXOffset, int blockYOffset, int targetXOffset, int targetYOffset)
    {
        BoardPosition blockPosition = start.Add(blockXOffset, blockYOffset);

        if (IsHorseBlocked(board, blockPosition))
        {
            return;
        }

        BoardPosition target = start.Add(targetXOffset, targetYOffset);
        TryAddPosition(positions, board, piece, target);
    }

    private static bool IsHorseBlocked(Board board, BoardPosition blockPosition)
    {
        if (board == null)
        {
            return true;
        }

        if (!board.IsInside(blockPosition))
        {
            return true;
        }

        if (!board.IsWalkable(blockPosition))
        {
            return true;
        }

        if (board.IsOccupied(blockPosition))
        {
            return true;
        }

        return false;
    }
}