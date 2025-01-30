using System;
using Unity.VisualScripting;
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
    protected bool startAttack;

    public virtual void EnterState()
    {
        animEnded = false;
    }

    public virtual void ExitState()
    {
        startAttack = false;
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

    public virtual void DoAttack()
    {
     throw new NotImplementedException();
    }

    public virtual void Die()
    {
        
    }
    

    public virtual void FacePlayer()
    {
        throw new NotImplementedException();
    }

    protected bool StateTimerDone() => stateTimer < 0;
    
    
    
}