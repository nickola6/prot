using System;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Health _health;

    public event Action<float> HealthChanged;

    private void Awake()
    {
        _health = GetComponentInParent<Health>();
    }

    private void OnEnable()
    {
        _health.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(float currentHealth, float maxHealth)
    {
        HealthChanged?.Invoke(currentHealth / maxHealth);
    }
}