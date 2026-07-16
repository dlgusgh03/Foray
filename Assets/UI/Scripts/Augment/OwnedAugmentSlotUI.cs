using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OwnedAugmentSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private RectTransform _cardVisual;
    [SerializeField] private Image _iconImage;
    [SerializeField] private Image _rarityBorder;
    [SerializeField] private Sprite _defaultIcon;
    [SerializeField] private float _expandedScale = 1.08f;
    [SerializeField] private RectTransform _tooltipAnchor;

    private Augment _augment;
    private OwnedAugmentUI _owner;
    private int _index;
    private bool _isHovered;
    private bool _isSelected;

    public Augment Augment => _augment;
    public Vector3 TooltipPosition => _tooltipAnchor.position;

    public void Initialize(OwnedAugmentUI owner, int index)
    {
        _owner = owner;
        _index = index;
    }

    public void Refresh(Augment augment)
    {
        _augment = augment;

        if (augment == null)
        {
            _iconImage.sprite = null;
            return;
        }

        _iconImage.sprite = augment.Icon != null ? augment.Icon : _defaultIcon;

        _iconImage.gameObject.SetActive(true);
        _rarityBorder.gameObject.SetActive(true);
    }

    public void SetSelected(bool selected)
    {
        _isSelected = selected;
        RefreshScale();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_augment == null)
        {
            return;
        }

        _isHovered = true;
        RefreshScale();

        _owner.OnSlotHovered(_index);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isHovered = false;
        RefreshScale();

        _owner.OnSlotHoverExited(_index);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_augment == null)
        {
            return;
        }

        _owner.OnSlotClicked(_index);
    }

    private void RefreshScale()
    {
        bool shouldExpand = _isHovered || _isSelected;

        _cardVisual.localScale = shouldExpand ? Vector3.one * _expandedScale : Vector3.one;
    }
}