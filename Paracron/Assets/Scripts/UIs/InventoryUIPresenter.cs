using UnityEngine;

public class InventoryUIPresenter : MonoBehaviour
{
    [SerializeField] Transform[] inventorySlots;
    [SerializeField] GameObject[] weaponRawImagesPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnGetWeapon(WeaponSO weaponSO)
    {
        GameObject.Instantiate(weaponRawImagesPrefab[weaponSO.inventoryIndex], inventorySlots[weaponSO.inventoryIndex]);
    }
}
