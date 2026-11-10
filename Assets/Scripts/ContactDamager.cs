using System.Collections;
using UnityEngine;

public class ContactDamager : MonoBehaviour
{
    [SerializeField] private float _damage = 10;
    [SerializeField] private int _hitCount = 1;
    [SerializeField] private float _hitInterval = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Health target = other.GetComponentInParent<Health>();
        target.TakeDamage(_damage);

        StartCoroutine(DamageRoutine(target));
    }

    private IEnumerator DamageRoutine(Health target)
    {
        for (int i = 0; i < _hitCount; i++)
        {
            if (target == null)
                yield break;

            target.TakeDamage(_damage);

            if (i < _hitCount - 1)
                yield return new WaitForSeconds(_hitInterval);
        }
    }
}