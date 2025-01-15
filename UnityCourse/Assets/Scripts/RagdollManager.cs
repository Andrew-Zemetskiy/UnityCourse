using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RagdollManager : MonoBehaviour
{
    private Animator _animator;
    public List<Rigidbody> ragdollParts;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        DeactivateRagdoll();
    }

    private void OnEnable()
    {
        PlayerMove.OnPlayerDeath += ActivateRagdoll;
        PlayerMove.OnPlayerStandUp += DeactivateRagdoll;
    }

    private void OnDisable()
    {
        PlayerMove.OnPlayerDeath -= ActivateRagdoll;
        PlayerMove.OnPlayerStandUp -= DeactivateRagdoll;
    }

    public void ActivateRagdoll()
    {
        _animator.enabled = false;
        SetValueToRagdoll(false);
    }

    public void DeactivateRagdoll()
    {
        _animator.enabled = true;
        SetValueToRagdoll(true);
    }

    private void SetValueToRagdoll(bool value)
    {
        foreach (var rag in ragdollParts)
        {
            rag.isKinematic = value;
        }
    }
}
