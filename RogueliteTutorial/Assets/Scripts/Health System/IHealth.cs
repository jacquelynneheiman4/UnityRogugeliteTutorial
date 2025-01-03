public interface IHealth
{
    float CurrentHealth { get; }
    float MaxHealth { get; }

    void TakeDamage(float amount);
    void Heal(float amount);
    void ResetHealth();
}
