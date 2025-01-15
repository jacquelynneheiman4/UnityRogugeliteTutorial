using UnityEngine;

public class RangedAttack : IAttackType
{
    private GameObject projectilePrefab;
    private Vector3 attackOffset;
    private Animator attackAnimator;

    public RangedAttack(GameObject prefab, Vector3 offset, Animator animator)
    {
        projectilePrefab = prefab;
        attackOffset = offset;
        attackAnimator = animator;
    }

    public void ExecuteAttack(Transform attacker, int damage, LayerMask targetLayer, bool isFacingLeft)
    {
        Vector3 firePosition = attacker.position + (isFacingLeft ? -attackOffset : attackOffset);

        GameObject projectile = GameObject.Instantiate(projectilePrefab, firePosition, Quaternion.identity);
        projectile.GetComponent<Projectile>()?.Launch(isFacingLeft ? Vector2.left : Vector2.right);

        attackAnimator?.SetTrigger("attack");
    }
}