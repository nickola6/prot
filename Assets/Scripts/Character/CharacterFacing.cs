using UnityEngine;

public class CharacterFacing : MonoBehaviour
{
    private const float DirectionThreshold = 0.001f;

    private Vector3 _initialScale;
    private Vector2 _facingDirection = Vector2.right;

    public Vector2 FacingDirection => _facingDirection;

    private void Awake()
    {
        _initialScale = transform.localScale;
    }

    public void SetDirection(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) <= DirectionThreshold && Mathf.Abs(direction.y) <= DirectionThreshold)
            return;

        bool faceRight = Mathf.Abs(direction.x) > DirectionThreshold && direction.x > 0f;

        Vector3 scale = _initialScale;
        scale.x = Mathf.Abs(_initialScale.x) * (faceRight ? 1f : -1f);

        transform.localScale = scale;
        _facingDirection = faceRight ? Vector2.right : Vector2.left;
    }
}