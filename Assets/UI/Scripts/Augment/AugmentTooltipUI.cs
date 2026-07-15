using TMPro;
using UnityEngine;

public class AugmentTooltipUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _rarityText;
    [SerializeField] private TMP_Text _descriptionText;

    public void Show(Augment augment)
    {
        _nameText.text = augment.Name;
        _rarityText.text = augment.Rarity.ToString();
        _descriptionText.text = augment.Description;

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}