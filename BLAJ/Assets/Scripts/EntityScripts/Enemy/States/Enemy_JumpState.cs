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
    }

    public override EnemyStateMachine.EEnemyState GetNextState()
    {
        if (enemy.takingDamage) return EnemyStateMachine.EEnemyState.takingDamage;
        if (rb.velocityY < 0) return EnemyStateMachine.EEnemyState.falling;
        
        return StateKey;
    }

    public override void ExitState()
    {
        base.ExitState();
        enemy.SetVelocity(new Vector2(rb.velocity.x, 0));
    }
}
