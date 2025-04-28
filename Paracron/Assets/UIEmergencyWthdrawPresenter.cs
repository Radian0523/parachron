using Microsoft.Unity.VisualStudio.Editor;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class UIEmergencyWthdrawPresenter : MonoBehaviour
{
    [SerializeField] GameObject emergencyWithdrawContainer;
    [SerializeField] UnityEngine.UI.Image eButtonImage;
    [SerializeField] StarterAssetsInputs inputs;
    [SerializeField] float holdTime = 2f;

    float holdTimer = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ContainerSetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!emergencyWithdrawContainer) return;
        if (inputs.withdraw)
        {
            holdTimer += Time.deltaTime;
        }
        else
        {
            holdTimer = 0;
        }
        eButtonImage.fillAmount = 1 - holdTimer / holdTime;

        if (eButtonImage.fillAmount <= 0)
        {
            Debug.Log("go to next scene");
        }
    }

    public void ContainerSetActive(bool newState)
    {
        emergencyWithdrawContainer.SetActive(newState);
    }
}
