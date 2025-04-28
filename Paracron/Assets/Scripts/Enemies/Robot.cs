using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    NavMeshAgent agent;
    // [SerializeField] Transform target;
    FirstPersonController player;
    EnemyHealth enemyHealth;

    const string PLAYER_STRING = "Player";

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();
    }
    void Start()
    {
        // シーン内で FirstPersonController コンポーネントを持つオブジェクトを検索し、その最初の一つをplayerに格納する
        player = FindFirstObjectByType<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player) return;
        if (agent.enabled == false) return;
        agent.SetDestination(player.transform.position);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_STRING))
        {
            enemyHealth.SelfDestruct();
        }
    }

    public void SetAgentEnabled(bool newAgentState)
    {
        agent.enabled = newAgentState;
    }

}
