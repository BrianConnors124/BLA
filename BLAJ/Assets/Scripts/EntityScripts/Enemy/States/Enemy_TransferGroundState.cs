using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_TransferGroundState : EnemyState
{
    public Enemy_TransferGroundState(EnemyStateMachine.EEnemyState key, Enemy entity) : base(key, entity)
    {
        
    }

    public override EnemyStateMachine.EEnemyState GetNextState()
    {
        if (!enemy.PlayerInRange() && enemy.Returned) return EnemyStateMachine.EEnemyState.idle;
        if (!enemy.PlayerInRange()) return EnemyStateMachine.EEnemyState.retrieve;
        if (enemy.PlayerInRange() && !enemy.PlayerOutOfSight()) return EnemyStateMachine.EEnemyState.pursuit;
        if (rb.velocityY < 0) return EnemyStateMachine.EEnemyState.falling;
        
        return base.GetNextState();
    }
}
