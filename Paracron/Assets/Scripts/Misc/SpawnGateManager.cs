using UnityEngine;

public static class SpawnGateManager
{
    public static void SetCanSpawn(SpawnGate[] spawnGates, bool newSpawnState)
    {
        foreach (SpawnGate spawnGate in spawnGates)
        {
            spawnGate.canSpawn = newSpawnState;
        }
    }
}