using UnityEngine;

public interface IMovement
{
    void Move(Vector3 direction);
    void SetFacingDirection(bool isLeft);
}