using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_LongRangeAttackState : EnemyState
{
    public Enemy_LongRangeAttackState(EnemyStateMachine.EEnemyState key, Enemy entity) : base(key, entity)
    {
        
    }
    
    

    public override void EnterState()
    {
        base.EnterState();
        enemy.ZeroVelocity();
        stateTimer = .2f;
        enemy.longRangeAttackReady = false;
    }

    public override EnemyStateMachine.EEnemyState GetNextState()
    {
        if (animEnded) return EnemyStateMachine.EEnemyState.idle;
        return StateKey;
    }


    public override void ExitState()
    {
        base.ExitState();
        Timer.SetActionTimer("LongRangeAttackCoolDown", enemy.primaryCD * 2, () => enemy.longRangeAttackReady = true);
    }
}
