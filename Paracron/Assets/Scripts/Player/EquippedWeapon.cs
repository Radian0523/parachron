using UnityEngine;
using StarterAssets;
using Cinemachine;

public class EquippedWeapon : MonoBehaviour
{
    [SerializeField] WeaponSO[] emptyWeaponSOs;
    [SerializeField] Camera overlayWeaponCamera;
    [SerializeField] CinemachineVirtualCamera cinemachineCamera;
    [SerializeField] GameObject zoomVignette;


    WeaponState currentState;
    StarterAssetsInputs starterAssetsInputs;
    FirstPersonController firstPersonController;
    Inventory inventory;
    Weapon currentWeapon;
    WeaponSO currentWeaponSO;
    Animator animator;
    OwnedWeapon ownedWeapon;
    AudioSource audioSource;


    bool isZoomingIn = false;

    float timeSinceLastShot = 0f;
    float timeSinceStartReload = 0f;
    float defaultFOV;
    float defaultRotationSpeed;

    const string SHOOT_STRING = "Shoot";

    public WeaponSO[] EmptyWeaponSOs => emptyWeaponSOs;
    public Weapon CurrentWeapon => currentWeapon;
    public WeaponSO CurrentWeaponSO => currentWeaponSO;


    public enum WeaponState
    {
        Idle,
        Firing,
        Reload,
    }

    void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        firstPersonController = GetComponentInParent<FirstPersonController>();
        ownedWeapon = GetComponent<OwnedWeapon>();
        inventory = GetComponent<Inventory>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        currentState = WeaponState.Idle;
        defaultFOV = cinemachineCamera.m_Lens.FieldOfView;
        defaultRotationSpeed = firstPersonController.RotationSpeed;
    }

    void Start()
    {
        currentWeaponSO = emptyWeaponSOs[0];
    }

    void Update()
    {
        HandleShoot();
        HandleZoom();
    }

    void HandleShoot()
    {
        switch (currentState)
        {
            case WeaponState.Idle:
                if (starterAssetsInputs.reload && inventory.MagazineAmmo(currentWeaponSO.inventoryIndex) < currentWeaponSO.MagazineSize && inventory.ReserveAmmo > 0)
                {
                    currentState = WeaponState.Reload;
                    audioSource.PlayOneShot(currentWeaponSO.ReloadSE, currentWeaponSO.ReloadSEScale);
                    timeSinceStartReload = 0;
                    break;
                }
                if (!starterAssetsInputs.shoot) return;
                if (inventory.MagazineAmmo(currentWeaponSO.inventoryIndex) <= 0)
                {
                    if (currentWeaponSO.DryFireSE)
                        audioSource.PlayOneShot(currentWeaponSO.DryFireSE);
                    starterAssetsInputs.ShootInput(false);
                    return;
                }
                currentWeapon.Shoot(currentWeaponSO);
                inventory.AdjustMagazineAmmo(currentWeaponSO.inventoryIndex, -1);
                animator.Play(SHOOT_STRING, 0, 0f);
                audioSource.PlayOneShot(currentWeaponSO.FireSE, currentWeaponSO.FireSEScale);
                currentState = WeaponState.Firing;
                break;
            case WeaponState.Firing:
                timeSinceLastShot += Time.deltaTime;
                if (timeSinceLastShot >= currentWeaponSO.FireRate)
                {
                    timeSinceLastShot = 0;
                    currentState = WeaponState.Idle;
                    if (currentWeaponSO.IsAutomatic) break;
                    starterAssetsInputs.ShootInput(false);
                }
                break;
            case WeaponState.Reload:
                timeSinceStartReload += Time.deltaTime;
                if (timeSinceStartReload >= currentWeaponSO.ReloadRate)
                {
                    inventory.ReloadAmmo(currentWeaponSO);
                    timeSinceStartReload = 0f;
                    currentState = WeaponState.Idle;
                    audioSource.Stop();
                    starterAssetsInputs.reload = false;
                }
                break;
        }
    }
    void HandleZoom()
    {
        if (!currentWeaponSO.CanZoom) return;

        if (starterAssetsInputs.zoom)
        {
            if (!isZoomingIn)
            {
                ZoomIn();
            }
            else
            {
                ZoomOut();
            }
        }
    }


    private void ZoomIn()
    {
        isZoomingIn = true;
        starterAssetsInputs.ZoomInput(false);
        cinemachineCamera.m_Lens.FieldOfView = currentWeaponSO.ZoomAmount;
        overlayWeaponCamera.fieldOfView = currentWeaponSO.ZoomAmount;
        zoomVignette.SetActive(true);
        firstPersonController.ChangeRotationSpeed(currentWeaponSO.ZoomRotationSpeed);
    }

    private void ZoomOut()
    {
        isZoomingIn = false;
        starterAssetsInputs.ZoomInput(false);
        cinemachineCamera.m_Lens.FieldOfView = defaultFOV;
        overlayWeaponCamera.fieldOfView = defaultFOV;
        zoomVignette.SetActive(false);
        firstPersonController.ChangeRotationSpeed(defaultRotationSpeed);
    }

    public void UpdateCurrentWeapon(int weaponInventoryIndex)
    {
        Weapon newWeapon = ownedWeapon.OwnedWeaponData(weaponInventoryIndex).gameObject.GetComponent<Weapon>();
        WeaponSO newWeaponSO = ownedWeapon.OwnedWeaponData(weaponInventoryIndex).weaponSO;
        currentWeapon = newWeapon;
        currentWeaponSO = newWeaponSO;
        ZoomOut();
    }
}
