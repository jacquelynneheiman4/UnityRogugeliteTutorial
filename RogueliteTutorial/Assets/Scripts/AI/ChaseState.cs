using UnityEngine;

public class ChaseState : State
{
    private Transform player; 
    private IMovement movement;

    public ChaseState(Transform owner, Transform player) : base(owner)
    {
        movement = owner.GetComponent<IMovement>();
        this.player = player;
    }

    public override void Enter()
    {
    
    }

    public override void Execute()
    {
        movement.Move(player.position);
    }

    public override void Exit()
    {
        
    }
}