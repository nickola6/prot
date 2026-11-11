
using UnityEngine;

public class EnemyPool : GenericPool<Enemy, EnemySpawnConfigSO>
{
    [SerializeField] private EnemySpawnConfigSO[] _configs;

    private void Awake()
    {
        Initialize(_configs);
    }

    protected override Enemy GetPrefab(EnemySpawnConfigSO config)
    {
        return config.Prefab;
    }

    protected override int GetInitialCapacity(EnemySpawnConfigSO config)
    {
        return config.InitialPoolSize;
    }

    protected override void Spawn(Enemy enemy)
    {
        enemy.Died += OnEnemyDied;
        enemy.Restore();
    }

    protected override void ReturnToPool(Enemy enemy)
    {
        enemy.Died -= OnEnemyDied;
    }

    private void OnEnemyDied(Enemy enemy)
    {
        Release(enemy);
    }
}
