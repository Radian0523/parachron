using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class OwnedWeaponData
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

    StarterAssetsInputs starterAssetsInputs;
    Inventory inventory;
    EquippedWeapon equippedWeapon;
    AudioSource audioSource;

    // Weapon currentWeapon;
    Dictionary<int, OwnedWeaponData> ownedWeapons = new();

    public Inventory Inventory => inventory;
    public OwnedWeaponData OwnedWeaponData(int weaponInventoryIndex) => ownedWeapons.ContainsKey(weaponInventoryIndex) ? ownedWeapons[weaponInventoryIndex] : null;


    void Awake()
    {
        // 親の中から、StarterAssetsInputsをコンポーネントに持つオブジェクトを一つ取ってきて入れる
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        inventory = GetComponent<Inventory>();
        equippedWeapon = GetComponent<EquippedWeapon>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        // Dix の初期化 (全インベントリにEmptyWeaponを持たせる)
        for (int i = 0; i < inventoryUIPresenter.InventoryCount; i++)
        {
            ownedWeapons[i] = new();
            ownedWeapons[i].gameObject = Instantiate(equippedWeapon.EmptyWeaponSOs[i].weaponPrefab, this.transform);
            ownedWeapons[i].weaponSO = equippedWeapon.EmptyWeaponSOs[i];
            ownedWeapons[i].alreadyWeaponCreated = false;
        }
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
        }
        else
        {
            int nextWeaponIndex = equippedWeapon.CurrentWeaponSO.inventoryIndex + 1;
            if (nextWeaponIndex == inventoryUIPresenter.InventoryCount) nextWeaponIndex = 0;
            SwitchWeapon(nextWeaponIndex);
        }

    }

    private void DeleteWeapon(WeaponSO weaponSO)
    {
        inventory.RemoveItem(weaponSO);
        Destroy(ownedWeapons[weaponSO.inventoryIndex].gameObject); // Empty Weapon を破壊
    }



    public void OnPickupWeapon(WeaponSO weaponSO)
    {
        if (ownedWeapons[weaponSO.inventoryIndex].alreadyWeaponCreated == true)
        {
            // もしすでに武器を持っていた場合は、そのマガジンサイズ分のAmmoを、ReserveAmmoに加算
            inventory.AdjustReserveAmmo(weaponSO.MagazineSize);
            return;
        }
        DeleteWeapon(weaponSO);
        CreateNewWeapon(weaponSO);
        // Weaponを、少し下の位置におく
        GameObject weaponGameObj = ownedWeapons[weaponSO.inventoryIndex].gameObject;
        Vector3 weaponStartingPos = new Vector3(weaponGameObj.transform.localPosition.x, weaponGameObj.transform.localPosition.y - unusedWeaponDistance, weaponGameObj.transform.localPosition.z);
        ownedWeapons[weaponSO.inventoryIndex].gameObject.transform.localPosition = weaponStartingPos;
        // 持ち替える
        SwitchWeapon(weaponSO.inventoryIndex);
        // 落ちてる武器にはすでにMagazineは満杯
        inventory.AdjustMagazineAmmo(weaponSO.inventoryIndex, weaponSO.MagazineSize);
    }

    private void CreateNewWeapon(WeaponSO weaponSO)
    {
        ownedWeapons[weaponSO.inventoryIndex].weaponSO = weaponSO;
        ownedWeapons[weaponSO.inventoryIndex].gameObject = Instantiate(weaponSO.weaponPrefab, this.transform);
        ownedWeapons[weaponSO.inventoryIndex].alreadyWeaponCreated = true;
    }

    public void SwitchWeapon(int newWeaponInventoryIndex)
    {
        WeaponSO newWeaponSO = ownedWeapons[newWeaponInventoryIndex].weaponSO;

        // StopAllCoroutines();
        if (equippedWeapon.CurrentWeapon)
            StartCoroutine(ChangeWeaponRoutine(equippedWeapon.CurrentWeaponSO.inventoryIndex, false));
        StartCoroutine(ChangeWeaponRoutine(newWeaponSO.inventoryIndex, true));

        inventoryUIPresenter.OnSwitchWeapon(newWeaponInventoryIndex);
        equippedWeapon.UpdateCurrentWeapon(newWeaponInventoryIndex);

        if (equippedWeapon.CurrentWeaponSO.PrepareSE)
            audioSource.PlayOneShot(equippedWeapon.CurrentWeaponSO.PrepareSE);
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
