using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] Transform turretHead;
    [SerializeField] Transform playerTargetPoint;
    [SerializeField] Transform projectileSpawnPoint;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float fireRate = 2f;
    [SerializeField] int damageToPlayer = 2;

    PlayerHealth player;
    void Start()
    {
        player = FindFirstObjectByType<PlayerHealth>();
        StartCoroutine(SpawnProjectileRoutine());
    }
    void Update()
    {
        turretHead.LookAt(playerTargetPoint);
    }

    IEnumerator SpawnProjectileRoutine()
    {
        while (player)
        {
            yield return new WaitForSeconds(fireRate);
            // Instantiateと同時に、その生み出すもののスクリプトを得るという定石
            Projectile newProjectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity).GetComponent<Projectile>(); // 最後の引数はなんで？
            newProjectile.transform.LookAt(playerTargetPoint);
            newProjectile.Init(damageToPlayer);
        }
    }
}
