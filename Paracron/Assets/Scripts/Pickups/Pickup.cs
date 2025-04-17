using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 100f;
    const string PLAYER_STRING = "Player";

    protected abstract void OnPickUp(OwnedWeapon activeWeapon);

    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(PLAYER_STRING))
        {
            OwnedWeapon ownedWeapon = other.gameObject.GetComponentInChildren<OwnedWeapon>();
            OnPickUp(ownedWeapon);
            Destroy(this.gameObject);
        }
    }

}
