using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
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
        rb2d.MovePosition(rb2d.position + movement);

        if (movementInput.x != 0)
        {
            lastMoveDirection.x = movementInput.x;
            FlipX(movementInput.x < 0f);
        }

        animator.SetBool("isMoving", movementInput != Vector2.zero);
    }

    private void FlipX(bool flip)
    {
        float currentScale = Mathf.Abs(transform.localScale.x);

        Vector3 scale = transform.localScale;
        scale.x = flip ? -currentScale : currentScale;

        transform.localScale = scale;
    }
}
