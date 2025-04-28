using UnityEngine;

public class CommanderTextTrigger : BaseTrigger
{
    public bool text = false;

    protected override void OnPlayerEnter(Collider other)
    {
        text = true;
    }

    protected override void OnPlayerExit(Collider other)
    {
    }
}