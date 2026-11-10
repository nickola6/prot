using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private float _maxHealth = 100f;

    private float _currentHealth;

    public event Action<float, float> HealthChanged;

    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _maxHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (damage <= 0f)
            return;

        _currentHealth = Mathf.Max(0f, _currentHealth - damage);
        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    public void Restore()
    {
        _currentHealth = _maxHealth;
        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }
}