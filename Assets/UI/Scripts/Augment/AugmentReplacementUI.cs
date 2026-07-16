using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AugmentReplacementUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _pendingNameText;
    [SerializeField] private TMP_Text _pendingRarityText;
    [SerializeField] private TMP_Text _pendingDescriptionText;

    [SerializeField] private Button _skipButton;
    [SerializeField] private RunController _runController;

    public void Refresh()
    {
        RunManager runManager = _runController.RunManager;
        Augment pendingAugment = runManager.PendingAugment;

        if (pendingAugment == null)
        {
            return;
        }

        _pendingNameText.text = pendingAugment.Name;
        _pendingRarityText.text = pendingAugment.Rarity.ToString();
        _pendingDescriptionText.text = pendingAugment.Description;

        _skipButton.onClick.RemoveAllListeners();
        _skipButton.onClick.AddListener(OnSkipButtonClicked);
    }

    private void OnSkipButtonClicked()
    {
        _runController.OnAugmentReplacementSkipped();
    }
}