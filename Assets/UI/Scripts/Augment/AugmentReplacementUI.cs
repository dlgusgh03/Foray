using UnityEngine;
using UnityEngine.UI;

public class AugmentReplacementUI : MonoBehaviour
{
    private const int PendingAugmentIndex = 5;

    [SerializeField] private PendingAugmentUI _pendingAugmentUI;
    [SerializeField] private AugmentTooltipUI _tooltip;
    [SerializeField] private Button _confirmButton;
    [SerializeField] private RunController _runController;

    private int _selectedIndex = -1;
    private bool _isPendingHovered;

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
        Augment pendingAugment = runManager.PendingAugment;

        _selectedIndex = -1;
        _isPendingHovered = false;

        _pendingAugmentUI.Refresh(pendingAugment);

        _confirmButton.interactable = false;
        _tooltip.Hide();
    }

    public void OnPendingAugmentHovered()
    {
        _isPendingHovered = true;
        ShowPendingAugmentTooltip();
    }

    public void OnPendingAugmentHoverExited()
    {
        _isPendingHovered = false;

        if (_selectedIndex == PendingAugmentIndex)
        {
            ShowPendingAugmentTooltip();
            return;
        }

        _tooltip.Hide();
    }

    public void OnPendingAugmentClicked()
    {
        if (_selectedIndex == PendingAugmentIndex)
        {
            _selectedIndex = -1;

            _pendingAugmentUI.SetSelected(false);
            _confirmButton.interactable = false;

            if (!_isPendingHovered)
            {
                _tooltip.Hide();
            }

            return;
        }

        _selectedIndex = PendingAugmentIndex;

        _pendingAugmentUI.SetSelected(true);
        _confirmButton.interactable = true;

        ShowPendingAugmentTooltip();
    }

    private void ShowPendingAugmentTooltip()
    {
        Augment augment = _pendingAugmentUI.Augment;

        if (augment == null)
        {
            _tooltip.Hide();
            return;
        }

        _tooltip.transform.position = _pendingAugmentUI.TooltipPosition;
        _tooltip.Show(augment);
    }

    private void OnConfirmButtonClicked()
    {
        if (_selectedIndex != PendingAugmentIndex)
        {
            return;
        }

        _selectedIndex = -1;
        _isPendingHovered = false;

        _pendingAugmentUI.SetSelected(false);
        _confirmButton.interactable = false;
        _tooltip.Hide();

        _runController.OnAugmentReplacementSkipped();
    }
}