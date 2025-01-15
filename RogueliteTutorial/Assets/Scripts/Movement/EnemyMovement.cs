using UnityEngine;

public class EnemyMovement : Movement
{
    public float speed = 1f;

    public override void Move(Vector3 destination)
    {
        transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        
        Vector3 direction = destination - transform.position;
        SetFacingDirection(direction.x < 0f);
    }
}