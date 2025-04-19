using System.Collections.Generic;
using UnityEngine;

public class RobotTrigger : MonoBehaviour
{
    [SerializeField] List<Robot> robots;
    bool passedThrough;

    void Start()
    {
        foreach (Robot robot in robots)
        {
            robot.OnStopChase();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (passedThrough) return;
        if (other.transform.CompareTag("Player"))
        {
            passedThrough = true;
            foreach (Robot robot in robots)
            {
                if (!robot) continue;
                if (!robot.gameObject) continue;
                robot.OnStartChase();
            }
        }
    }
}
