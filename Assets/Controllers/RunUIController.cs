using UnityEngine;

public class RunUIController : MonoBehaviour
{
    [Header("Base Run UI")]
    [SerializeField] private RectTransform _mainRunContentRoot;
    [SerializeField] private GameObject _goldHUDRoot;
    [SerializeField] private RectTransform _ownedCardUIRoot;

    [Header("State UI")]
    [SerializeField] private GameObject _shopUIRoot;
    [SerializeField] private GameObject _augmentSelectionUIRoot;
    [SerializeField] private GameObject _augmentReplacementUIRoot;

    [Header("Battle Layout")]
    [SerializeField] private Vector2 _battleMainContentPosition = Vector2.zero;
    [SerializeField] private Vector2 _battleOwnedCardPosition = Vector2.zero;

    [Header("Shop Layout")]
    [SerializeField] private Vector2 _shopMainContentPosition = new Vector2(-300f, 0f);
    [SerializeField] private Vector2 _shopOwnedCardPosition = new Vector2(-100f, 0f);

    public void Refresh(RunState state)
    {
        bool showBaseRunUI = state == RunState.Battle || state == RunState.Shop || state == RunState.AugmentSelection || state == RunState.AugmentReplacement;

        _mainRunContentRoot.gameObject.SetActive(showBaseRunUI);
        _goldHUDRoot.SetActive(showBaseRunUI);
        _ownedCardUIRoot.gameObject.SetActive(showBaseRunUI);

        _shopUIRoot.SetActive(state == RunState.Shop);
        _augmentSelectionUIRoot.SetActive(state == RunState.AugmentSelection);
        _augmentReplacementUIRoot.SetActive(state == RunState.AugmentReplacement);

        if (state == RunState.Shop)
        {
            ApplyShopLayout();
        }
        else if (showBaseRunUI)
        {
            ApplyBattleLayout();
        }
    }

    private void ApplyBattleLayout()
    {
        _mainRunContentRoot.anchoredPosition = _battleMainContentPosition;

        _ownedCardUIRoot.anchoredPosition = _battleOwnedCardPosition;
    }

    private void ApplyShopLayout()
    {
        _mainRunContentRoot.anchoredPosition = _shopMainContentPosition;

        _ownedCardUIRoot.anchoredPosition = _shopOwnedCardPosition;
    }
}