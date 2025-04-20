using Cinemachine;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] ParticleSystem muzzuleFlash;
    [SerializeField] LayerMask interactionLayers;

    CinemachineImpulseSource impulseSource;
    GameManager gameManager;

    const int TiRewInventoryIndex = 5;

    void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    public void Shoot(WeaponSO weaponSO)
    {
        muzzuleFlash.Play();
        impulseSource.GenerateImpulse();

        RaycastHit hit;
        // 最後の引数は、TriggerEnterのついたオブジェクトには、あたらないということ。その前の引数には、必ずLayerMask型のものが必要で、今回の場合、Everythingにしている
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, interactionLayers, QueryTriggerInteraction.Ignore))
        {
            Instantiate(weaponSO.HitVFXPrefab, hit.point, Quaternion.identity);

            // もし、EnemyHealthがあれば、ダメージを与える
            // GetComponentInParentは、自分自身、親の順番に走査する。TurretのColliderを、Modelの中に付けているためInParentにしている
            EnemyHealth enemyHealth = hit.collider.GetComponentInParent<EnemyHealth>();
            if (enemyHealth)
            {
                enemyHealth.TakeDamage(weaponSO.Damage);
                gameManager.StartHitStopCoroutine(weaponSO.hitStopDuration);
            }

            if (weaponSO.inventoryIndex == TiRewInventoryIndex)
            {
                TimeRewindableObject timeRewindableObject = hit.collider.GetComponentInParent<TimeRewindableObject>();
                if (timeRewindableObject)
                {
                    timeRewindableObject.StartRewind();
                }
            }


        }
    }
}
