using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_JumpState : EnemyState
{
    public Enemy_JumpState(EnemyStateMachine.EEnemyState key, Enemy entity) : base(key, entity)
    {
        
    }

    public override void EnterState()
    {
        base.EnterState();
        rb.velocity = new Vector2(enemy.movementSpeed * enemy.MovementDirection(), enemy.jumpHeight);
        
    }

    public override void UpdateState()
    {
        base.UpdateState();
        rb.velocity = new Vector2(enemy.movementSpeed * enemy.MovementDirection(), rb.velocityY);
    }

    public override EnemyStateMachine.EEnemyState GetNextState()
    {
        if (StateTimerDone())
        {
            if (!enemy.PlayerInRange()) return EnemyStateMachine.EEnemyState.retrieve;
            if (enemy.IsTouchingGround()) return EnemyStateMachine.EEnemyState.pursuit;
        }
        
        
        
        return StateKey;
    }
}
