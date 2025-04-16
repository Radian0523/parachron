using UnityEngine;

public class AmmoPickup : Pickup
{
    [SerializeField] int ammoAmount = 30;
    protected override void OnPickUp(OwnedWeapon activeWeapon)
    {
        activeWeapon.Inventory.AdjustReserveAmmo(ammoAmount);
    }
}
