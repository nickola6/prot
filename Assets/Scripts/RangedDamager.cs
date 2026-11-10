using UnityEngine;

public class RangedDamager : MonoBehaviour
{
    [SerializeField] private float _damage = 10f;
    [SerializeField] private DamageableArea _damageableArea;

    public void Attack()
    {
        Health target = _damageableArea.GetClosestTarget();

        if (target == null || target.CurrentHealth <= 0f)
            return;

        target.TakeDamage(_damage);
    }
}