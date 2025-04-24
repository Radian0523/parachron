using System.Collections;
using UnityEngine;

public class SpawnerFromMe : MonoBehaviour
{
    [SerializeField] GameObject spawnPrefab; // プレハブをInspectorから設定
    [SerializeField] float spawnInterval = 2f; // スポーン間隔

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnRoutine()); // スポーンルーチンを開始
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval); // スポーン間隔を待つ
            Instantiate(spawnPrefab, transform.position, Quaternion.identity); // プレハブをスポーン
        }
    }
}
