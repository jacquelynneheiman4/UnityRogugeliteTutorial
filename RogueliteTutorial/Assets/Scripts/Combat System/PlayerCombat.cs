using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : Combat
{
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Attack();
        }
    }

    public override void Attack()
    {
        attackType.ExecuteAttack(transform, AttackDamage, AttackLayer, IsFacingLeft());
    }

    private bool IsFacingLeft()
    {
        return transform.localScale.x < 0f;
    }
}