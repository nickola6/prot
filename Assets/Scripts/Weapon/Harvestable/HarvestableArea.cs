using System;
using System.Collections.Generic;
using UnityEngine;

public class HarvestableArea : DetectionArea
{
    [SerializeField] private HarvestableWeapon _weapon;

    private readonly HashSet<Resource> _targets = new();
    private float _nextAttackTime;

    public event Action<Resource> TargetEntered;
    public event Action<Resource> TargetExited;

    public IReadOnlyCollection<Resource> Targets => _targets;

    private void OnEnable()
    {
        TargetEntered += OnResourceEntered;
    }

    private void OnDisable()
    {
        TargetEntered -= OnResourceEntered;
        _targets.Clear();
    }

    private void Update()
    {
        if (_targets.Count == 0 || Time.time < _nextAttackTime)
            return;

        PerformHarvest();
    }

    protected override void OnTargetEntered(Collider2D other)
    {
        Resource resource = other.GetComponentInParent<Resource>();

        if (resource != null && _targets.Add(resource))
            TargetEntered?.Invoke(resource);
    }

    protected override void OnTargetExited(Collider2D other)
    {
        Resource resource = other.GetComponentInParent<Resource>();

        if (resource != null && _targets.Remove(resource))
            TargetExited?.Invoke(resource);
    }

    private void OnResourceEntered(Resource resource)
    {
        if (_targets.Count == 1)
            PerformHarvest();
    }

    private void PerformHarvest()
    {
        _weapon.Attack();

        foreach (Resource resource in _targets)

            if (resource != null)
            {
                resource.Health.TakeDamage(_weapon.Damage);
                resource.PlayHit();
            }

        _nextAttackTime = Time.time + 1f / _weapon.AttackSpeed;
    }
}