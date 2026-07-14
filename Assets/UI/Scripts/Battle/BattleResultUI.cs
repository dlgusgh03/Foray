using TMPro;
using UnityEngine;

public class BattleResultUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _resultText;

    private void Awake()
    {
        Hide();
    }

    public void ShowVictory()
    {
        gameObject.SetActive(true);
        _resultText.text = "Victory";
    }

    public void ShowDefeat()
    {
        gameObject.SetActive(true);
        _resultText.text = "Defeat";
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}