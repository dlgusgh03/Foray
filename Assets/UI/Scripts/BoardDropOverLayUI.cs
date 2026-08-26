using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoardDropOverlayUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image _background;
    [SerializeField] private TMP_Text _instructionText;

    [Header("Colors")]
    [SerializeField] private Color _validColor = new Color(1f, 1f, 1f, 0.35f);

    [SerializeField] private Color _invalidColor = new Color(0.35f, 0.35f, 0.35f, 0.55f);

    private void Awake()
    {
        Hide();
    }

    public void ShowValid(string instruction)
    {
        Show(instruction, _validColor);
    }

    public void ShowInvalid(string instruction)
    {
        Show(instruction, _invalidColor);
    }

    public void Hide()
    {
        _instructionText.text = string.Empty;
        gameObject.SetActive(false);
    }

    private void Show(string instruction, Color backgroundColor)
    {
        _background.color = backgroundColor;
        _instructionText.text = instruction;
        gameObject.SetActive(true);
    }

#if UNITY_EDITOR
    [ContextMenu("Debug Show Valid")]
    private void DebugShowValid()
    {
        ShowValid("이곳에 배치");
    }

    [ContextMenu("Debug Show Invalid")]
    private void DebugShowInvalid()
    {
        ShowInvalid("구매 불가");
    }

    [ContextMenu("Debug Hide")]
    private void DebugHide()
    {
        Hide();
    }
#endif
}