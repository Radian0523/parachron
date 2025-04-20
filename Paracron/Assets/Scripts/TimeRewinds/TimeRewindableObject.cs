using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.AI;

public class TimeRewindableObject : MonoBehaviour
{
    private struct TransformSnapshot
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;
    }

    private List<TransformSnapshot> history = new List<TransformSnapshot>();
    [SerializeField] private float recordTime = 10f; // 記録時間（秒）
    // [SerializeField] private float rewindTimeInterval = 0.06f;

    private bool isRewinding = false;
    private bool wasAgentEnabled; // NavMeshAgentが有効だったかどうか
    // int rewindTimeScaleAdjuster = 0;

    Rigidbody rb;
    NavMeshAgent agent;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (isRewinding)
        {
            Rewind();
        }
        else
        {
            Record();
        }
    }

    void Record()
    {
        if (history.Count > Mathf.RoundToInt(recordTime / Time.fixedDeltaTime))
        {
            history.RemoveAt(history.Count - 1); // 一番古いものを削除
        }

        if (history.Count > 0)
        {
            // 最後の位置を取得
            var lastSnapshot = history[0];
            if (lastSnapshot.position == transform.position && lastSnapshot.rotation == transform.rotation && lastSnapshot.scale == transform.localScale)
            {
                return; // 変化がない場合は記録しない
            }
        }

        history.Insert(0, new TransformSnapshot
        {
            position = transform.position,
            rotation = transform.rotation,
            scale = transform.localScale
        });
    }

    void Rewind()
    {
        // rewindTimeScaleAdjuster++;
        // if (rewindTimeScaleAdjuster >= rewindTimeInterval)
        // {
        //     rewindTimeScaleAdjuster = 0;
        // }
        // else
        // {
        //     return; // 一定時間ごとにリワインドを実行
        // }


        if (history.Count > 0)
        {
            var snapshot = history[0];
            transform.position = snapshot.position;
            transform.rotation = snapshot.rotation;
            transform.localScale = snapshot.scale;
            history.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }
    }

    public void StartRewind()
    {
        isRewinding = true;
        if (rb) rb.isKinematic = true; // Rigidbodyを無効にする
        if (agent) { wasAgentEnabled = agent.enabled; agent.enabled = false; } // NavMeshAgentを無効にする(agentが生きている状態でTransformを変更するとワープする)
    }

    public void StopRewind()
    {
        isRewinding = false;
        if (rb) rb.isKinematic = false; // Rigidbodyを有効に戻す
        if (agent) agent.enabled = wasAgentEnabled; // NavMeshAgentを元に戻す
    }
}

