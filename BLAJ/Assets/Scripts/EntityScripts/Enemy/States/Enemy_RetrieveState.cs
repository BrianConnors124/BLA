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
        if (!enemy.Returned)
        {
            rb.velocity = new Vector2(enemy.movementSpeed * Line.LeftOrRight(enemy.transform.position.x, enemy.startingXPos), rb.velocity.y);
            if (rb.velocityX > 0 && enemy.transform.position.x > enemy.startingXPos)
            {
                enemy.Returned = true;
            }
            if (rb.velocityX < 0 && enemy.transform.position.x < enemy.startingXPos)
            {
                enemy.Returned = true;
            }
        }
        else
        {
            enemy.ZeroVelocity();
        }
        
    }

    public override EnemyStateMachine.EEnemyState GetNextState()
    {
        if (enemy.PlayerInRange()) return EnemyStateMachine.EEnemyState.idle;
        if (enemy.DetectsObjectForward()) return EnemyStateMachine.EEnemyState.jump;
        return StateKey;
    }
}
