using UnityEngine;

public class InstantiateTrigger : BaseTrigger
{
    [SerializeField] GameObject prefabToInstantiate; // InstantiateするPrefab
    [SerializeField] Transform spawnPoint; // Instantiateする位置
    protected override void OnPlayerEnter(Collider other)
    {
        // プレイヤーがトリガーに入ったときにPrefabをInstantiateする
        if (prefabToInstantiate != null && spawnPoint != null)
        {
            Instantiate(prefabToInstantiate, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning("Prefab or spawn point is not set.");
        }
    }

    protected override void OnPlayerExit(Collider other)
    {
    }

}
