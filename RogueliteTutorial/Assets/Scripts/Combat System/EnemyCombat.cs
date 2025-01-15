using UnityEngine;

public class EnemyCombat : Combat
{
    public bool useMeleeAttack;
    public GameObject projectilePrefab;

    private void Awake()
    {
        if (useMeleeAttack)
        {
            attackType = new MeleeAttack(AttackRange, new Vector2(0.5f, 0), transform.Find("Sword").GetComponent<Animator>());
        }
        else
        {
            attackType = new RangedAttack(projectilePrefab, new Vector2(0.5f, 0), transform.Find("Bow").GetComponent<Animator>());
        }
    }

    public override void Attack()
    {
        float currentTime = Time.time;

        if (currentTime - lastAttackTime >= attackDelay)
        {
            attackType?.ExecuteAttack(transform, AttackDamage, AttackLayer, IsFacingLeft());
            lastAttackTime = currentTime;
        }
    }
}