
using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemySpawnConfigSO _config;
    [SerializeField] private EnemyPool _pool;
    [SerializeField] private Transform _player;

    private Transform _transform;
    private Coroutine _spawnRoutine;

    private void Awake()
    {
        _transform = transform;

        if (_config == null)
            throw new InvalidOperationException($"{nameof(EnemySpawner)} requires Config.");

        if (_pool == null)
            throw new InvalidOperationException($"{nameof(EnemySpawner)} requires EnemyPool.");

        if (_player == null)
            throw new InvalidOperationException($"{nameof(EnemySpawner)} requires Player reference.");
    }

    private void Start()
    {
        StartSpawning();
    }

    private void OnDisable()
    {
        StopSpawning();
    }

    public void StartSpawning()
    {
        if (_spawnRoutine != null)
            return;

        _spawnRoutine = StartCoroutine(SpawnRoutine());
    }

    public void StopSpawning()
    {
        if (_spawnRoutine == null)
            return;

        StopCoroutine(_spawnRoutine);
        _spawnRoutine = null;
    }

    private IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(_config.StartDelay);

        for (int i = 0; i < _config.SpawnCount; i++)
        {
            Enemy enemy = _pool.Get(_config, _transform);
            enemy.Initialize(_player);

            if (i < _config.SpawnCount - 1)
                yield return new WaitForSeconds(_config.SpawnInterval);
        }

        _spawnRoutine = null;
    }
}
