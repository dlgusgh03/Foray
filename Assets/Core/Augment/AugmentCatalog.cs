using System.Collections.Generic;

public static class AugmentCatalog
{
    public static List<Augment> GetAllAugments()
    {
        List<Augment> augments = new List<Augment>();

        augments.Add(new Augment(
            0,
            "Empty", // 빈 증강
            "This augment is empty...",
            AugmentRarity.Common,
            null
        ));

        augments.Add(new Augment(
            1,
            "Scarecrow King", // 허수아비 왕
            "Prevents the king's death by sacrificing another allied piece.",
            AugmentRarity.Legendary,
            null
        ));

        augments.Add(new Augment(
            2,
            "Undead Lord", // 언데드 군주
            "The king temporarily revives enemies it captures.",
            AugmentRarity.Legendary,
            null
        ));

        augments.Add(new Augment(
            3,
            "Continuous Fire", // 연속 사격
            "The cannon gains an additional action after capturing an enemy.",
            AugmentRarity.Epic,
            null
        ));

        augments.Add(new Augment(
            4,
            "Flawless Victory", // 무결점 승리
            "Gain bonus gold after winning without losing an allied piece.",
            AugmentRarity.Common,
            null
        ));

        augments.Add(new Augment(
            5,
            "Plunder", // 약탈
            "Gain gold when a horse captures an enemy piece.",
            AugmentRarity.Epic,
            null
        ));

        augments.Add(new Augment(
            6,
            "Composure", // 평정심
            "Ignore special debuffs during boss battles.",
            AugmentRarity.Legendary,
            null
        ));

        augments.Add(new Augment(
            7,
            "BOOOOM!!!", // 폭!!!발!!!
            "Destroy enemies adjacent to a cannon after it moves.",
            AugmentRarity.Legendary,
            null
        ));

        augments.Add(new Augment(
            8,
            "Insurance Claim", // 보험 처리
            "Gain gold when a chariot is captured by an enemy.",
            AugmentRarity.Rare,
            null
        ));

        return augments;
    }
}