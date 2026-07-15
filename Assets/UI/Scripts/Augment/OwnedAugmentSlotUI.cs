using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OwnedAugmentSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private Sprite _defaultIcon;

    private Augment _augment;
    private OwnedAugmentUI _owner;

    public void Initialize(OwnedAugmentUI owner)
    {
        _owner = owner;
    }

    public void Refresh(Augment augment)
    {
        _augment = augment;

        bool hasAugment = augment != null;

        _iconImage.gameObject.SetActive(hasAugment);

        if (!hasAugment)
        {
            _iconImage.sprite = null;
            return;
        }

        _iconImage.sprite = augment.Icon != null ? augment.Icon : _defaultIcon;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_augment == null)
        {
            return;
        }

        _owner.ShowTooltip(_augment, transform.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _owner.HideTooltipUnlessPinned();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_augment == null)
        {
            return;
        }

        _owner.TogglePinnedTooltip(_augment, transform.position);
    }
}