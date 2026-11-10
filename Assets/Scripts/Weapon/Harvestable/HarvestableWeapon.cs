
using UnityEngine;

public abstract class HarvestableWeapon : Weapon
{
    [SerializeField, Min(0.01f)] private float _attackSpeed = 2f;

    public float AttackSpeed => _attackSpeed;

    public abstract void Attack();
}
