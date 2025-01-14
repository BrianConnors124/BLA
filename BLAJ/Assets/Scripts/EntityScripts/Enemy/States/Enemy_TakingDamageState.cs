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
        enemy.sprite.color = Color.red;
        Timer.SetActionTimer("ChangeColorBack", 0.2f, () => enemy.sprite.color = Color.white);
        stateTimer = 0.1f;
        enemy.SetVelocity(enemy.knockBackDirection == -100
            ? new Vector2(enemy.recentKnockBack * -enemy.PlayerDirection(), enemy.recentKnockBack)
            : new Vector2(enemy.recentKnockBack * enemy.knockBackDirection, enemy.recentKnockBack));
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
    }
}
