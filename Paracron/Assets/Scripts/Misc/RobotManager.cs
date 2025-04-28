using UnityEngine;

public static class RobotManager
{
    public static void SetRobotAgentEnabled(Robot[] robots, bool newRobotState)
    {
        foreach (Robot robot in robots)
        {
            if (!robot) continue;
            if (!robot.gameObject) continue;
            robot.SetAgentEnabled(newRobotState);
        }
    }
}
