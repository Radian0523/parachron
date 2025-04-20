using UnityEngine;

public abstract class BaseTrigger : MonoBehaviour
{
    bool hadEntered = false; // プレイヤーがトリガーを通過したかどうかのフラグ
    bool hadExited = false; // プレイヤーがトリガーを通過したかどうかのフラグ

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (hadEntered) return; // すでに通過している場合は何もしない
            hadEntered = true; // プレイヤーがトリガーを通過したことを記録
            OnPlayerEnter(other);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (hadExited) return; // すでに通過している場合は何もしない
            hadExited = true; // プレイヤーがトリガーを通過したことを記録
            OnPlayerExit(other);
        }
    }

    protected abstract void OnPlayerEnter(Collider other);

    protected abstract void OnPlayerExit(Collider other);

}
