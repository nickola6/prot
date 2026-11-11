using System;
using System.Collections.Generic;
using UnityEngine;

public class DamageableArea : DetectionArea
{
    [SerializeField] private LayerMask _targetLayers;

    private readonly HashSet<Health> _targets = new();

    public event Action<Health> TargetEntered;
    public event Action<Health> TargetExited;

    public IReadOnlyCollection<Health> Targets => _targets;

    protected override void OnTargetEntered(Collider2D other)
    {
        if (IsTargetLayer(other.gameObject.layer) == false)
            return;

        Health health = other.GetComponentInParent<Health>();

        if (health != null && _targets.Add(health))
            TargetEntered?.Invoke(health);
    }

    protected override void OnTargetExited(Collider2D other)
    {
        if (IsTargetLayer(other.gameObject.layer) == false)
            return;

        Health health = other.GetComponentInParent<Health>();

        if (health != null && _targets.Remove(health))
            TargetExited?.Invoke(health);
    }

    public Health GetClosestTarget()
    {
        Health closestTarget = null;
        float closestDistance = float.MaxValue;

        foreach (Health target in _targets)
            if (target != null)
            {
                float distance = (target.transform.position - transform.position).sqrMagnitude;

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = target;
                }
            }

        return closestTarget;
    }

    private bool IsTargetLayer(int layer)
    {
        return (_targetLayers.value & (1 << layer)) != 0;
    }
}