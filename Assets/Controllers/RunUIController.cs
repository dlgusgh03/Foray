using UnityEngine;

public class RunUIController : MonoBehaviour
{
    [SerializeField] private GameObject _battleUIRoot;
    [SerializeField] private GameObject _shopUIRoot;
    [SerializeField] private GameObject _augmentUIRoot;

    public void Refresh(RunState state)
    {
        _battleUIRoot.SetActive(state == RunState.Battle);
        _shopUIRoot.SetActive(state == RunState.Shop);
        _augmentUIRoot.SetActive(state == RunState.AugmentSelection);
    }
}