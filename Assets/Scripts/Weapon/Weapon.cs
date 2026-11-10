using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField, Min(0f)] private float _damage = 10f;

    public float Damage => _damage;
}