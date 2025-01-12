using System;
using UnityEngine;
using UnityEngine.AI;

public class ZoneManager : MonoBehaviour
{
    public static ZoneManager Instance;
    public Action<float> OnPlayerEnterSlowZone;
    public Action OnPlayerExitSlowZone;

    [SerializeField, Range(0, 100)] private float _slowZoneStrength;
    private NavMeshAgent _agent;

    private NavMeshAgent Agent
    {
        get { return _agent ??= FindFirstObjectByType<NavMeshAgent>(); }
        set { _agent = value; }
    }

    private bool _isInSlowZone = false;

    private void Awake()
    {
        Instance = this;
    }

    private void FixedUpdate()
    {
        SlowZoneCheck();
    }
    
    private void OnEnable()
    {
        GameManager.OnPlayerInit += AgentInit;
    }
    private void OnDisable()
    {
        GameManager.OnPlayerInit -= AgentInit;
    }

    
    private void AgentInit()
    {
        Agent = FindFirstObjectByType<NavMeshAgent>();
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
        if (!Agent) return false;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(Agent.transform.position, out hit, 1.0f, NavMesh.AllAreas))
        {
            int areaMask = hit.mask;
            int slowZoneMask = 1 << NavMesh.GetAreaFromName("SlowZone");

            return (areaMask & slowZoneMask) != 0;
        }

        return false;
    }
}