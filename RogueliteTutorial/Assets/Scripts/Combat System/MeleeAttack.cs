using UnityEngine;

public class MeleeAttack : IAttackType
{
    private float attackRange;
    private Vector2 attackOffset;

    public MeleeAttack(float range, Vector2 offset)
    {
        attackRange = range;
        attackOffset = offset;
    }

    public void ExecuteAttack(Transform attacker, int damage, LayerMask targetLayer, bool isFacingLeft)
    {
        Vector2 attackPosition = (Vector2)attacker.position + (isFacingLeft ? -attackOffset : attackOffset);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPosition, attackRange, targetLayer);

        foreach (var enemy in hitEnemies)
        {
            enemy.GetComponent<IHealth>()?.TakeDamage(damage);
        }

        DebugAttackPosition(attackPosition, isFacingLeft ? Vector2.left : Vector2.right, attackRange);
    }

    private void DebugAttackPosition(Vector3 origin, Vector2 direction, float radius)
    {
        int segments = 36;
        float step = 360 / segments;

        Vector3 start = Quaternion.Euler(0, 0, -360 / 2) * direction * radius;

        for (int i = 1; i <= segments; i++)
        {
            Vector3 end = Quaternion.Euler(0, 0, -360 / 2 + step * i) * direction * radius;
            Debug.DrawLine(origin + start, origin + end, Color.red, 5f);
            start = end;
        }
    }
}