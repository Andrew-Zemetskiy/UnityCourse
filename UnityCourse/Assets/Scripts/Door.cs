using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform _door;
    [SerializeField] private Transform _openPosTarget;
    [SerializeField] private float _openSpeed;

    
    private Vector3 _openPos;
    private Vector3 _closePos;

    private void Start()
    {
        _openPos = _openPosTarget.position;
        _closePos = _door.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Door is open");
            OpenDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Door is close");
            CloseDoor();
        }
    }

    private void OpenDoor()
    {
        StopAllCoroutines();
        StartCoroutine(MoveDoor(_openPos)); 
    }

    private void CloseDoor()
    {
        StopAllCoroutines();
        StartCoroutine(MoveDoor(_closePos)); 
    }
    
    private IEnumerator MoveDoor(Vector3 targetPos)
    {
        while (Vector3.Distance(_door.position, targetPos) > 0.01f)
        {
            _door.position = Vector3.MoveTowards(_door.position, targetPos, _openSpeed * Time.deltaTime);
            yield return null;
        }

        _door.position = targetPos;
    }
}
