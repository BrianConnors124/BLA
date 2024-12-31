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
        if (enemy.player == null) return EnemyStateMachine.EEnemyState.retrieve;
        if (enemy.DetectsObjectForward() && Timer.TimerDone(jumpKey) && rb.velocityX != 0) return EnemyStateMachine.EEnemyState.jump;
        if (rb.velocityY < 0) return EnemyStateMachine.EEnemyState.falling;
        if (animEnded) return EnemyStateMachine.EEnemyState.transferGround;
        
        return StateKey;
    }
}
