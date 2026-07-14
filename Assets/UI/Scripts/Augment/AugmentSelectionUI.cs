using System.Collections.Generic;
using UnityEngine;

public class AugmentSelectionUI : MonoBehaviour
{
    [SerializeField] private AugmentChoiceUI[] _choiceUIs;
    [SerializeField] private RunController _runController;

    public void Refresh()
    {
        List<Augment> choices = _runController.RunManager.GetAugmentChoices();

        for (int i = 0; i < _choiceUIs.Length; i++)
        {
            Augment augment = i < choices.Count ? choices[i] : null;

            _choiceUIs[i].Setup(augment, i, OnAugmentSelected);
        }
    }

    private void OnAugmentSelected(int choiceIndex)
    {
        _runController.OnAugmentSelected(choiceIndex);
    }


}
