using System.Collections.Generic;

public class TurnManager
{
    private readonly Board _board;

    private PieceOwner _currentTurnOwner;
    private bool _isBattleOver;
    private PieceOwner? _winner;

    public PieceOwner CurrentTurnOwner => _currentTurnOwner;
    public bool IsBattleOver => _isBattleOver;
    public PieceOwner? Winner => _winner;

    public TurnManager(Board board)
    {
        _board = board;
        _currentTurnOwner = PieceOwner.Player;
        _isBattleOver = false;
        _winner = null;
    }

    public bool TryAct(BoardPosition from, BoardPosition to)
    {
        if (_isBattleOver)
        {
            return false;
        }

        Piece piece = _board.GetPiece(from);

        if(piece == null)
        {
            return false;
        }

        if (!IsCurrentTurnPiece(piece))
        {
            return false;
        }

        if(!CanActTo(piece, to))
        {
            return false;
        }

        Piece captured = ExecuteAction(piece, to);
        CheckBattleEnd(captured);

        if (!_isBattleOver)
        {
            SwitchTurn();
        }

        return true;
    }

    private bool IsCurrentTurnPiece(Piece piece)
    {
        if (piece == null) return false;

        return piece.IsOwnedBy(_currentTurnOwner);
    }

    private bool CanActTo(Piece piece, BoardPosition target)
    {
        List<BoardPosition> movablePositions = MovementCalculator.GetMovablePositions(_board, piece);
        bool containsTarget = false;
        foreach (BoardPosition position in movablePositions)
        {
            if (position.Equals(target))
            {
                containsTarget = true;
                break;
            }
        }
        return containsTarget;
    }

    private Piece ExecuteAction(Piece piece, BoardPosition target)
    {
        if(piece == null)
        {
            return null;
        }

        if(!CanActTo(piece, target))
        {
            return null;
        }

        if (!_board.IsOccupied(target))
        {
            _board.MovePiece(piece.Position, target);
            return null;
        }

        return _board.CapturePiece(piece.Position, target);
    }

    private void CheckBattleEnd(Piece capturedPiece)
    {
        if (capturedPiece == null) return;

        if (capturedPiece.Type != PieceType.King) return;

        _isBattleOver = true;

        if(capturedPiece.Owner == PieceOwner.Player)
        {
            _winner = PieceOwner.Enemy;
        }
        else
        {
            _winner = PieceOwner.Player;
        }
    }

    private void SwitchTurn()
    {
        _currentTurnOwner = _currentTurnOwner == PieceOwner.Player ? PieceOwner.Enemy : PieceOwner.Player;
    }

#if UNITY_EDITOR
    public void DebugForceBattleResult(PieceOwner winner)
    {
        _isBattleOver = true;
        _winner = winner;
    }
#endif
}