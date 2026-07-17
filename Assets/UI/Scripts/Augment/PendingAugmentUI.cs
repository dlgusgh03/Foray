using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PendingAugmentUI :
    MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerClickHandler
{
    [SerializeField] private RectTransform _cardVisual;
    [SerializeField] private Image _iconImage;
    [SerializeField] private Image _rarityBorder;
    [SerializeField] private Sprite _defaultIcon;
    [SerializeField] private RectTransform _tooltipAnchor;
    [SerializeField] private float _expandedScale = 1.08f;

    private Augment _augment;
    private AugmentReplacementUI _owner;
    private bool _isHovered;
    private bool _isSelected;

    public Augment Augment => _augment;
    public Vector3 TooltipPosition => _tooltipAnchor.position;

    public void Initialize(AugmentReplacementUI owner)
    {
        _owner = owner;
    }

    public void Refresh(Augment augment)
    {
        _augment = augment;
        _isHovered = false;
        _isSelected = false;

        RefreshScale();

        bool hasAugment = augment != null;

        _iconImage.gameObject.SetActive(hasAugment);
        _rarityBorder.gameObject.SetActive(hasAugment);

        if (!hasAugment)
        {
            _iconImage.sprite = null;
            return;
        }

        _iconImage.sprite = augment.Icon != null ? augment.Icon : _defaultIcon;
    }

    public void SetSelected(bool selected)
    {
        _isSelected = selected;
        RefreshScale();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_augment == null || _owner == null)
        {
            return;
        }

        _isHovered = true;
        RefreshScale();

        _owner.OnPendingAugmentHovered();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_augment == null || _owner == null)
        {
            return;
        }

        _isHovered = false;
        RefreshScale();

        _owner.OnPendingAugmentHoverExited();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_augment == null || _owner == null)
        {
            return;
        }

        _owner.OnPendingAugmentClicked();
    }

    private void RefreshScale()
    {
        bool shouldExpand = _isHovered || _isSelected;

        _cardVisual.localScale = shouldExpand ? Vector3.one * _expandedScale : Vector3.one;
    }
}