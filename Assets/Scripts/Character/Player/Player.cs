using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private HarvestableArea _harvestableArea;

    public IReadOnlyCollection<Resource> Resources => _harvestableArea.Targets;
}