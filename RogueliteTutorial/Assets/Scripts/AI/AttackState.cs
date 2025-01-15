using UnityEngine;

public class AttackState : State
{
    private ICombat combat;
    private IMovement movement;
    private Transform player;

    public AttackState(Transform owner, Transform player) : base(owner)
    {
        combat = owner.GetComponent<ICombat>();
        movement = owner.GetComponent<IMovement>();

        this.player = player;
    }

    public override void Enter()
    {
        
    }

    public override void Execute()
    {
        float directionToPlayer = player.position.x - owner.position.x;
        movement.SetFacingDirection(directionToPlayer < 0f);

        Vector3 movePosition = new Vector3(owner.position.x, player.position.y);
        float closeEnough = 0.1f;

        if (Vector3.Distance(owner.position, movePosition) > closeEnough)
        {
            movement.Move(movePosition);
        }

        combat.Attack();
    }

    public override void Exit()
    {
        
    }
}