using System.Collections.Generic;
using UnityEngine;

public class OwnedAugmentUI : MonoBehaviour
{
    private const int NormalSortingOrder = 0;
    private const int ReplacementSortingOrder = 20;

    [SerializeField] private OwnedAugmentSlotUI[] _slots;
    [SerializeField] private AugmentTooltipUI _tooltip;
    [SerializeField] private Canvas _sortingCanvas;

    private int _selectedSlotIndex = -1;
    private int _hoveredSlotIndex = -1;
    private bool _isReplacementMode;
    private AugmentReplacementUI _replacementUI;

    private void Awake()
    {
        _sortingCanvas.overrideSorting = true;
        _sortingCanvas.sortingOrder = NormalSortingOrder;
        
        for (int i = 0; i < _slots.Length; i++)
        {
            _slots[i].Initialize(this, i);
        }

        _tooltip.Hide();
    }

    public void Refresh(IReadOnlyList<Augment> augments)
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            _slots[i].gameObject.SetActive(true);

            Augment augment = i < augments.Count ? augments[i] : null;

            _slots[i].Refresh(augment);
        }
    }

    public void ClearSelection()
    {
        if (_selectedSlotIndex >= 0)
        {
            _slots[_selectedSlotIndex].SetSelected(false);
        }

        _selectedSlotIndex = -1;
        _hoveredSlotIndex = -1;
        _tooltip.Hide();
    }

    public void OnSlotHovered(int index)
    {
        _hoveredSlotIndex = index;

        if (_isReplacementMode)
        {
            _replacementUI?.OnOwnedAugmentHovered(index);
            return;
        }

        RefreshTooltip();
    }

    public void OnSlotHoverExited(int index)
    {
        if (_hoveredSlotIndex == index)
        {
            _hoveredSlotIndex = -1;
        }

        if (_isReplacementMode)
        {
            _replacementUI?.OnOwnedAugmentHoverExited(index);
            return;
        }

        RefreshTooltip();
    }

    public void OnSlotClicked(int index)
    {
        if (_isReplacementMode)
        {
            _replacementUI?.OnOwnedAugmentClicked(index);
            return;
        }

        if (_selectedSlotIndex == index)
        {
            _slots[_selectedSlotIndex].SetSelected(false);
            _selectedSlotIndex = -1;
        }
        else
        {
            if (_selectedSlotIndex >= 0)
            {
                _slots[_selectedSlotIndex].SetSelected(false);
            }

            _slots[index].SetSelected(true);
            _selectedSlotIndex = index;
        }

        RefreshTooltip();
    }

    public void EnterReplacementMode(AugmentReplacementUI replacementUI)
    {
        ClearSelection();

        _isReplacementMode = true;
        _replacementUI = replacementUI;
        _sortingCanvas.sortingOrder = ReplacementSortingOrder;
    }

    public void ExitReplacementMode()
    {
        _isReplacementMode = false;
        _replacementUI = null;
        _sortingCanvas.sortingOrder = NormalSortingOrder;

        ClearSelection();
    }

    public void SetSlotSelected(int index, bool selected)
    {
        if (index < 0 || index >= _slots.Length)
        {
            return;
        }

        _slots[index].SetSelected(selected);
    }

    public Augment GetAugment(int index)
    {
        if (index < 0 || index >= _slots.Length)
        {
            return null;
        }

        return _slots[index].Augment;
    }

    public Vector3 GetTooltipPosition(int index)
    {
        if (index < 0 || index >= _slots.Length)
        {
            return Vector3.zero;
        }

        return _slots[index].TooltipPosition;
    }

    private void RefreshTooltip()
    {
        int displayIndex = _hoveredSlotIndex >= 0 ? _hoveredSlotIndex : _selectedSlotIndex;

        if (displayIndex < 0)
        {
            _tooltip.Hide();
            return;
        }

        OwnedAugmentSlotUI slot = _slots[displayIndex];
        Augment augment = slot.Augment;

        if (augment == null)
        {
            _tooltip.Hide();
            return;
        }

        _tooltip.transform.position = slot.TooltipPosition;
        _tooltip.Show(augment);
    }
}