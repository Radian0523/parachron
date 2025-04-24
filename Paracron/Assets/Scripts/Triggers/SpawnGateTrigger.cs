using UnityEngine;

public class SpawnGateTrigger : BaseTrigger
{
    [SerializeField] GameObject[] spawnGates;

    SpawnGate[] spawnGateScripts;

    void Start()
    {
        spawnGateScripts = new SpawnGate[spawnGates.Length];

        for (int i = 0; i < spawnGates.Length; i++)
        {
            spawnGateScripts[i] = spawnGates[i].GetComponent<SpawnGate>();
            spawnGateScripts[i].canSpawn = false; // 初期状態ではスポーンしないようにする
        }
    }

    protected override void OnPlayerEnter(Collider other)
    {
        foreach (SpawnGate spawnGateScript in spawnGateScripts)
        {
            spawnGateScript.canSpawn = true;
        }
    }

    protected override void OnPlayerExit(Collider other)
    {
    }
}
