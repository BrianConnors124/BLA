using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_IdleState : EnemyState
{
    public Enemy_IdleState(EnemyStateMachine.EEnemyState key, Enemy entity) : base(key, entity)
    {
        
    }

    public override void EnterState()
    {
        base.EnterState();
        enemy.ZeroVelocity();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override EnemyStateMachine.EEnemyState GetNextState()
    {
        if (animEnded) return EnemyStateMachine.EEnemyState.transferGround;
        return StateKey;
    }
}
