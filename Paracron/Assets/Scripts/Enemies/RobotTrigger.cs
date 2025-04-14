using UnityEngine;

public class RobotTrigger : MonoBehaviour
{
    [SerializeField] Robot[] robots;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (Robot robot in robots)
        {
            robot.OnStopChase();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            foreach (Robot robot in robots)
            {
                robot.OnStartChase();
            }
        }
    }
}
