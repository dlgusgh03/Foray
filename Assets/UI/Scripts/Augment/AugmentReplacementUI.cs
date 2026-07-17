using UnityEngine;
using UnityEngine.UI;

public class AugmentReplacementUI : MonoBehaviour
{
    private const int PendingAugmentIndex = 5;

    [SerializeField] private PendingAugmentUI _pendingAugmentUI;
    [SerializeField] private AugmentTooltipUI _tooltip;
    [SerializeField] private Button _confirmButton;
    [SerializeField] private RunController _runController;
    [SerializeField] private OwnedAugmentUI _ownedAugmentUI;

    private int _selectedIndex = -1;
    private int _hoveredIndex = -1;

    private void Awake()
    {
        _pendingAugmentUI.Initialize(this);

        _confirmButton.interactable = false;
        _confirmButton.onClick.AddListener(OnConfirmButtonClicked);
    }

    private void OnDestroy()
    {
        _confirmButton.onClick.RemoveListener(OnConfirmButtonClicked);
    }

    public void Refresh()
    {
        RunManager runManager = _runController.RunManager;

        ClearSelectionState();

        _pendingAugmentUI.Refresh(runManager.PendingAugment);
        _ownedAugmentUI.EnterReplacementMode(this);
    }

    public void OnOwnedAugmentHovered(int index)
    {
        if (!IsOwnedAugmentIndex(index))
        {
            return;
        }

        _hoveredIndex = index;
        RefreshTooltip();
    }

    public void OnOwnedAugmentHoverExited(int index)
    {
        if (!IsOwnedAugmentIndex(index))
        {
            return;
        }

        if (_hoveredIndex == index)
        {
            _hoveredIndex = -1;
        }

        RefreshTooltip();
    }

    public void OnOwnedAugmentClicked(int index)
    {
        if (!IsOwnedAugmentIndex(index))
        {
            return;
        }

        ToggleSelection(index);
    }

    public void OnPendingAugmentHovered()
    {
        _hoveredIndex = PendingAugmentIndex;
        RefreshTooltip();
    }

    public void OnPendingAugmentHoverExited()
    {
        if (_hoveredIndex == PendingAugmentIndex)
        {
            _hoveredIndex = -1;
        }

        RefreshTooltip();
    }

    public void OnPendingAugmentClicked()
    {
        ToggleSelection(PendingAugmentIndex);
    }

    private void ToggleSelection(int index)
    {
        if (!IsValidCardIndex(index))
        {
            return;
        }

        if (_selectedIndex == index)
        {
            SetCardSelected(_selectedIndex, false);

            _selectedIndex = -1;
            _confirmButton.interactable = false;

            RefreshTooltip();
            return;
        }

        if (_selectedIndex >= 0)
        {
            SetCardSelected(_selectedIndex, false);
        }

        _selectedIndex = index;

        SetCardSelected(_selectedIndex, true);
        _confirmButton.interactable = true;

        RefreshTooltip();
    }

    private void SetCardSelected(int index, bool selected)
    {
        if (IsOwnedAugmentIndex(index))
        {
            _ownedAugmentUI.SetSlotSelected(index, selected);
            return;
        }

        if (index == PendingAugmentIndex)
        {
            _pendingAugmentUI.SetSelected(selected);
        }
    }

    private void RefreshTooltip()
    {
        int displayIndex = _hoveredIndex >= 0
            ? _hoveredIndex
            : _selectedIndex;

        if (!IsValidCardIndex(displayIndex))
        {
            _tooltip.Hide();
            return;
        }

        Augment augment;
        Vector3 tooltipPosition;

        if (displayIndex == PendingAugmentIndex)
        {
            augment = _pendingAugmentUI.Augment;
            tooltipPosition = _pendingAugmentUI.TooltipPosition;
        }
        else
        {
            augment = _ownedAugmentUI.GetAugment(displayIndex);
            tooltipPosition = _ownedAugmentUI.GetTooltipPosition(displayIndex);
        }

        if (augment == null)
        {
            _tooltip.Hide();
            return;
        }

        _tooltip.transform.position = tooltipPosition;
        _tooltip.Show(augment);
    }

    private void OnConfirmButtonClicked()
    {
        if (!IsValidCardIndex(_selectedIndex))
        {
            return;
        }

        int confirmedIndex = _selectedIndex;

        ClearSelectionState();
        _ownedAugmentUI.ExitReplacementMode();

        if (IsOwnedAugmentIndex(confirmedIndex))
        {
            _runController.OnAugmentReplaced(confirmedIndex);
            return;
        }

        _runController.OnAugmentReplacementSkipped();
    }

    private void ClearSelectionState()
    {
        if (_selectedIndex >= 0)
        {
            SetCardSelected(_selectedIndex, false);
        }

        _selectedIndex = -1;
        _hoveredIndex = -1;

        _confirmButton.interactable = false;
        _tooltip.Hide();
    }

    private static bool IsOwnedAugmentIndex(int index)
    {
        return index >= 0 && index < PendingAugmentIndex;
    }

    private static bool IsValidCardIndex(int index)
    {
        return index >= 0 && index <= PendingAugmentIndex;
    }
}