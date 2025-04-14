using System.Collections;
using UnityEngine;

public class SpawnGate : MonoBehaviour
{
    [SerializeField] GameObject robotPrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float spwanTime = 5f;

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
            Instantiate(robotPrefab, spawnPoint.position, transform.rotation);
            yield return new WaitForSeconds(spwanTime);
        }

    }
}
