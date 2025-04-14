using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] float speed = 30f;
    [SerializeField] GameObject projectileHitVFX;
    int damageToPlayer;

    // const string PLAYER_STRING = "Player";
    Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        rb.linearVelocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        // rb.linearVelocity;
    }

    public void Init(int damage)
    {
        this.damageToPlayer = damage;
    }

    void OnTriggerEnter(Collider other)
    {

        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        playerHealth?.TakeDamage(damageToPlayer);
        Debug.Log(playerHealth);
        Instantiate(projectileHitVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
