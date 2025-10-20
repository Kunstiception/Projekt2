using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class PlayerStateMachine
{
    public IState CurrentState { get; private set; }

    public WalkState walkState;
    public IdleState idleState;
    public InteractionState interactionState;

    public void Initialize(IState startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
    }

    public void TransitionTo(IState nextState)
    {
        CurrentState.Exit();
        CurrentState = nextState;
        nextState.Enter();
    }

    public void Execute()
    {
        if(CurrentState != null)
        {
            CurrentState.Execute();
        }
    }
}
