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
        stateTimer = 0.2f;
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override EnemyStateMachine.EEnemyState GetNextState()
    {
        if (enemy.takingDamage) return EnemyStateMachine.EEnemyState.takingDamage;
        return enemy.IsTouchingGround() && StateTimerDone() ? enemyStateMachine.GetLastState().StateKey : StateKey;
    }

    public override void ExitState()
    {
        base.ExitState();
        Timer.SetTimer(jumpKey, .2f);
        enemy.Move(rb.velocity.x, 0);
    }
}
