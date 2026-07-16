using System.Collections.Generic;
using UnityEngine;

public class OwnedAugmentUI : MonoBehaviour
{
    [SerializeField] private OwnedAugmentSlotUI[] _slots;
    [SerializeField] private AugmentTooltipUI _tooltip;

    private int _selectedSlotIndex = -1;
    private int _hoveredSlotIndex = -1;
    private bool _isReplacementMode;

    private void Awake()
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            _slots[i].Initialize(this, i);
        }

        _tooltip.Hide();
    }

    public void Refresh(IReadOnlyList<Augment> ownedAugments)
    {
        ClearSelection();

        for (int i = 0; i < _slots.Length; i++)
        {
            bool hasAugment = i < ownedAugments.Count;

            _slots[i].gameObject.SetActive(hasAugment);

            if (!hasAugment)
            {
                continue;
            }

            _slots[i].Refresh(ownedAugments[i]);
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

        RefreshTooltip();
    }

    public void OnSlotHoverExited(int index)
    {
        if (_hoveredSlotIndex == index)
        {
            _hoveredSlotIndex = -1;
        }

        RefreshTooltip();
    }

    public void OnSlotClicked(int index)
    {
        if (_selectedSlotIndex == index)
        {
            _slots[_selectedSlotIndex].SetSelected(false);
            _selectedSlotIndex = -1;
        }
        else
        {
            if(_selectedSlotIndex  != -1)
            {
                _slots[_selectedSlotIndex].SetSelected(false);
                
            }
            _slots[index].SetSelected(true);
            _selectedSlotIndex = index;
        }

        RefreshTooltip();
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

        _tooltip.transform.position = slot.TooltipPosition;
        _tooltip.Show(augment);
    }
}