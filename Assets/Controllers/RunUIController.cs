using UnityEngine;

public class RunUIController : MonoBehaviour
{
    [Header("Base Run UI")]
    [SerializeField] private GameObject _ownedAugmentUIRoot;
    [SerializeField] private GameObject _stageRoundUIRoot;
    [SerializeField] private RectTransform _boardContentRoot;
    [SerializeField] private GameObject _goldHUDRoot;
    [SerializeField] private GameObject _ownedCardUIRoot;

    [Header("State UI")]
    [SerializeField] private GameObject _shopUIRoot;
    [SerializeField] private GameObject _augmentSelectionUIRoot;
    [SerializeField] private GameObject _augmentReplacementUIRoot;

    [Header("Battle Layout")]
    [SerializeField] private Vector2 _battleBoardPosition = Vector2.zero;

    [Header("Shop Layout")]
    [SerializeField]
    private Vector2 _shopBoardPosition =
        new Vector2(-300f, 0f);

    public void Refresh(RunState state)
    {
        bool showBaseRunUI = state == RunState.Battle || state == RunState.Shop || state == RunState.CardSelection || state == RunState.AugmentSelection || state == RunState.AugmentReplacement;

        _ownedAugmentUIRoot.SetActive(showBaseRunUI);
        _stageRoundUIRoot.SetActive(showBaseRunUI);
        _boardContentRoot.gameObject.SetActive(showBaseRunUI);
        _goldHUDRoot.SetActive(showBaseRunUI);
        _ownedCardUIRoot.SetActive(showBaseRunUI);

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
        _boardContentRoot.anchoredPosition = _battleBoardPosition;
    }

    private void ApplyShopLayout()
    {
        _boardContentRoot.anchoredPosition = _shopBoardPosition;
    }
}