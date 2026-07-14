using TMPro;
using UnityEngine;

public class RunStatusUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _stageText;
    [SerializeField] private TMP_Text _roundText;
    [SerializeField] private TMP_Text _goldText;

    public void Refresh(RunManager runManager)
    {
        if (runManager == null)
        {
            return;
        }

        _stageText.text = $"Stage {runManager.StageIndex}";
        _roundText.text = $"Round {runManager.RoundIndex}";
        _goldText.text = $"Gold {runManager.Gold}";
    }
}