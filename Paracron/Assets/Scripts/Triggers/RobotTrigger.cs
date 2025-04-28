using System.Collections.Generic;
using UnityEngine;

public class RobotTrigger : BaseTrigger
{
    [SerializeField] Robot[] robots;

    void Start()
    {
        RobotManager.SetRobotAgentEnabled(robots, false);

    }

    protected override void OnPlayerEnter(Collider other)
    {
        RobotManager.SetRobotAgentEnabled(robots, true);
    }

    protected override void OnPlayerExit(Collider other)
    { }

}
