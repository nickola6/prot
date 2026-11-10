using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemySpawnConfigSO _config;
    [SerializeField] private EnemyPool _pool;

    private Transform _transform;
    private Coroutine _spawnRoutine;

    private void Awake()
    {
        _transform = transform;
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
            _pool.Get(_config, _transform);

            if (i < _config.SpawnCount - 1)
                yield return new WaitForSeconds(_config.SpawnInterval);
        }

        _spawnRoutine = null;
    }
}