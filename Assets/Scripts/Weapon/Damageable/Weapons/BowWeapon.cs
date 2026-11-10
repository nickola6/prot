
using System;
using UnityEngine;

public class BowWeapon : DamageableWeapon
{
    //[SerializeField] private ArrowProjectile _arrowPrefab;
    [SerializeField, Min(0f)] private float _projectileSpeed = 12f;

    private void Awake()
    {
        //if (_arrowPrefab == null)
            //throw new InvalidOperationException($"{nameof(BowWeapon)} requires {nameof(ArrowProjectile)} prefab.");
    }

    public override void Attack(Vector2 direction)
    {
        if (direction.sqrMagnitude <= Mathf.Epsilon)
            return;

        direction.Normalize();

        //ArrowProjectile projectile = Instantiate(_arrowPrefab, transform.position, Quaternion.identity);
      //  projectile.Initialize(direction, _projectileSpeed, Damage, transform.root);
    }
}
