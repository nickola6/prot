using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MoverAnimation : MonoBehaviour
{
    private const string SpeedParameter = "Speed";

    [SerializeField] private InputReader _inputReader;
    
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _inputReader.MovementUpdated += OnMovementUpdated;
    }

    private void OnDisable()
    {
        _inputReader.MovementUpdated -= OnMovementUpdated;
    }

    private void OnMovementUpdated(Vector2 movement)
    {
        _animator.SetFloat(SpeedParameter, movement.magnitude);
    }
}