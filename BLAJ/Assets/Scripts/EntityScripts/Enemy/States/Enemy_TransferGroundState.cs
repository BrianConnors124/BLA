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
        if (enemy.DetectsObjectForward() && rb.velocityX == 0) return EnemyStateMachine.EEnemyState.situateJump;
        if (rb.velocityY < 0) return EnemyStateMachine.EEnemyState.falling;
        if (!enemy.PlayerInRange() && enemy.Returned) return EnemyStateMachine.EEnemyState.idle;
        if (!enemy.PlayerInRange()) return EnemyStateMachine.EEnemyState.retrieve;
        
        
        
        
        return EnemyStateMachine.EEnemyState.pursuit;
    }
}
