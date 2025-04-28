using System.Collections;
using UnityEngine;

public struct ObjectsToMoveData
{
    public Transform transform;
    public Vector3 movementVector;
    public Vector3 endPos;
}

public class MoveTrigger : BaseTrigger
{
    [SerializeField] ObjectsToMoveData[] objectsToMove;

    void Start()
    {
        // foreach (ObjectsToMoveData objectToMove in objectsToMove)
        // {
        //     objectsToMove.endPos = objectsToMove.transform.position
        // }
    }

    protected override void OnPlayerEnter(Collider other)
    {

    }

    protected override void OnPlayerExit(Collider other)
    {

    }

    // IEnumerator MoveCoroutine()
    // {
    //     foreach (ObjectsToMoveData objectToMove in objectsToMove)
    //     {
    //         Vector3 startPos = objectToMove.transform.position;
    //         Vector3 targetPos = 

    //     }
    // }
}
