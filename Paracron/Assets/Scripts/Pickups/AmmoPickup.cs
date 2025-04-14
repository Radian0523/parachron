using UnityEngine;

public class AmmoPickup : Pickup
{
    [SerializeField] int ammoAmount = 100;
    protected override void OnPickUp(OwnedWeapon activeWeapon)
    {
        activeWeapon.AmmmController.AdjustAmmo(ammoAmount);
    }
}
