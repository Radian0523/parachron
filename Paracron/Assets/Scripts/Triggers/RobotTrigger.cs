using System.Collections.Generic;
using UnityEngine;

public class RobotTrigger : BaseTrigger
{
    [SerializeField] List<Robot> robots;

    void Start()
    {
        foreach (Robot robot in robots)
        {
            robot.OnStopChase();
        }
    }

    protected override void OnPlayerEnter(Collider other)
    {
        foreach (Robot robot in robots)
        {
            if (!robot) continue;
            if (!robot.gameObject) continue;
            robot.OnStartChase();
        }
    }

    protected override void OnPlayerExit(Collider other)
    { }

}
