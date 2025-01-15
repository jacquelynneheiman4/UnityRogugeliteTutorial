using UnityEngine;

public class Movement : MonoBehaviour, IMovement
{
    public virtual void Move(Vector3 direction) {}

    public virtual void SetFacingDirection(bool isLeft)
    {
        FlipX(isLeft);
    }

    protected void FlipX(bool flip)
    {
        float currentScale = Mathf.Abs(transform.localScale.x);

        Vector3 scale = transform.localScale;
        scale.x = flip ? -currentScale : currentScale;

        transform.localScale = scale;
    }
}