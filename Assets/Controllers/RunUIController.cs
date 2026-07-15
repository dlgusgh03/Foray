using UnityEngine;

public class RunUIController : MonoBehaviour
{
    [SerializeField] private GameObject _battleUIRoot;
    [SerializeField] private GameObject _shopUIRoot;
    [SerializeField] private GameObject _augmentSelectionUIRoot;
    [SerializeField] private GameObject _augmentReplacementUIRoot;

    public void Refresh(RunState state)
    {
        _battleUIRoot.SetActive(state == RunState.Battle);
        _shopUIRoot.SetActive(state == RunState.Shop);
        _augmentSelectionUIRoot.SetActive(state == RunState.AugmentSelection);
        _augmentReplacementUIRoot.SetActive(state == RunState.AugmentReplacement);
    }
}