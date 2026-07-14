using UnityEngine;
using System.Collections.Generic;

public class RunController : MonoBehaviour
{
    [SerializeField] private BoardUI _boardUI;
    [SerializeField] private BattleResultUI _battleResultUI;
    [SerializeField] private RunUIController _runUIController;
    [SerializeField] private RunStatusUI _runStatusUI;

    private RunManager _runManager;
    private Piece _selectedPiece;
    private BoardPosition _selectedPosition;
    private List<BoardPosition> _movablePositions = new List<BoardPosition>();

    public RunManager RunManager => _runManager;

    private void Awake()
    {
        _runManager = new RunManager();
        _runManager.StartRun();

        _runUIController.Refresh(_runManager.CurrentState);
        _runStatusUI.Refresh(_runManager);
    }

    public bool OnCellClicked(BoardPosition position)
    {
        BattleManager battleManager = _runManager.CurrentBattleManager;

        if (battleManager == null)
        {
            return false;
        }

        if (battleManager.IsBattleOver())
        {
            return false;
        }

        if (battleManager.TurnManager.CurrentTurnOwner
            != PieceOwner.Player)
        {
            return false;
        }

        Board board = _runManager.CurrentBoard;
        Piece clickedPiece = board.GetPiece(position);

        // 선택이 없는 상태
        if (_selectedPiece == null)
        {
            if (clickedPiece == null || clickedPiece.Owner != PieceOwner.Player)
            {
                return false;
            }

            SelectPiece(clickedPiece, position, board);
            return true;
        }

        // 같은 기물을 다시 클릭하면 선택 취소
        if (position.Equals(_selectedPosition))
        {
            ClearSelection();
            return false;
        }

        // 다른 아군 기물을 클릭하면 선택 변경
        if (clickedPiece != null && clickedPiece.Owner == PieceOwner.Player)
        {
            ClearSelection();
            SelectPiece(clickedPiece, position, board);
            return true;
        }

        // 이동 또는 포획
        if (_movablePositions.Contains(position))
        {
            bool acted = _runManager.CurrentBattleManager.PlayerAct(_selectedPosition, position);

            ClearSelection();

            if (acted)
            {
                _boardUI.RefreshBoard();

                CheckBattleEnd();

                if (!battleManager.IsBattleOver())
                {
                    battleManager.EnemyAct();
                    _boardUI.RefreshBoard();
                    CheckBattleEnd();
                }
            }

            return false;
        }

        // 이동 불가능한 빈 칸 또는 적 기물 클릭
        ClearSelection();
        return false;
    }

    public void OnBattleResultContinue()
    {
        _runManager.ResolveCurrentBattle();
        _battleResultUI.Hide();

        _runUIController.Refresh(_runManager.CurrentState);
        _runStatusUI.Refresh(_runManager);

        Debug.Log(
         $"Continue clicked. " +
         $"State: {_runManager.CurrentState}, " +
         $"Stage: {_runManager.StageIndex}, " +
         $"Round: {_runManager.RoundIndex}, " +
         $"Gold: {_runManager.Gold}"
         );
    }

    public void OnLeaveShop()
    {
        _runManager.LeaveShop();

        if (_runManager.CurrentState != RunState.Battle)
        {
            return;
        }

        _boardUI.RebuildBoard(_runManager.CurrentBoard);
        _runUIController.Refresh(_runManager.CurrentState);
        _runStatusUI.Refresh(_runManager);
    }

    private void SelectPiece(Piece piece, BoardPosition position, Board board)
    {
        _selectedPiece = piece;
        _selectedPosition = position;
        _movablePositions = MovementCalculator.GetMovablePositions(board, piece);

        _boardUI.HighlightMovableCells(_movablePositions);
    }

    private void ClearSelection()
    {
        _selectedPiece = null;
        _movablePositions.Clear();
        _boardUI.ClearSelection();
    }

    private void CheckBattleEnd()
    {
        BattleManager battleManager = _runManager.CurrentBattleManager;

        if (!battleManager.IsBattleOver())
        {
            return;
        }

        PieceOwner? winner = battleManager.GetWinner();

        if (winner == PieceOwner.Player)
        {
            _battleResultUI.ShowVictory();
        }
        else if (winner == PieceOwner.Enemy)
        {
            _battleResultUI.ShowDefeat();
        }

        Debug.Log($"Battle Over. Winner: {winner}");
    }
}