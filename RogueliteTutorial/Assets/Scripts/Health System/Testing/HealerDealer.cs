using UnityEngine;

public class HealerDealer : MonoBehaviour
{
    public float healAmount = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        IHealth health = other.GetComponent<IHealth>();

        if (health != null)
        {
            health.Heal(healAmount);
        }
    }
}
