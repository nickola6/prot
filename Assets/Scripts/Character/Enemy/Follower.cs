using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Follower : MonoBehaviour
{
    [SerializeField] private CharacterFacing _characterFacing;

    private NavMeshAgent _agent;
    private Transform _target;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();

        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    public void SetTarget(Transform target)
    {
        if (target == null)
            throw new ArgumentNullException(nameof(target));

        _target = target;
    }

    private void Update()
    {
        if (_target == null || _agent.isOnNavMesh == false)
            return;

        _agent.SetDestination(_target.position);

        Vector2 direction = _agent.velocity;

        if (direction.sqrMagnitude > 0.001f)
            _characterFacing.SetDirection(direction);
    }
}