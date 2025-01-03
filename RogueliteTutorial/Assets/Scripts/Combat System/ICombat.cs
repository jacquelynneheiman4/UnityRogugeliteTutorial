using UnityEngine;

public interface ICombat
{
    float AttackRange { get; }
    int AttackDamage { get; }
    LayerMask AttackLayer { get; }

    void Attack();
}
