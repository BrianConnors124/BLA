using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_RetrieveState : EnemyState
{
    public Enemy_RetrieveState(EnemyStateMachine.EEnemyState key, Enemy entity) : base(key, entity)
    {
        
    }

    public override void EnterState()
    {
        base.EnterState();
        rb.velocity = new Vector2(enemy.movementSpeed * Line.LeftOrRight(enemy.transform.position.x, enemy.startingXPos), rb.velocity.y);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        rb.velocity = new Vector2(enemy.movementSpeed * Line.LeftOrRight(enemy.transform.position.x, enemy.startingXPos), rb.velocity.y);
        
    }

    public override EnemyStateMachine.EEnemyState GetNextState()
    {
        if (enemy.takingDamage) return EnemyStateMachine.EEnemyState.takingDamage;
        if (enemy.DetectsObjectForward() && Timer.TimerDone(jumpKey) && !enemy.ObjectTooHigh()) return EnemyStateMachine.EEnemyState.jump;
        if (enemy.returned) return EnemyStateMachine.EEnemyState.idle;
        if (!enemy.PlayerOutOfSight() && enemy.playerInPursuitRange) return EnemyStateMachine.EEnemyState.pursuit;

        return StateKey;
    }

    public override void ExitState()
    {
        base.ExitState();
        enemy.ZeroVelocity();
    }
}
