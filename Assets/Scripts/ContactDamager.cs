
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamager : MonoBehaviour
{
    [SerializeField] private DamageableArea _damageableArea;
    [SerializeField] private float _damage = 50;
    [SerializeField] private float _hitInterval = 1;

    private readonly HashSet<Health> _targets = new();
    private readonly HashSet<Health> _contacts = new();
    private readonly Dictionary<Health, Coroutine> _damageRoutines = new();

    private void OnEnable()
    {
        _damageableArea.TargetEntered += OnTargetEntered;
        _damageableArea.TargetExited += OnTargetExited;

        foreach (Health target in _damageableArea.Targets)
            OnTargetEntered(target);
    }

    private void OnDisable()
    {
        _damageableArea.TargetEntered -= OnTargetEntered;
        _damageableArea.TargetExited -= OnTargetExited;

        foreach (Coroutine routine in _damageRoutines.Values)
            StopCoroutine(routine);

        _damageRoutines.Clear();
        _contacts.Clear();
        _targets.Clear();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ProcessContactEnter(collision.collider);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        ProcessContactExit(collision.collider);
    }

    private void ProcessContactEnter(Collider2D other)
    {
        Health target = other.GetComponentInParent<Health>();

        if (target == null || _targets.Contains(target) == false || _contacts.Add(target) == false)
            return;

        target.TakeDamage(_damage);
        _damageRoutines.Add(target, StartCoroutine(DamageRoutine(target)));
    }

    private void ProcessContactExit(Collider2D other)
    {
        Health target = other.GetComponentInParent<Health>();

        if (target == null)
            return;

        _contacts.Remove(target);
        StopDamageRoutine(target);
    }

    private void OnTargetEntered(Health target)
    {
        if (target != null)
            _targets.Add(target);
    }

    private void OnTargetExited(Health target)
    {
        if (target == null)
            return;

        _targets.Remove(target);
        _contacts.Remove(target);
        StopDamageRoutine(target);
    }

    private IEnumerator DamageRoutine(Health target)
    {
        while (target != null && _contacts.Contains(target))
        {
            yield return new WaitForSeconds(_hitInterval);

            if (target == null || _contacts.Contains(target) == false)
                break;

            target.TakeDamage(_damage);
        }

        _damageRoutines.Remove(target);
    }

    private void StopDamageRoutine(Health target)
    {
        if (_damageRoutines.TryGetValue(target, out Coroutine routine) == false)
            return;

        StopCoroutine(routine);
        _damageRoutines.Remove(target);
    }
}
