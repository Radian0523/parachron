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


    bool isZoomingIn = false;

    float timeSinceLastShot = 0f;
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
    }

    void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        firstPersonController = GetComponentInParent<FirstPersonController>();
        ownedWeapon = GetComponent<OwnedWeapon>();
        inventory = GetComponent<Inventory>();
        animator = GetComponent<Animator>();

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
                if (!starterAssetsInputs.shoot || inventory.ReserveAmmo <= 0) return;
                currentWeapon.Shoot(currentWeaponSO);
                inventory.AdjustMagazineAmmo(currentWeaponSO.inventoryIndex, -1);
                animator.Play(SHOOT_STRING, 0, 0f);
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

    public void OnSwitchWeapon(int weaponInventoryIndex)
    {
        Weapon newWeapon = ownedWeapon.weaponType(weaponInventoryIndex).gameObject.GetComponent<Weapon>();
        WeaponSO newWeaponSO = ownedWeapon.weaponType(weaponInventoryIndex).weaponSO;
        currentWeapon = newWeapon;
        currentWeaponSO = newWeaponSO;
        ZoomOut();
    }
}
