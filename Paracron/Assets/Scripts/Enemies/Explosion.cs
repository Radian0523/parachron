using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float radius = 1.5f;
    [SerializeField] int damageToPlayer = 3;
    // [SerializeField] int damageToEnemy = 3;

    void Start()
    {
        Explode();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void Explode()
    {
        // 指定した中心と半径の範囲内にあるコライダーを取得します。3D空間で球形の領域を使って、衝突しているコライダーを探します。
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider hitCollider in hitColliders)
        {
            PlayerHealth playerHealth = hitCollider.GetComponent<PlayerHealth>();

            if (!playerHealth) continue;

            playerHealth.TakeDamage(damageToPlayer);

            break;
            // EnemyHealth enemyHealth = hitCollider.GetComponent<EnemyHealth>();

            // if (!playerHealth || !enemyHealth)

            // hitCollider.GetComponent<EnemyHealth>()?.TakeDamage(-damageToEnemy);

        }
    }
}
