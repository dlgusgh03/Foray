using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AugmentReplacementUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _pendingNameText;
    [SerializeField] private TMP_Text _pendingRarityText;
    [SerializeField] private TMP_Text _pendingDescriptionText;

    [SerializeField] private AugmentChoiceUI[] _ownedAugmentUIs;
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

        List<Augment> ownedAugments =
            runManager.GetCurrentAugments();

        for (int i = 0; i < _ownedAugmentUIs.Length; i++)
        {
            Augment augment =
                i < ownedAugments.Count
                    ? ownedAugments[i]
                    : null;

            _ownedAugmentUIs[i].Setup(
                augment,
                i,
                OnOwnedAugmentSelected
            );
        }

        _skipButton.onClick.RemoveAllListeners();
        _skipButton.onClick.AddListener(OnSkipButtonClicked);
    }

    private void OnOwnedAugmentSelected(int index)
    {
        _runController.OnAugmentReplaced(index);
    }

    private void OnSkipButtonClicked()
    {
        _runController.OnAugmentReplacementSkipped();
    }
}