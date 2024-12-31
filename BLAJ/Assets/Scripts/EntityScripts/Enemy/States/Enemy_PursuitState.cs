using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_PursuitState : EnemyState
{
    public Enemy_PursuitState(EnemyStateMachine.EEnemyState key, Enemy entity) : base(key, entity)
    {
        
    }
    
    
    private string code = "key";

    public override void EnterState()
    {
        if (!enemy.PlayerOutOfSight()) Timer.RemoveActionTimer(code);
        enemyLost = false;
        base.EnterState();
        rb.velocity = new Vector2(enemy.movementSpeed * Line.LeftOrRight(enemy.transform.position.x, enemy.player.transform.position.x), rb.velocity.y);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(enemy.PlayerOutOfSight()) Timer.SetActionTimer(code, 3, () => enemyLost = true);
        if (!enemy.PlayerOutOfSight()) Timer.RemoveActionTimer(code);
        rb.velocity = new Vector2(enemy.movementSpeed * Line.LeftOrRight(enemy.transform.position.x, enemy.player.transform.position.x), rb.velocity.y);
    }

    public override EnemyStateMachine.EEnemyState GetNextState()
    {
        if (enemy.player == null) return EnemyStateMachine.EEnemyState.retrieve;
        if (enemy.ObjectForwardTooClose()) return EnemyStateMachine.EEnemyState.situateJump;
        if (enemy.DetectsObjectForward() && Timer.TimerDone(jumpKey) && rb.velocityX != 0) return EnemyStateMachine.EEnemyState.jump;
        if (rb.velocityY < 0) return EnemyStateMachine.EEnemyState.falling;
        if (animEnded) return EnemyStateMachine.EEnemyState.transferGround;
        if (enemyLost) return EnemyStateMachine.EEnemyState.idle;
        
        
        return StateKey;
    }
}
