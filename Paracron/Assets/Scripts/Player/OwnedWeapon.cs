using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnedWeapon : MonoBehaviour
{
    [SerializeField] Transform equippedWeaponPos;
    [SerializeField] Transform nonEquippedWeaponPos;
    [SerializeField] float changeWeaponTime = 0.2f;

    Inventory inventory;
    EquippedWeapon equippedWeapon;

    // Weapon currentWeapon;
    Dictionary<int, GameObject> ownedWeapons = new();





    public Inventory Inventory => inventory;



    void Awake()
    {
        // 親の中から、StarterAssetsInputsをコンポーネントに持つオブジェクトを一つ取ってきて入れる
        inventory = GetComponent<Inventory>();
        equippedWeapon = GetComponent<EquippedWeapon>();

    }

    void Start()
    {
        // inventory.AddItem(equippedWeapon.CurrentWeaponSO);
        // SwitchWeapon(startingWeapon);
    }
    void Update()
    {
    }



    public void OnGetWeapon(WeaponSO weaponSO)
    {
        if (ownedWeapons.ContainsKey(weaponSO.inventoryIndex))
        {
            // もしすでに武器を持っていた場合は、そのマガジンサイズ分のAmmoを、ReserveAmmoに加算
            inventory.AdjustReserveAmmo(weaponSO.MagazineSize);
            return;
        }
        ownedWeapons[weaponSO.inventoryIndex] = Instantiate(weaponSO.weaponPrefab, nonEquippedWeaponPos.transform);
        Weapon newWeapon = ownedWeapons[weaponSO.inventoryIndex].GetComponent<Weapon>();
    }

    // Weapon と WeaponSOは繋がってるんだ、Dictionaryとか構造体、配列とかにした方がいいかも。
    public void SwitchWeapon(Weapon newWeapon, WeaponSO newWeaponSO)
    {
        StartCoroutine(ChangeWeaponRoutine(equippedWeapon.CurrentWeaponSO.inventoryIndex, false));
        StartCoroutine(ChangeWeaponRoutine(newWeaponSO.inventoryIndex, true));
        equippedWeapon.OnSwitchWeapon(newWeapon);
    }



    IEnumerator ChangeWeaponRoutine(int weaponInventoryIndex, bool equip)
    {
        Vector3 startPos = equip ? nonEquippedWeaponPos.position : equippedWeaponPos.position;
        Vector3 targetPos = equip ? equippedWeaponPos.position : nonEquippedWeaponPos.position;
        float elapsedTime = 0;

        while (elapsedTime < changeWeaponTime)
        {
            float t = elapsedTime / changeWeaponTime;
            elapsedTime += Time.deltaTime;
            ownedWeapons[weaponInventoryIndex].transform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }

        ownedWeapons[weaponInventoryIndex].transform.position = targetPos;
    }
}
