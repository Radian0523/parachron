using UnityEngine;

public class ExplodesOnPlayerHit : MonoBehaviour
{
    [SerializeField] GameObject explosionEffect; // 爆発エフェクトのプレハブ
    [SerializeField] AudioClip explosionSound; // 爆発音のAudioClip

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // プレイヤーに当たったら爆発する
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
            Destroy(gameObject); // 自分自身を削除
        }
    }


}
