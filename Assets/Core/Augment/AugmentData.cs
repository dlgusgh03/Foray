using UnityEngine;

[CreateAssetMenu(fileName = "AugmentData", menuName = "Foray/Augment Data")]
public class AugmentData : ScriptableObject
{
    [SerializeField] private int _id;
    [SerializeField] private string _displayName;

    [TextArea][SerializeField] private string _description;

    [SerializeField] private AugmentRarity _rarity;
    [SerializeField] private Sprite _icon;

    public int Id => _id;
    public string DisplayName => _displayName;
    public string Description => _description;
    public AugmentRarity Rarity => _rarity;
    public Sprite Icon => _icon;

    public Augment CreateAugment()
    {
        return new Augment(_id, _displayName, _description, _rarity, null, _icon);
    }
}