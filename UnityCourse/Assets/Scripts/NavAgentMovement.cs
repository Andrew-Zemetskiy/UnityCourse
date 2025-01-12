using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class NavAgentMovement : MonoBehaviour
{
    private InputSystem _inputSystem;
    
    private Camera _cam;
    private NavMeshAgent _agent;
    private float _baseSpeed;

    private void Awake()
    {
        _inputSystem = new InputSystem();
    }

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
        _inputSystem.Player.Move.performed += OnMouseClick;
        _inputSystem.Enable();
    }

    private void OnDisable()
    {
        ZoneManager.Instance.OnPlayerEnterSlowZone -= OnEnterInSlowZone;
        ZoneManager.Instance.OnPlayerExitSlowZone -= ResetSpeed;
        _inputSystem.Disable();
        _inputSystem.Player.Move.performed -= OnMouseClick;
    }

    private void OnMouseClick(InputAction.CallbackContext ctx)
    {
        Ray ray = _cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            _agent.SetDestination(hit.point);
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