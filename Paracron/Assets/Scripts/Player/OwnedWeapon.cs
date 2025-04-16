using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class WeaponType
{
    public GameObject gameObject;
    public WeaponSO weaponSO;
    public bool alreadyWeaponCreated = false;
}

public class OwnedWeapon : MonoBehaviour
{
    [SerializeField] InventoryUIPresenter inventoryUIPresenter;
    [SerializeField] Transform equippedWeaponPos;
    [SerializeField] Transform nonEquippedWeaponPos;
    [SerializeField] float changeWeaponTime = 0.2f;
    [SerializeField] float unusedWeaponDistance = 0.2f;

    Inventory inventory;
    EquippedWeapon equippedWeapon;
    StarterAssetsInputs starterAssetsInputs;

    // Weapon currentWeapon;
    Dictionary<int, WeaponType> ownedWeapons = new();

    public Inventory Inventory => inventory;

    public WeaponType weaponType(int weaponInventoryIndex) => ownedWeapons.ContainsKey(weaponInventoryIndex) ? ownedWeapons[weaponInventoryIndex] : null;



    void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        // 親の中から、StarterAssetsInputsをコンポーネントに持つオブジェクトを一つ取ってきて入れる
        inventory = GetComponent<Inventory>();
        equippedWeapon = GetComponent<EquippedWeapon>();
    }

    void Start()
    {
        // Dix の初期化
        for (int i = 0; i < inventoryUIPresenter.InventoryCount; i++)
        {
            ownedWeapons[i] = new();
            ownedWeapons[i].gameObject = Instantiate(equippedWeapon.EmptyWeaponSOs[i].weaponPrefab, this.transform);
            ownedWeapons[i].weaponSO = equippedWeapon.EmptyWeaponSOs[i];
            ownedWeapons[i].alreadyWeaponCreated = false;
        }
        // inventory.AddItem(equippedWeapon.CurrentWeaponSO);
        // SwitchWeapon(startingWeapon);
    }
    void Update()
    {
        HandleSelectInput();
    }

    private void HandleSelectInput()
    {

        if (starterAssetsInputs.select.y == 0)
        {
            return;
        }
        else if (starterAssetsInputs.select.y > 0)
        {
            int nextWeaponIndex = equippedWeapon.CurrentWeaponSO.inventoryIndex - 1;
            if (nextWeaponIndex == -1) nextWeaponIndex = inventoryUIPresenter.InventoryCount - 1;
            SwitchWeapon(nextWeaponIndex);
            inventoryUIPresenter.ChangeSelectedCursor(nextWeaponIndex);
        }
        else
        {
            int nextWeaponIndex = equippedWeapon.CurrentWeaponSO.inventoryIndex + 1;
            if (nextWeaponIndex == inventoryUIPresenter.InventoryCount) nextWeaponIndex = 0;
            SwitchWeapon(nextWeaponIndex);
            inventoryUIPresenter.ChangeSelectedCursor(nextWeaponIndex);

        }

    }



    public void OnGetWeapon(WeaponSO weaponSO)
    {
        if (ownedWeapons[weaponSO.inventoryIndex].alreadyWeaponCreated == true)
        {
            // もしすでに武器を持っていた場合は、そのマガジンサイズ分のAmmoを、ReserveAmmoに加算
            inventory.AdjustReserveAmmo(weaponSO.MagazineSize);
            return;
        }
        ownedWeapons[weaponSO.inventoryIndex].weaponSO = weaponSO;
        Destroy(ownedWeapons[weaponSO.inventoryIndex].gameObject); // Empty Weapon を破壊
        ownedWeapons[weaponSO.inventoryIndex].gameObject = Instantiate(weaponSO.weaponPrefab, /* nonEquippedWeaponPos.transform.position, nonEquippedWeaponPos.transform.rotation, */ this.transform);
        ownedWeapons[weaponSO.inventoryIndex].alreadyWeaponCreated = true;
        GameObject weaponGameObj = ownedWeapons[weaponSO.inventoryIndex].gameObject;
        Vector3 weaponStartingPos = new Vector3(weaponGameObj.transform.localPosition.x, weaponGameObj.transform.localPosition.y - 0.35f, weaponGameObj.transform.localPosition.z);
        ownedWeapons[weaponSO.inventoryIndex].gameObject.transform.localPosition = weaponStartingPos;
        inventoryUIPresenter.ChangeSelectedCursor(weaponSO.inventoryIndex);
        SwitchWeapon(weaponSO.inventoryIndex);
        inventory.AdjustMagazineAmmo(weaponSO.inventoryIndex, weaponSO.MagazineSize);
        Weapon newWeapon = ownedWeapons[weaponSO.inventoryIndex].gameObject.GetComponent<Weapon>();
    }

    public void SwitchWeapon(int newWeaponInventoryIndex)
    {
        WeaponSO newWeaponSO = ownedWeapons[newWeaponInventoryIndex].weaponSO;
        StopAllCoroutines();
        if (equippedWeapon.CurrentWeapon)
            StartCoroutine(ChangeWeaponRoutine(equippedWeapon.CurrentWeaponSO.inventoryIndex, false));
        StartCoroutine(ChangeWeaponRoutine(newWeaponSO.inventoryIndex, true));

        inventoryUIPresenter.OnSwitchWeapon(newWeaponInventoryIndex);
        equippedWeapon.OnSwitchWeapon(newWeaponInventoryIndex);
    }



    IEnumerator ChangeWeaponRoutine(int weaponInventoryIndex, bool equip)
    {
        Vector3 curWeaponLocalPos = ownedWeapons[weaponInventoryIndex].gameObject.transform.localPosition;
        float startPosY = curWeaponLocalPos.y;
        float targetPosY = equip ? ownedWeapons[weaponInventoryIndex].weaponSO.weaponPrefab.transform.localPosition.y : ownedWeapons[weaponInventoryIndex].weaponSO.weaponPrefab.transform.localPosition.y - unusedWeaponDistance;
        float elapsedTime = 0;

        while (elapsedTime < changeWeaponTime)
        {
            float t = elapsedTime / changeWeaponTime;
            elapsedTime += Time.deltaTime;
            curWeaponLocalPos.y = Mathf.Lerp(startPosY, targetPosY, t);
            ownedWeapons[weaponInventoryIndex].gameObject.transform.localPosition = curWeaponLocalPos;
            yield return null;
        }
        ownedWeapons[weaponInventoryIndex].gameObject.transform.localPosition = new Vector3(curWeaponLocalPos.x, targetPosY, curWeaponLocalPos.z);
    }
}
