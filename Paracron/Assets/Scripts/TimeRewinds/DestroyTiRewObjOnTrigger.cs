using UnityEngine;

public class DestroyTiRewObjOnTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out TimeRewindableObject timeRewindableObject))
        {
            // Effectもだしたいけど、どこにかけばいいか迷うので一旦ただただ削除
            // TimeRewindObjectがトリガーに入ったときに自分自身を削除する
            Destroy(other.gameObject);
        }
    }
}
