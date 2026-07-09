public static class EnemyScaling
{
    public static int GetTargetPopulationCost(int stageIndex, int roundIndex)
    {
        int battleIndex = (stageIndex - 1) * 3 + (roundIndex - 1);
        int targetPopulationCost = 2 * (3 + (battleIndex + 1) / 2 + (battleIndex * battleIndex) / 70);

        return targetPopulationCost;
    }
}