using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_TakingDamage : EnemyState
{
    public Enemy_TakingDamage(EnemyStateMachine.EEnemyState key, Enemy entity) : base(key, entity)
    {
        
    }

    public override void EnterState()
    {
        base.EnterState();
        if (enemy.recentKnockBack > 0) stateTimer = 0.2f;
        enemy.SetVelocity(new Vector2(enemy.recentKnockBack * enemy.knockBackDirection, enemy.recentKnockBack));
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }
    

    public override EnemyStateMachine.EEnemyState GetNextState()
    {
        if (enemy.IsTouchingGround() && StateTimerDone()) return EnemyStateMachine.EEnemyState.stunned;
        
        return StateKey;
    }
    
    public override void ExitState()
    {
        base.ExitState();
        enemy.takingDamage = false;
        enemy.recentKnockBack = 0;
        enemy.recentStun = 0;
        enemy.recentDamage = 0;
    }
}
