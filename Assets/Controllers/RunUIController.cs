using UnityEngine;

public class RunUIController : MonoBehaviour
{
    [Header("Base Run UI")]
    [SerializeField] private GameObject _fixedLeftUIRoot;
    [SerializeField] private GameObject _goldHUDRoot;
    [SerializeField] private GameObject _boardAreaRoot;

    [Header("State UI")]
    [SerializeField] private GameObject _shopUIRoot;
    [SerializeField] private GameObject _augmentSelectionUIRoot;
    [SerializeField] private GameObject _augmentReplacementUIRoot;

    public void Refresh(RunState state)
    {
        bool showBaseRunUI = state == RunState.Battle || state == RunState.Shop || state == RunState.AugmentSelection || state == RunState.AugmentReplacement;

        _fixedLeftUIRoot.SetActive(showBaseRunUI);
        _goldHUDRoot.SetActive(showBaseRunUI);
        _boardAreaRoot.SetActive(showBaseRunUI);

        _shopUIRoot.SetActive(state == RunState.Shop);
        _augmentSelectionUIRoot.SetActive(state == RunState.AugmentSelection);
        _augmentReplacementUIRoot.SetActive(state == RunState.AugmentReplacement);
    }
}