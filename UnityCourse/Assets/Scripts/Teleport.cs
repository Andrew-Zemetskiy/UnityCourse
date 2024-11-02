using UnityEngine;
using Random = UnityEngine.Random;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform targetObject;
    [SerializeField] private BoxCollider teleportArea;
    [SerializeField] private float teleportInterval = 2f;

    [SerializeField] private Color gizmosColor = Color.red;

    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= teleportInterval)
        {
            Teleportation();
            _timer -= teleportInterval;
        }
    }

    private void Teleportation()
    {
        Bounds bounds = teleportArea.bounds;

        Vector3 randomPos = new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );

        targetObject.position = randomPos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawWireCube(teleportArea.bounds.center, teleportArea.bounds.size);
    }
}