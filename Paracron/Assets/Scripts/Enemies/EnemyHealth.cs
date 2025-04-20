using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] GameObject robotExplosionVFX;
    [SerializeField] AudioClip robotExplosionSE;
    [SerializeField] float robotExplosionSEScale = 1;
    [SerializeField] int startingHealth = 3;
    int currentHealth;

    GameManager gameManager;
    AudioSource audioSource;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        currentHealth = startingHealth;
    }
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        gameManager.AdjustEnemiesLeft(1);
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            SelfDestruct();
        }
    }

    public void SelfDestruct()
    {
        Debug.Log(robotExplosionSEScale);
        Debug.Log(robotExplosionSE);
        gameManager.AdjustEnemiesLeft(-1);

        audioSource.PlayOneShot(robotExplosionSE, robotExplosionSEScale);
        Instantiate(robotExplosionVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
