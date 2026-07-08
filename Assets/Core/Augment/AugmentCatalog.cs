using System.Collections.Generic;

public static class AugmentCatalog
{
    public static List<Augment> GetAllAugments()
    {
        List<Augment> augments = new List<Augment>();

        // 빈 증강
        augments.Add(new Augment(
            0,
            "Empty",
            "This augment is empty...",
            AugmentRarity.Common,
            null
        ));

        // 허수아비 왕
        augments.Add(new Augment(
            1,
            "Scarecrow King",
            "Prevents the king's death by sacrificing another allied piece.",
            AugmentRarity.Legendary,
            null
        ));

        // 언데드 군주
        augments.Add(new Augment(
            2,
            "Undead Lord",
            "The king temporarily revives enemies it captures.",
            AugmentRarity.Legendary,
            null
        ));

        // 연속 사격
        augments.Add(new Augment(
            3,
            "Continuous Fire",
            "The cannon gains an additional action after capturing an enemy.",
            AugmentRarity.Epic,
            null
        ));

        // 무결점 승리
        augments.Add(new Augment(
            4,
            "Flawless Victory",
            "Gain bonus gold after winning without losing an allied piece.",
            AugmentRarity.Common,
            null
        ));

        // 약탈
        augments.Add(new Augment(
            5,
            "Plunder",
            "Gain gold when a horse captures an enemy piece.",
            AugmentRarity.Epic,
            null
        ));

        // 평정심
        augments.Add(new Augment(
            6,
            "Composure",
            "Ignore special debuffs during boss battles.",
            AugmentRarity.Legendary,
            null
        ));

        // 폭발
        augments.Add(new Augment(
            7,
            "BOOOOM!!!",
            "Destroy enemies adjacent to a cannon after it moves.",
            AugmentRarity.Legendary,
            null
        ));

        // 보험 처리
        augments.Add(new Augment(
            8,
            "Insurance Claim",
            "Gain gold when a chariot is captured by an enemy.",
            AugmentRarity.Rare,
            null
        ));

        return augments;
    }
}