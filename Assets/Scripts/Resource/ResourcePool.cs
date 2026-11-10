using UnityEngine;

public class ResourcePool : GenericPool<Resource, ResourceSpawnConfigSO>
{
    [SerializeField] private ResourceSpawnConfigSO[] _configs;

    private void Awake()
    {
        Initialize(_configs);
    }

    protected override Resource GetPrefab(ResourceSpawnConfigSO config)
    {
        return config.Prefab;
    }

    protected override int GetInitialCapacity(ResourceSpawnConfigSO config)
    {
        return config.InitialPoolSize;
    }

    protected override void Spawn(Resource resource)
    {
        resource.Health.Restore();
    }
}