using System;
using UnityEngine;
using UnityEngine.AI;

public class ZoneManager : MonoBehaviour
{
    public static ZoneManager Instance;
    public Action<float> OnPlayerEnterSlowZone;
    public Action OnPlayerExitSlowZone;


    [SerializeField] private NavMeshAgent _agent;
    [SerializeField, Range(0, 100)] private float _slowZoneStrength;


    private bool _isInSlowZone = false;

    private void Awake()
    {
        Instance = this;
        if (_agent == null)
        {
            _agent = FindFirstObjectByType<NavMeshAgent>();
        }
    }

    private void FixedUpdate()
    {
        SlowZoneCheck();
    }

    private void SlowZoneCheck()
    {
        bool currentlyInSlowZone = IsInSlowZone();

        if (currentlyInSlowZone && !_isInSlowZone)
        {
            _isInSlowZone = true;
            OnPlayerEnterSlowZone?.Invoke(_slowZoneStrength);
        }
        else if (!currentlyInSlowZone && _isInSlowZone)
        {
            _isInSlowZone = false;
            OnPlayerExitSlowZone?.Invoke();
        }
    }


    private bool IsInSlowZone()
    {
        if (!_agent) return false;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(_agent.transform.position, out hit, 1.0f, NavMesh.AllAreas))
        {
            int areaMask = hit.mask;
            int slowZoneMask = 1 << NavMesh.GetAreaFromName("SlowZone");

            return (areaMask & slowZoneMask) != 0;
        }

        return false;
    }
}