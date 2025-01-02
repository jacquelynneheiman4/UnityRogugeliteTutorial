using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public float damageAmount = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        IHealth health = other.GetComponent<IHealth>();

        if (health != null)
        {
            health.TakeDamage(damageAmount);
        }
    }
}
