using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AxeWeapon : HarvestableWeapon
{
    private const string HarvestTrigger = "Harvest";

    [SerializeField] private TrailRenderer _trail;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _trail.emitting = false;
    }

    public override void Attack()
    {
        _animator.SetTrigger(HarvestTrigger);
    }

    public void EnableTrail()
    {
        _trail.emitting = true;
    }

    public void DisableTrail()
    {
        _trail.emitting = false;
    }
}