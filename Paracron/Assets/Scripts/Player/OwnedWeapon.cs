using Cinemachine;
using StarterAssets;
using UnityEngine;

public class OwnedWeapon : MonoBehaviour
{
    public enum WeaponState
    {
        Idle,
        Firing,
    }
    [SerializeField] WeaponSO startingWeapon;
    [SerializeField] CinemachineVirtualCamera cinemachineCamera;
    [SerializeField] Camera overlayWeaponCamera;
    [SerializeField] GameObject zoomVignette;

    WeaponSO currentWeaponSO;
    AmmoController ammoController;
    Inventory inventory;
    FirstPersonController firstPersonController;
    Animator animator;
    StarterAssetsInputs starterAssetsInputs;
    WeaponState currentState;
    float timeSinceLastShot = 0f;

    Weapon currentWeapon;

    bool isZoomingIn = false;
    float defaultFOV;
    float defaultRotationSpeed;

    const string SHOOT_STRING = "Shoot";

    public WeaponSO CurrentWeaponSO => currentWeaponSO;
    public AmmoController AmmmController => ammoController;
    public Inventory Inventory => inventory;



    void Awake()
    {
        // 親の中から、StarterAssetsInputsをコンポーネントに持つオブジェクトを一つ取ってきて入れる
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        firstPersonController = GetComponentInParent<FirstPersonController>();
        animator = GetComponent<Animator>();
        ammoController = GetComponent<AmmoController>();
        inventory = GetComponent<Inventory>();

        defaultFOV = cinemachineCamera.m_Lens.FieldOfView;
        currentState = WeaponState.Idle;
        defaultRotationSpeed = firstPersonController.RotationSpeed;
    }

    void Start()
    {
        inventory.AddItem(currentWeaponSO);
        // SwitchWeapon(startingWeapon);
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
                if (!starterAssetsInputs.shoot || ammoController.CurrentAmmo <= 0) return;
                currentWeapon.Shoot(currentWeaponSO);
                ammoController.AdjustAmmo(-1);
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

    public void OnGetWeapon(WeaponSO weaponSO)
    {

    }

    public void SwitchWeapon(WeaponSO weaponSO)
    {
        Debug.Log(weaponSO.name);
        if (currentWeapon)
        {
            Destroy(currentWeapon.gameObject);
        }

        Weapon newWeapon = Instantiate(weaponSO.weaponPrefab, this.gameObject.transform).GetComponent<Weapon>();
        currentWeapon = newWeapon;
        this.currentWeaponSO = weaponSO;
        ammoController.AdjustAmmo(currentWeaponSO.MagazineSize);
        ZoomOut();
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
        Debug.Log("Zooming in");
        isZoomingIn = true;
        starterAssetsInputs.ZoomInput(false);
        cinemachineCamera.m_Lens.FieldOfView = currentWeaponSO.ZoomAmount;
        overlayWeaponCamera.fieldOfView = currentWeaponSO.ZoomAmount;
        zoomVignette.SetActive(true);
        firstPersonController.ChangeRotationSpeed(currentWeaponSO.ZoomRotationSpeed);
    }

    private void ZoomOut()
    {
        Debug.Log("Not zooming in");
        isZoomingIn = false;
        starterAssetsInputs.ZoomInput(false);
        cinemachineCamera.m_Lens.FieldOfView = defaultFOV;
        overlayWeaponCamera.fieldOfView = defaultFOV;
        zoomVignette.SetActive(false);
        firstPersonController.ChangeRotationSpeed(defaultRotationSpeed);
    }
}
