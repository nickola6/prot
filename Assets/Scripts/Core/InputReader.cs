using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const float InputChangeThreshold = 0.001f;

    private Vector2 _mobileMovement;
    private Vector2 _currentMovement;

    public event Action<Vector2> MovementUpdated;

    private void Update()
    {
        Vector2 keyboardMovement = ReadKeyboardMovement();

        Vector2 movement = keyboardMovement.sqrMagnitude > 0f
            ? keyboardMovement
            : _mobileMovement;

        movement = Vector2.ClampMagnitude(movement, 1f);

        UpdateMovement(movement);
    }

    public void SetMobileMovement(Vector2 movement)
    {
        _mobileMovement = Vector2.ClampMagnitude(movement, 1f);
    }

    private Vector2 ReadKeyboardMovement()
    {
        return new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );
    }

    private void UpdateMovement(Vector2 movement)
    {
        float difference = (movement - _currentMovement).sqrMagnitude;

        if (difference <= InputChangeThreshold * InputChangeThreshold)
        {
            return;
        }

        _currentMovement = movement;
        MovementUpdated?.Invoke(_currentMovement);
    }
}
