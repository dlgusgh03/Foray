using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AugmentChoiceUI : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private Sprite _defaultIcon;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _rarityText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private Button _selectButton;

    private int _choiceIndex;
    private Action<int> _onSelected;

    public void Setup(Augment augment, int choiceIndex, Action<int> onSelected)
    {
        if (augment == null)
        {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);

        _choiceIndex = choiceIndex;
        _onSelected = onSelected;

        _iconImage.sprite = augment.Icon != null ? augment.Icon : _defaultIcon;
        _nameText.text = augment.Name;
        _rarityText.text = augment.Rarity.ToString();
        _descriptionText.text = augment.Description;

        _selectButton.onClick.RemoveAllListeners();
        _selectButton.onClick.AddListener(OnSelectButtonClicked);
    }

    private void OnSelectButtonClicked()
    {
        _onSelected?.Invoke(_choiceIndex);
    }
}
