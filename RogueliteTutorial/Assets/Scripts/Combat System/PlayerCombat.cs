using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : Combat
{
    private Animator meleeAnimator;

    private void Awake()
    {
        meleeAnimator = transform.Find("Sword").GetComponent<Animator>();
    }

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
        meleeAnimator.SetTrigger("attack");
    }

    private bool IsFacingLeft()
    {
        return transform.localScale.x < 0f;
    }
}