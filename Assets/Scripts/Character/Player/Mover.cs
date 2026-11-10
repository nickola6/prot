using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private float _moveSpeed = 4f;

    private Rigidbody2D _rigidbody;
    private CharacterFacing _characterFacing;
    private Vector2 _moveInput;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _characterFacing = GetComponentInChildren<CharacterFacing>(true);

        if (_inputReader == null)
            throw new InvalidOperationException($"{nameof(Mover)} requires {nameof(InputReader)}.");

        if (_characterFacing == null)
            throw new InvalidOperationException($"{nameof(Mover)} requires {nameof(CharacterFacing)} in children.");
    }

    private void OnEnable()
    {
        _inputReader.MovementUpdated += OnMovementUpdated;
    }

    private void OnDisable()
    {
        _inputReader.MovementUpdated -= OnMovementUpdated;
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _moveInput * _moveSpeed;
    }

    private void OnMovementUpdated(Vector2 movement)
    {
        _moveInput = movement;
        _characterFacing.SetDirection(movement);
    }
}