using UnityEngine;

public class Combat : MonoBehaviour, ICombat
{
    [SerializeField] private float attackRange;
    [SerializeField] private int attackDamage;
    [SerializeField] private LayerMask attackLayer;

    public IAttackType attackType;

    public float AttackRange => attackRange;

    public int AttackDamage => attackDamage;

    public LayerMask AttackLayer => attackLayer;

    public virtual void Attack()
    {
        Debug.Log($"{gameObject.name} performed an attack.");
    }
}