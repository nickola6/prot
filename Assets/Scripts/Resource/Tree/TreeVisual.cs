
using System;
using UnityEngine;

public class TreeVisual : MonoBehaviour
{
    private const string SwayingTrigger = "Swaying";

    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _leafParticles;
    [SerializeField] private ParticleSystem _woodChipsParticles;

    private void Awake()
    {
        if (_animator == null)
            throw new InvalidOperationException($"{nameof(TreeVisual)} requires Animator.");

        if (_leafParticles == null)
            throw new InvalidOperationException($"{nameof(TreeVisual)} requires LeafParticles.");

        if (_woodChipsParticles == null)
            throw new InvalidOperationException($"{nameof(TreeVisual)} requires WoodChipsParticles.");
    }

    public void PlayHit()
    {
        _animator.SetTrigger(SwayingTrigger);
        _leafParticles.Play();
        _woodChipsParticles.Play();
    }
}
