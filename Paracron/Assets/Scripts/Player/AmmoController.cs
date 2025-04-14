using UnityEngine;
using TMPro;

public class AmmoController : MonoBehaviour
{
    [SerializeField] TMP_Text ammoText;

    OwnedWeapon activeWeapon;
    int currentAmmo = 0;

    public int CurrentAmmo => currentAmmo;
    void Awake()
    {
        activeWeapon = GetComponent<OwnedWeapon>();
    }
    void Start()
    {

    }

    public void AdjustAmmo(int amount)
    {
        currentAmmo = Mathf.Min(currentAmmo + amount, activeWeapon.CurrentWeaponSO.MagazineSize);
        ammoText.text = currentAmmo.ToString("D2");
    }
}
