using UnityEngine;

public abstract class DetectionArea : MonoBehaviour
{
    protected void OnTriggerEnter2D(Collider2D other)
    {
        OnTargetEntered(other);
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        OnTargetExited(other);
    }

    protected abstract void OnTargetEntered(Collider2D other);
    protected abstract void OnTargetExited(Collider2D other);
}