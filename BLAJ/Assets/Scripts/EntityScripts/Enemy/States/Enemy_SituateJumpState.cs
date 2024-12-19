using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SituateJumpState : EnemyState
{
    public Enemy_SituateJumpState(EnemyStateMachine.EEnemyState key, Enemy entity) : base(key, entity)
    {
        
    }

    public override void EnterState()
    {
        base.EnterState();
        rb.velocity = new Vector2(enemy.movementSpeed * -Line.LeftOrRight(enemy.transform.position.x, enemy.player.transform.position.x), rb.velocity.y);
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override EnemyStateMachine.EEnemyState GetNextState()
    {
        if (!enemy.DetectsObjectForward()) return EnemyStateMachine.EEnemyState.pursuit;
        return StateKey;
    }
}
