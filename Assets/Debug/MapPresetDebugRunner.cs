using UnityEngine;

public class MapPresetDebugRunner : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("===== Map Preset Test Start =====");

        for (int stageIndex = 1; stageIndex <= 10; stageIndex++)
        {
            int mapLength = MapPreset.GetMapLengthForStage(stageIndex);
            MapPreset mapPreset = MapPreset.CreateSquare(mapLength);

            Debug.Log(
                $"Stage {stageIndex} | " +
                $"Map: {mapPreset.Width}x{mapPreset.Height} | " +
                $"PlayerDeploySlots: {mapPreset.PlayerDeployPositions.Count} | " +
                $"EnemyDeploySlots: {mapPreset.EnemyDeployPositions.Count} | " +
                $"BlockedPositions: {mapPreset.BlockedPositions.Count}"
            );
        }

        Debug.Log("===== Map Preset Test End =====");
    }
}