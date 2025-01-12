using UnityEngine;
using UnityEngine.AI;

public class NavAgentMovement : MonoBehaviour
{
    private Camera _cam;
    private NavMeshAgent _agent;
    private float _baseSpeed;

    private void Start()
    {
        _cam = Camera.main;
        _agent = GetComponent<NavMeshAgent>();
        _baseSpeed = _agent.speed;
    }

    private void OnEnable()
    {
        ZoneManager.Instance.OnPlayerEnterSlowZone += OnEnterInSlowZone;
        ZoneManager.Instance.OnPlayerExitSlowZone += ResetSpeed;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                _agent.SetDestination(hit.point);
            }
        }
    }

    private void OnEnterInSlowZone(float slowingStrength)
    {
        _agent.speed = _baseSpeed * (slowingStrength / 100);
    }

    private void ResetSpeed()
    {
        _agent.speed = _baseSpeed;
    }
}