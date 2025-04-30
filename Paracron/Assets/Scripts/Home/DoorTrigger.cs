using System.Collections;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] Transform door;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float openDoorDuration = 1f;

    private Vector3 startPos;
    private Vector3 endPos;

    AudioSource audioSource;

    void Start()
    {
        audioSource = door.GetComponent<AudioSource>();
        startPos = door.position;
        endPos = door.position + movementVector;
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
        Vector3 startPos = door.position;
        float elapsedTime = 0;
        while (elapsedTime < openDoorDuration)
        {
            float t = elapsedTime / openDoorDuration;
            elapsedTime += Time.deltaTime;
            door.transform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }
        door.transform.position = targetPos;

    }
}
