using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_PursuitState : EnemyState
{
    public Enemy_PursuitState(EnemyStateMachine.EEnemyState key, Enemy entity) : base(key, entity)
    {
        
    }
    
    
    private string code = "Player Lost";

    public override void EnterState()
    {
        Timer.RemoveActionTimer(code);
        playerLost = false;
        base.EnterState();
        rb.velocity = new Vector2(enemy.movementSpeed * Line.LeftOrRight(enemy.transform.position.x, enemy.player.transform.position.x), rb.velocity.y);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if((enemy.PlayerOutOfSight() || !enemy.playerInPursuitRange) && !Timer.TimerActive(code)) Timer.SetActionTimer(code, 1, () => playerLost = true);
        if (!enemy.PlayerOutOfSight()) Timer.RemoveActionTimer(code);
        rb.velocity = new Vector2(enemy.movementSpeed * Line.LeftOrRight(enemy.transform.position.x, enemy.player.transform.position.x), rb.velocity.y);
    }

    public override EnemyStateMachine.EEnemyState GetNextState()
    {
        if (enemy.takingDamage) return EnemyStateMachine.EEnemyState.takingDamage;
        if (playerLost) return EnemyStateMachine.EEnemyState.retrieve;
        if (enemy.ObjectForwardTooClose()) return EnemyStateMachine.EEnemyState.situateJump;
        if (rb.velocityY < 0) return EnemyStateMachine.EEnemyState.falling;
        if (playerLost || enemy.playerInAttackRange && !enemy.canAttack || !enemy.ThereIsAFloor()) return EnemyStateMachine.EEnemyState.idle;
        if (enemy.playerInAttackRange && enemy.canAttack) return EnemyStateMachine.EEnemyState.attack;
        if (enemy.DetectsObjectForward() && Timer.TimerDone(jumpKey) && !enemy.ObjectTooHigh()) return EnemyStateMachine.EEnemyState.jump;
        
        
        
        return StateKey;
    }
}
