using Cinemachine;
using StarterAssets;
using TMPro;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
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
    [SerializeField] TMP_Text ammoText;

    WeaponSO currentWeaponSO;
    FirstPersonController firstPersonController;
    Animator animator;
    StarterAssetsInputs starterAssetsInputs;
    WeaponState currentState;
    float timeSinceLastShot = 0f;

    Weapon currentWeapon;

    bool isZoomingIn = false;
    float defaultFOV;
    float defaultRotationSpeed;
    int currentAmmo;

    const string SHOOT_STRING = "Shoot";



    void Awake()
    {
        // 親の中から、StarterAssetsInputsをコンポーネントに持つオブジェクトを一つ取ってきて入れる
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        firstPersonController = GetComponentInParent<FirstPersonController>();
        animator = GetComponent<Animator>();
        defaultFOV = cinemachineCamera.m_Lens.FieldOfView;
        currentState = WeaponState.Idle;
        defaultRotationSpeed = firstPersonController.RotationSpeed;
    }

    void Start()
    {
        SwitchWeapon(startingWeapon);
    }
    void Update()
    {
        HandleShoot();
        HandleZoom();
    }

    public void AdjustAmmo(int amount)
    {
        currentAmmo = Mathf.Min(currentAmmo + amount, currentWeaponSO.MagazineSize);
        ammoText.text = currentAmmo.ToString("D2");
    }

    void HandleShoot()
    {
        switch (currentState)
        {
            case WeaponState.Idle:
                if (!starterAssetsInputs.shoot || currentAmmo <= 0) return;
                currentWeapon.Shoot(currentWeaponSO);
                AdjustAmmo(-1);
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
        AdjustAmmo(currentWeaponSO.MagazineSize);
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
