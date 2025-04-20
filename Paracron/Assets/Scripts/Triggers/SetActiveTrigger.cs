using UnityEngine;

public class SetActiveTrigger : BaseTrigger
{
    [SerializeField] GameObject[] objectsToActivate; // アクティブにするオブジェクト

    void Start()
    {
        foreach (var objectToActivate in objectsToActivate)
        {
            objectToActivate?.SetActive(false); // スタート時にオブジェクトを非アクティブにする
        }
    }
    protected override void OnPlayerEnter(Collider other)
    {

        foreach (var objectToActivate in objectsToActivate)
        {
            objectToActivate?.SetActive(true); // プレイヤーがトリガーに入ったときにオブジェクトをアクティブにする
        }

    }

    protected override void OnPlayerExit(Collider other)
    {
    }
}
