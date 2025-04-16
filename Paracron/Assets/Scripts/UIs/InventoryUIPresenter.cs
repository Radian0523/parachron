using UnityEngine;
using TMPro;

public class InventoryUIPresenter : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] Transform[] inventorySlots;
    [SerializeField] GameObject[] weaponRawImagesPrefab;
    [SerializeField] TMP_Text magazineAmmoText;
    [SerializeField] TMP_Text reserveAmmoText;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnAdjustMagazineAmmo(int weaponInventoryIndex)
    {
        UpdateMagazineAmmoText(weaponInventoryIndex);
    }


    public void OnAdjustReserveAmmo()
    {
        UpdateReserveAmmoText();
    }

    public void OnGetWeapon(WeaponSO weaponSO)
    {
        GameObject.Instantiate(weaponRawImagesPrefab[weaponSO.inventoryIndex], inventorySlots[weaponSO.inventoryIndex]);
    }

    private void UpdateReserveAmmoText()
    {
        reserveAmmoText.text = inventory.ReserveAmmo.ToString("D3");
    }

    private void UpdateMagazineAmmoText(int weaponInventoryIndex)
    {
        magazineAmmoText.text = inventory.MagazineAmmo[weaponInventoryIndex].ToString("D2");
    }

}
