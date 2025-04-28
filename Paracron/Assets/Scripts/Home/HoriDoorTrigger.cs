using System.Collections;
using UnityEngine;

public class HoriDoorDrigger : MonoBehaviour
{
    // [SerializeField] Transform[] horiDoor;
    // [SerializeField] Vector3[] movementVector;
    // [SerializeField] float openDoorDuration = 1f;

    // private Vector3[] startPos;
    // private Vector3[] endPos;

    // AudioSource audioSource;

    // void Start()
    // {
    //     for (int i = 0; i < 2; i++)
    //     {
    //         audioSource = horiDoor[i].GetComponent<AudioSource>();
    //         startPos[i] = horiDoor[i].position;
    //         endPos[i] = horiDoor[i].position + movementVector[i];
    //     }

    // }


    // void OnTriggerEnter(Collider other)
    // {
    //     if (!other.CompareTag("Player")) return;
    //     audioSource.Play();
    //     StopAllCoroutines();
    //     StartCoroutine(OpenDoorCoroutine(endPos));
    // }

    // void OnTriggerExit(Collider other)
    // {
    //     if (!other.CompareTag("Player")) return;
    //     audioSource.Play();
    //     StopAllCoroutines();
    //     StartCoroutine(OpenDoorCoroutine(startPos));
    // }

    // IEnumerator OpenDoorCoroutine(Vector3[] targetPos)
    // {
    //     Vector3[i] startPos = horiDoor[i].position;
    //     float elapsedTime = 0;
    //     while (elapsedTime < openDoorDuration)
    //     {
    //         float t = elapsedTime / openDoorDuration;
    //         elapsedTime += Time.deltaTime;
    //         rightDoor.transform.position = Vector3.Lerp(startPos, targetPos, t);
    //         yield return null;
    //     }
    //     rightDoor.transform.position = targetPos;

    // }
}
