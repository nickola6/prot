using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(SortingGroup))]
public class YSorter : MonoBehaviour
{
    [SerializeField] private int _sortingOrderBase;
    [SerializeField] private float _offset;
    [SerializeField] private float _precision = 100f;

    private SortingGroup _sortingGroup;

    private void Awake()
    {
        _sortingGroup = GetComponent<SortingGroup>();
    }

    private void LateUpdate()
    {
        _sortingGroup.sortingOrder = _sortingOrderBase - Mathf.RoundToInt((transform.position.y + _offset) * _precision);
    }
}