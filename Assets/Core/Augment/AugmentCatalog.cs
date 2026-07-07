using System.Collections.Generic;

public static class AugmentCatalog
{
    public static List<Augment> GetAllAugments()
    {
        List<Augment> augments = new List<Augment>();

        augments.Add(new Augment(
            0,
            "Scarecrow King",
            "Prevents the king's death by sacrificing another allied piece.",
            AugmentRarity.Legendary,
            null
        ));

        augments.Add(new Augment(
            1,
            "Undead Lord",
            "The king temporarily revives enemies it captures.",
            AugmentRarity.Legendary,
            null
        ));

        augments.Add(new Augment(
            2,
            "Cannon Extra Action",
            "The cannon gains an additional action after capturing an enemy.",
            AugmentRarity.Rare,
            null
        ));

        augments.Add(new Augment(
            3,
            "Perfect Victory",
            "Gain bonus gold after winning without losing an allied piece.",
            AugmentRarity.Common,
            null
        ));

        return augments;
    }
}