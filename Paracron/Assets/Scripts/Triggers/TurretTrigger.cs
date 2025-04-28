using UnityEngine;

public class TurretTrigger : BaseTrigger
{
    [SerializeField] Turret[] turrets;

    protected override void OnPlayerEnter(Collider other)
    {
        foreach (Turret turret in turrets)
        {
            turret.canSpawn = true;
        }
    }

    protected override void OnPlayerExit(Collider other)
    {
    }

    void Start()
    {
        foreach (Turret turret in turrets)
        {
            turret.canSpawn = false;
        }
    }




}