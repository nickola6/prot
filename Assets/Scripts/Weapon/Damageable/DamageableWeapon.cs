using UnityEngine;

public abstract class DamageableWeapon : Weapon
{
    [SerializeField, Min(0.01f)] private float _attackSpeed = 2f;

    public float AttackSpeed => _attackSpeed;

    public abstract void Attack(Vector2 direction);
}