
using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Follower))]
public class Enemy : MonoBehaviour
{
    private Health _health;
    private Follower _follower;
    private bool _isDead;

    public event Action<Enemy> Died;

    public Health Health => _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _follower = GetComponent<Follower>();
    }

    private void OnEnable()
    {
        _isDead = false;
        _health.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= OnHealthChanged;
    }

    public void Initialize(Transform player)
    {
        _follower.Initialize(player);
    }

    public void Restore()
    {
        _health.Restore();
    }

    private void OnHealthChanged(float currentHealth, float maxHealth)
    {
        if (currentHealth > 0f || _isDead == true)
            return;

        _isDead = true;
        Died?.Invoke(this);
    }
}
