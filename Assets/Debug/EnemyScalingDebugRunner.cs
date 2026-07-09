using UnityEngine;

public class EnemyScalingDebugRunner : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("===== Enemy Scaling Test Start =====");

        for (int stageIndex = 1; stageIndex <= 10; stageIndex++)
        {
            Debug.Log($"----- Stage {stageIndex} -----");

            for (int roundIndex = 1; roundIndex <= 3; roundIndex++)
            {
                int targetPopulationCost =
                    EnemyScaling.GetTargetPopulationCost(stageIndex, roundIndex);

                float designPopulation = targetPopulationCost / 2f;

                Debug.Log(
                    $"{stageIndex}-{roundIndex} | " +
                    $"TargetPopulationCost: {targetPopulationCost} | " +
                    $"Population: {designPopulation}"
                );
            }
        }

        Debug.Log("===== Enemy Scaling Test End =====");
    }
}