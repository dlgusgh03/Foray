using UnityEngine;

public class RunController : MonoBehaviour
{
    private RunManager _runManager;

    public RunManager RunManager => _runManager;

    private void Awake()
    {
        _runManager = new RunManager();
        _runManager.StartRun();
    }
}