using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpawnConfig", menuName = "Enemy Spawn Config")]
public class EnemySpawnConfigSO : ScriptableObject
{
    [SerializeField] private Enemy _prefab;
    [SerializeField] private int _spawnCount = 5;
    [SerializeField] private float _startDelay = 2;
    [SerializeField] private float _spawnInterval = 1f;
    [SerializeField] private int _initialPoolSize = 5;

    public Enemy Prefab => _prefab;
    public int SpawnCount => _spawnCount;
    public float StartDelay => _startDelay;
    public float SpawnInterval => _spawnInterval;
    public int InitialPoolSize => _initialPoolSize;
}