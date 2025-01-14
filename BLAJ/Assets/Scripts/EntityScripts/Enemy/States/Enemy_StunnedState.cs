using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_StunnedState : EnemyState
{
    public Enemy_StunnedState(EnemyStateMachine.EEnemyState key, Enemy entity) : base(key, entity)
    {
        
    }

    protected bool doneStartUp = false;

    public override void EnterState()
    {
        stateTimer = enemy.recentStun;
        base.EnterState();
        enemy.ZeroVelocity();
        doneStartUp = true;
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }
    

    public override EnemyStateMachine.EEnemyState GetNextState()
    {
        if(!doneStartUp) return StateKey;
        if (StateTimerDone()) return EnemyStateMachine.EEnemyState.idle;
        return StateKey;

    }
    
    public override void ExitState()
    {
        base.ExitState();
        enemy.recentKnockBack = 0;
        enemy.recentStun = 0;
        enemy.recentDamage = 0;
    }
}
