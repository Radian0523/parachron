using UnityEngine;

public class InventoryUIPresenter : MonoBehaviour
{
    [SerializeField] Transform[] inventorySlots;
    [SerializeField] GameObject[] weaponRawImagesPrefab;
    
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
