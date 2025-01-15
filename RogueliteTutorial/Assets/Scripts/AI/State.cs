using UnityEngine;

public abstract class State
{
    protected Transform owner;

    public State(Transform owner)
    {
        this.owner = owner;
    }

    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();
}
