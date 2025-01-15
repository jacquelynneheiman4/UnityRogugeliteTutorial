using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : Movement
{
    public float speed = 5f;

    private Vector2 movementInput;
    private Vector2 lastMoveDirection = Vector2.right;
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        movementInput = movementInput.normalized;

        Vector2 movement = movementInput * speed * Time.fixedDeltaTime;
        Move(movement);
        
        if (movementInput.x != 0)
        {
            lastMoveDirection.x = movementInput.x;
            SetFacingDirection(movementInput.x < 0f);
        }

        animator.SetBool("isMoving", movementInput != Vector2.zero);
    }

    public override void Move(Vector3 direction)
    {
        rb2d.MovePosition(rb2d.position + (Vector2)direction);
    }
}