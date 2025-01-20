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
        stateTimer = 0.3f;

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
        if (enemy.IsTouchingGround() && StateTimerDone()) return EnemyStateMachine.EEnemyState.stunned;
        
        return StateKey;
    }
    
    public override void ExitState()
    {
        base.ExitState();
        enemy.takingDamage = false;
    }
}
