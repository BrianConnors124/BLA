using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_StunnedState : EnemyState
{
    public Enemy_StunnedState(EnemyStateMachine.EEnemyState key, Enemy entity) : base(key, entity)
    {
        
    }

    public override void EnterState()
    {
        base.EnterState();
        enemy.ZeroVelocity();
        stateTimer = enemy.recentStun;
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }
    

    public override EnemyStateMachine.EEnemyState GetNextState()
    {
        return StateTimerDone() ? EnemyStateMachine.EEnemyState.idle : StateKey;
    }
    
    public override void ExitState()
    {
        base.ExitState();
        
    }
}
