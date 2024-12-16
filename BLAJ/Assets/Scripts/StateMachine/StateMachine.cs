using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine<EState> : MonoBehaviour where EState : Enum
{
    public Dictionary<EState, State<EState>> States = new Dictionary<EState, State<EState>>();

    public State<EState> CurrentState;
    public State<EState> LastState;
    protected bool animEnded;
    

    public State<EState> GetCurrentState() => CurrentState;
    public State<EState> GetLastState() => LastState;

    protected EState NextStateKey;

    protected bool IsTransitioningState = false;

    public Action<State<EState>> StateChange;

    private void Start()
    {
        CurrentState.EnterState();
    }

    private void Update()
    {
        NextStateKey = CurrentState.GetNextState();
        
        if(!IsTransitioningState && NextStateKey.Equals(CurrentState.StateKey))
            CurrentState.UpdateState();
        else if (!IsTransitioningState)
            TransitionToState(NextStateKey);
    }

    private void TransitionToState(EState stateKey)
    {
        IsTransitioningState = true;
        LastState = CurrentState;
        CurrentState.ExitState();
        CurrentState = States[stateKey];
        CurrentState.EnterState();
        StateChange?.Invoke(CurrentState);
        IsTransitioningState = false;
    }
    
    public virtual void AnimEndTrigger()
    {
        CurrentState.AnimEndTrigger();
    }
}
