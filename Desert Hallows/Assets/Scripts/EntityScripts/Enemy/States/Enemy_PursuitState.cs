using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_PursuitState : EnemyState
{
    public Enemy_PursuitState(EnemyStateMachine.EEnemyState key, Enemy entity) : base(key, entity)
    {
        
    }

    public override void EnterState()
    {
        base.EnterState();
        rb.velocity = new Vector2(enemy.movementSpeed * Line.LeftOrRight(enemy.transform.position.x, enemy.player.transform.position.x), rb.velocity.y);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        rb.velocity = new Vector2(enemy.movementSpeed * Line.LeftOrRight(enemy.transform.position.x, enemy.player.transform.position.x), rb.velocity.y);
    }

    public override EnemyStateMachine.EEnemyState GetNextState()
    {
        if (enemy.takingDamage) return EnemyStateMachine.EEnemyState.takingDamage;
        if ((enemy.playerInMeleeRange && !enemy.canAttack) || !enemy.ThereIsAFloor() || enemy.ObjectTooHigh() || !enemy.playerInPursuitRange || enemy.SimilarX) return EnemyStateMachine.EEnemyState.idle;
        if (enemy.hasALongRangeAttack && enemy.playerInLongRange && enemy.longRangeAttackReady && !enemy.playerInMeleeRange) return EnemyStateMachine.EEnemyState.longRange;
        if (enemy.DetectsObjectForward() && Timer.TimerDone(jumpKey) && !enemy.ObjectTooHigh()) return EnemyStateMachine.EEnemyState.jump;
        if (playerLost) return EnemyStateMachine.EEnemyState.retrieve;
        if (enemy.ObjectForwardTooClose() && !enemy.ObjectTooHigh()) return EnemyStateMachine.EEnemyState.situateJump;
        if (rb.velocityY < -0.1f) return EnemyStateMachine.EEnemyState.falling;
        if (enemy.playerInMeleeRange && enemy.canAttack && enemy.player.GetComponent<Player>().IsTouchingGround() && enemy.hasAMeleAttack) return EnemyStateMachine.EEnemyState.attack;
        
        
        
        return StateKey;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
