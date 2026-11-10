using UnityEngine;

[RequireComponent(typeof(Health))]
public class Resource : MonoBehaviour
{
    [SerializeField] private TreeVisual _treeVisual;

    private Health _health;

    public Health Health => _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    public void PlayHit()
    {
        _treeVisual.PlayHit();
    }
}