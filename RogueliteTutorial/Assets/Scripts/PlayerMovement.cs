using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    private Vector2 movementInput;
    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
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
    }
}
