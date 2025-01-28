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
        if (!enemy.returned && playerLost && (enemy.PlayerOutOfSight() ||!enemy.playerInPursuitRange)) return EnemyStateMachine.EEnemyState.retrieve;
        if (enemy.playerInPursuitRange && !enemy.PlayerOutOfSight() && !enemy.DetectsObjectForward() && enemy.ThereIsAFloor() && !enemy.SimilarX) return EnemyStateMachine.EEnemyState.pursuit;
        if (enemy.playerInMeleeRange && enemy.canAttack && !enemy.PlayerOutOfSight()) return EnemyStateMachine.EEnemyState.attack;
        return StateKey;
    }
}
