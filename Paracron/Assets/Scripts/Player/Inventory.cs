using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] InventoryUIPresenter inventoryUIPresenter;
    HashSet<string> items = new();

    public void AddItem(WeaponSO weaponSO)
    {
        items.Add(weaponSO.name);
        inventoryUIPresenter.OnGetWeapon(weaponSO);
    }

    public void RemoveItem(WeaponSO weaponSO)
    {
        items.Remove(weaponSO.name);
    }

    public bool HasItem(WeaponSO weaponSO)
    {
        return items.Contains(weaponSO.name);
    }
}
