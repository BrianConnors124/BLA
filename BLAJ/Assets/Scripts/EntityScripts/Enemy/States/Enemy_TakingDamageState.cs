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
        enemy.SetVelocity(new Vector2(enemy.recentKnockBack * - enemy.PlayerDirection(), enemy.recentKnockBack));
        stateTimer = 0.2f;
        if (enemy.recentStun > 0) stateTimer = enemy.recentStun;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(enemy.IsTouchingGround() && enemy.recentStun - stateTimer >= .2f) enemy.ZeroVelocity();
    }
    

    public override EnemyStateMachine.EEnemyState GetNextState()
    {
        if (enemy.IsTouchingGround() && StateTimerDone()) return EnemyStateMachine.EEnemyState.idle;
        
        return StateKey;
    }
    
    public override void ExitState()
    {
        base.ExitState();
        enemy.takingDamage = false;
    }
}
