using UnityEngine;

[CreateAssetMenu(fileName = "ResourceSpawnConfig", menuName = "Resource Spawn Config")]
public class ResourceSpawnConfigSO : ScriptableObject
{
    [SerializeField] private Resource _prefab;
    [SerializeField] private int _initialPoolSize = 10;

    public Resource Prefab => _prefab;
    public int InitialPoolSize => _initialPoolSize;
}