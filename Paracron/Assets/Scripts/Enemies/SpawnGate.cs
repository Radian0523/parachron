using System.Collections;
using UnityEngine;

public class SpawnGate : MonoBehaviour
{
    [SerializeField] GameObject robotPrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float spwanTime = 5f;

    public bool canSpawn = true;

    PlayerHealth player;
    void Start()
    {
        player = FindFirstObjectByType<PlayerHealth>();
        StartCoroutine(SpawnRobotRoutine());
    }

    IEnumerator SpawnRobotRoutine()
    {
        while (player)
        {
            yield return new WaitForSeconds(spwanTime);
            if (canSpawn) Instantiate(robotPrefab, spawnPoint.position, transform.rotation);
        }
    }
}
