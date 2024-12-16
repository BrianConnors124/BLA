using System;
using UnityEngine;

public abstract class State<EState> where EState : Enum
{
    protected State(EState key)
    {
        StateKey = key;
    }
    
    public EState StateKey { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Animator Anim { get; private set; }

    protected float stateTimer;
    protected bool animEnded;

    public virtual void EnterState()
    {
        Debug.Log(StateKey);
        animEnded = false;
    }

    public virtual void ExitState()
    {
    }

    public virtual void UpdateState()
    {
        stateTimer -= Time.deltaTime;
    }
    public abstract EState GetNextState();

    public virtual void AnimEndTrigger()
    {
        animEnded = true;
    }

    protected bool StateTimerDone() => stateTimer < 0;
    
}