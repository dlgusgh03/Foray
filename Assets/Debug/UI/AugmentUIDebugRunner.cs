using UnityEngine;

public class AugmentUIDebugRunner : MonoBehaviour
{
    [SerializeField] private RunController _runController;

#if UNITY_EDITOR
    [ContextMenu("Enter Augment Replacement")]
    public void EnterAugmentReplacement()
    {
        if (_runController == null)
        {
            Debug.LogError(
                "RunController is not assigned to AugmentUIDebugRunner."
            );
            return;
        }

        _runController.DebugEnterAugmentReplacement();
    }
#endif
}