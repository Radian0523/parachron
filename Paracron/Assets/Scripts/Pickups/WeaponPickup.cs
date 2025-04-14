using UnityEngine;

public class WeaponPickup : Pickup
{
    [SerializeField] WeaponSO weaponSO;

    void Awake()
    {
    }

    protected override void OnPickUp(OwnedWeapon activeWeapon)
    {
        activeWeapon.Inventory.AddItem(weaponSO);
        // activeWeapon.SwitchWeapon(weaponSO);
    }


}
