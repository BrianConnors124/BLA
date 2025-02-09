using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_TakingDamage : EnemyState
{
    public Enemy_TakingDamage(EnemyStateMachine.EEnemyState key, Enemy entity) : base(key, entity)
    {
        
    }

    private bool doneLoading;

    public override void EnterState()
    {
        base.EnterState();
        stateTimer = 0.2f;
        enemy.sprite.color = Color.red;
        Timer.SetActionTimer("ChangeColorBack", 0.2f, () => enemy.sprite.color = Color.white);

        if (enemy.knockBackDirection == -100)
        {
            enemy.Move(enemy.recentKnockBack * -enemy.PlayerDirection(), enemy.recentKnockBack);
        }
        else if (enemy.knockBackDirection == -200)
        {
            enemy.Move(0, enemy.recentKnockBack);
        } else
        {
            enemy.Move(enemy.recentKnockBack * enemy.knockBackDirection, enemy.recentKnockBack);
        }
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }
    

    public override EnemyStateMachine.EEnemyState GetNextState()
    {
        if (enemy.dead) return EnemyStateMachine.EEnemyState.dying;
        if (enemy.IsTouchingGround() && enemy.canBeStunned && StateTimerDone()) return EnemyStateMachine.EEnemyState.stunned;
        if (!enemy.canBeStunned) return EnemyStateMachine.EEnemyState.pursuit;
        
        return StateKey;
    }
    
    public override void ExitState()
    {
        base.ExitState();
        enemy.takingDamage = false;
        enemy.recentKnockBack = 0;
    }
}
