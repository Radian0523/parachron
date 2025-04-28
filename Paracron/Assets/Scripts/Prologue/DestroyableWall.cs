using Unity.AI.Navigation;
using UnityEditor.Search;
using UnityEngine;

public class DestroyableWall : MonoBehaviour
{
    [SerializeField] EnemyHealth[] enemyHealth;
    [SerializeField] GameObject wallDestroyVFX;
    [Tooltip("壁が壊れた後にプレイヤーを追いかけるロボット")]
    [SerializeField] Robot[] robots;
    [SerializeField] SpawnGate[] spawnGates;
    // [SerializeField] NavMeshBaker baker;

    // bool hadDestroyedEnemy = false;

    // public bool HadDestroyedEnemy => hadDestroyedEnemy;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RobotManager.SetRobotAgentEnabled(robots, false);
        SpawnGateManager.SetCanSpawn(spawnGates, false);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < enemyHealth.Length; i++)
        {
            // if (!enemyHealth[i]) hadDestroyedEnemy = true;
            if (enemyHealth[i]) return;
        }

        Instantiate(wallDestroyVFX, transform.position, Quaternion.identity);
        RobotManager.SetRobotAgentEnabled(robots, true);
        SpawnGateManager.SetCanSpawn(spawnGates, true);
        Destroy(this.gameObject);
        // baker.Bake();
    }

}
