using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericPool<T, TConfig> : MonoBehaviour
    where T : Component
    where TConfig : ScriptableObject
{
    private readonly Dictionary<T, Queue<T>> _availableObjects = new();
    private readonly Dictionary<T, T> _prefabsByInstance = new();

    private Transform _poolRoot;

    protected void Initialize(TConfig[] configs)
    {
        if (configs == null || configs.Length == 0)
            throw new InvalidOperationException($"{GetType().Name} requires Configs.");

        GameObject poolRoot = new($"{GetType().Name}_Pool");
        poolRoot.transform.SetParent(transform);
        _poolRoot = poolRoot.transform;

        foreach (TConfig config in configs)
        {
            if (config == null)
                throw new InvalidOperationException($"{GetType().Name} contains a null Config.");

            T prefab = GetPrefab(config);
            int initialCapacity = GetInitialCapacity(config);

            if (prefab == null)
                throw new InvalidOperationException($"{GetType().Name} Config requires Prefab.");

            if (initialCapacity < 0)
                throw new ArgumentOutOfRangeException(nameof(initialCapacity));

            if (_availableObjects.ContainsKey(prefab) == true)
                throw new InvalidOperationException($"{GetType().Name} contains duplicate Prefabs.");

            _availableObjects.Add(prefab, new Queue<T>());

            for (int i = 0; i < initialCapacity; i++)
                _availableObjects[prefab].Enqueue(CreateObject(prefab));
        }
    }

    public T Get(TConfig config, Transform spawnPoint)
    {
        if (config == null)
            throw new ArgumentNullException(nameof(config));

        if (spawnPoint == null)
            throw new ArgumentNullException(nameof(spawnPoint));

        T prefab = GetPrefab(config);

        if (prefab == null)
            throw new InvalidOperationException($"{GetType().Name} Config requires Prefab.");

        if (_availableObjects.TryGetValue(prefab, out Queue<T> availableObjects) == false)
            throw new InvalidOperationException($"{GetType().Name} does not contain {prefab.name}.");

        T instance = availableObjects.Count > 0 ? availableObjects.Dequeue() : CreateObject(prefab);

        instance.transform.SetParent(spawnPoint);
        instance.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);

        Spawn(instance);
        instance.gameObject.SetActive(true);

        return instance;
    }

    public void Release(T instance)
    {
        if (instance == null)
            throw new ArgumentNullException(nameof(instance));

        if (_prefabsByInstance.TryGetValue(instance, out T prefab) == false)
            throw new InvalidOperationException($"{instance.name} does not belong to {GetType().Name}.");

        if (instance.gameObject.activeSelf == false)
            return;

        ReturnToPool(instance);
        instance.gameObject.SetActive(false);
        instance.transform.SetParent(_poolRoot);

        _availableObjects[prefab].Enqueue(instance);
    }

    protected abstract T GetPrefab(TConfig config);
    protected abstract int GetInitialCapacity(TConfig config);

    protected virtual void Spawn(T instance)
    {
    }

    protected virtual void ReturnToPool(T instance)
    {
    }

    private T CreateObject(T prefab)
    {
        T instance = Instantiate(prefab, _poolRoot);
        instance.gameObject.SetActive(false);
        _prefabsByInstance.Add(instance, prefab);

        return instance;
    }
}