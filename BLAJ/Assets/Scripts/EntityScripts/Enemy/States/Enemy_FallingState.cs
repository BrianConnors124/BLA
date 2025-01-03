using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_FallingState : EnemyState
{
    public Enemy_FallingState(EnemyStateMachine.EEnemyState key, Enemy entity) : base(key, entity)
    {
        
    }

    public override void EnterState()
    {
        base.EnterState();
        Timer.SetTimer(jumpKey, .3f);
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override EnemyStateMachine.EEnemyState GetNextState()
    {
        if (enemy.takingDamage) return EnemyStateMachine.EEnemyState.takingDamage;
        if (enemy.IsTouchingGround()) return EnemyStateMachine.EEnemyState.idle;
        return StateKey;
    }

    public override void ExitState()
    {
        base.ExitState();
        //enemy.SetVelocity(new Vector2(rb.velocity.x, 0));
    }
}
