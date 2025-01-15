using System;

public class StateMachine
{
    private State currentState;

    public void ChangeState(State newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void Update()
    {
        currentState?.Execute();
    }

    public bool IsCurrentState<T>()
    {
        return currentState.GetType() == typeof(T);
    }
}