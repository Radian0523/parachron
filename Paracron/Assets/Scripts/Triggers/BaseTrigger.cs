using UnityEngine;

public abstract class BaseTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerEnter(other);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerExit(other);
        }
    }

    protected abstract void OnPlayerEnter(Collider other);

    protected abstract void OnPlayerExit(Collider other);

}
