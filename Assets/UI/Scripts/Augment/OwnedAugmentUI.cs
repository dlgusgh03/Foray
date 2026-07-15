using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OwnedAugmentUI : MonoBehaviour
{
    [SerializeField] private OwnedAugmentSlotUI[] _slots;
    [SerializeField] private AugmentTooltipUI _tooltip;

    private bool _isTooltipPinned;
    private Augment _pinnedAugment;

    private void Awake()
    {
        foreach (OwnedAugmentSlotUI slot in _slots)
        {
            slot.Initialize(this);
        }

        _tooltip.Hide();
    }

    public void Refresh(IReadOnlyList<Augment> ownedAugments)
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            Augment augment = i < ownedAugments.Count ? ownedAugments[i] : null;

            _slots[i].Refresh(augment);
        }

        if (_pinnedAugment != null && !ownedAugments.Contains(_pinnedAugment))
        {
            CloseTooltip();
        }
    }

    public void ShowTooltip(Augment augment, Vector3 slotPosition)
    {
        if (_isTooltipPinned)
        {
            return;
        }

        _tooltip.transform.position = slotPosition;
        _tooltip.Show(augment);
    }

    public void HideTooltipUnlessPinned()
    {
        if (!_isTooltipPinned)
        {
            _tooltip.Hide();
        }
    }

    public void TogglePinnedTooltip(Augment augment, Vector3 slotPosition)
    {
        if (_isTooltipPinned && _pinnedAugment == augment)
        {
            CloseTooltip();
            return;
        }

        _isTooltipPinned = true;
        _pinnedAugment = augment;

        _tooltip.transform.position = slotPosition;
        _tooltip.Show(augment);
    }

    public void CloseTooltip()
    {
        _isTooltipPinned = false;
        _pinnedAugment = null;
        _tooltip.Hide();
    }
}