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
        stateTimer = .2f;
        enemy.longRangeAttackReady = false;
        ObjectPuller.PullProjectile(enemy.objPuller.enemyProjectilesFromAir, new Vector3(enemy.player.transform.position.x, enemy.player.transform.position.y + enemy.transform.localScale.y * 3), enemy.player.transform.position);
    }

    public override EnemyStateMachine.EEnemyState GetNextState()
    {
        if (StateTimerDone()) return EnemyStateMachine.EEnemyState.idle;
        return StateKey;
    }


    public override void ExitState()
    {
        base.ExitState();
        Timer.SetActionTimer("LongRangeAttackCoolDown", enemy.primaryCD, () => enemy.longRangeAttackReady = true);
    }
}
