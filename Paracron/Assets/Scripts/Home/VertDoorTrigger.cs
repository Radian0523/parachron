using System.Collections;
using UnityEngine;

public class VertDoorTrigger : MonoBehaviour
{
    [SerializeField] Transform vertDoor;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float openDoorDuration = 1f;

    private Vector3 startPos;
    private Vector3 endPos;

    AudioSource audioSource;

    void Start()
    {
        audioSource = vertDoor.GetComponent<AudioSource>();
        startPos = vertDoor.position;
        endPos = vertDoor.position + movementVector;
    }


    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        audioSource.Play();
        StopAllCoroutines();
        StartCoroutine(OpenDoorCoroutine(endPos));
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        audioSource.Play();
        StopAllCoroutines();
        StartCoroutine(OpenDoorCoroutine(startPos));
    }

    IEnumerator OpenDoorCoroutine(Vector3 targetPos)
    {
        Vector3 startPos = vertDoor.position;
        float elapsedTime = 0;
        while (elapsedTime < openDoorDuration)
        {
            float t = elapsedTime / openDoorDuration;
            elapsedTime += Time.deltaTime;
            vertDoor.transform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }
        vertDoor.transform.position = targetPos;

    }
}
