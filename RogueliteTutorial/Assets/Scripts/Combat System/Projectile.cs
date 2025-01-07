using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float timeout = 5f;
    public int damage = 10;
    public LayerMask targetLayers;

    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        Destroy(this.gameObject, timeout);
    }

    public void Launch(Vector2 direction)
    {
        spriteRenderer.flipX = (direction == Vector2.left);
        rb2d.AddForce(direction * speed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (LayerMaskUtility.IsInLayerMask(other.gameObject, targetLayers))
        {
            other.gameObject.GetComponent<IHealth>()?.TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
