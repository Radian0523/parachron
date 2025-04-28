using UnityEngine;

public class SpawnGateTrigger : BaseTrigger
{
    [SerializeField] SpawnGate[] spawnGates;


    void Start()
    {
        foreach (SpawnGate spawnGate in spawnGates)
        {
            spawnGate.canSpawn = false;
        }
    }

    protected override void OnPlayerEnter(Collider other)
    {
        foreach (SpawnGate spawnGate in spawnGates)
        {
            spawnGate.canSpawn = true;
        }
    }

    protected override void OnPlayerExit(Collider other)
    {
    }
}
