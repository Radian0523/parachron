using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Objects/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public GameObject weaponPrefab;
    public int Damage = 1;
    public float FireRate = .5f;
    public float ReloadRate = 1f;
    public GameObject HitVFXPrefab;
    public bool IsAutomatic = false;
    public bool CanZoom = false;
    public float ZoomAmount = 10f;
    public float ZoomRotationSpeed = .3f;
    public int MagazineSize = 12;
    public float hitStopDuration = 0.05f;
    public int inventoryIndex = 0;
    public AudioClip FireSE;
    public float FireSEScale = 1;
    public AudioClip ReloadSE;
    public float ReloadSEScale = 1;
    public AudioClip PrepareSE;
    public AudioClip DryFireSE;
}
