using UnityEngine;

public class Combat : MonoBehaviour, ICombat
{
    [SerializeField] private float attackRange;
    [SerializeField] protected float attackDelay;
    [SerializeField] private int attackDamage;
    [SerializeField] private LayerMask attackLayer;

    protected float lastAttackTime;

    public IAttackType attackType;

    public float AttackRange => attackRange;

    public int AttackDamage => attackDamage;

    public LayerMask AttackLayer => attackLayer;

    public virtual void Attack()
    {
        Debug.Log($"{gameObject.name} performed an attack.");
    }

    protected virtual bool IsFacingLeft()
    {
        return transform.localScale.x < 0f;
    }
}