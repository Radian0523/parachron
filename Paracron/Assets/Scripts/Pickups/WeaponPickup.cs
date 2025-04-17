using UnityEngine;

public class WeaponPickup : Pickup
{
    [SerializeField] WeaponSO weaponSO;

    void Awake()
    {
    }

    protected override void OnPickUp(OwnedWeapon ownedWeapon)
    {
        ownedWeapon.Inventory.AddItem(weaponSO);
        ownedWeapon.OnPickupWeapon(weaponSO);
        // activeWeapon.SwitchWeapon(weaponSO);
    }


}
