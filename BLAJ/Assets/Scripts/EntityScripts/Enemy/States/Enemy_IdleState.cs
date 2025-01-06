using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_IdleState : EnemyState
{
    public Enemy_IdleState(EnemyStateMachine.EEnemyState key, Enemy entity) : base(key, entity)
    {
        
    }

    public override void EnterState()
    {
        base.EnterState();
        enemy.ZeroVelocity();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(enemy.player != null) enemy.PlayerDirection();
    }

    public override EnemyStateMachine.EEnemyState GetNextState()
    {
        if (enemy.takingDamage) return EnemyStateMachine.EEnemyState.takingDamage;
        if (enemy.ObjectForwardTooClose()) return EnemyStateMachine.EEnemyState.situateJump;
        if (rb.velocityY < 0) return EnemyStateMachine.EEnemyState.falling;
        if (!enemy.playerInPursuitRange && !enemy.returned) return EnemyStateMachine.EEnemyState.retrieve;
        if (enemy.playerInPursuitRange && !enemy.playerInAttackRange && !enemy.PlayerOutOfSight()) return EnemyStateMachine.EEnemyState.pursuit;
        if (enemy.playerInAttackRange && enemy.canAttack) return EnemyStateMachine.EEnemyState.attack;
        return StateKey;
    }
}
