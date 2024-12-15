using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_RetrieveState : EnemyState
{
    public bool returned;
    public Enemy_RetrieveState(EnemyStateMachine.EEnemyState key, Enemy entity) : base(key, entity)
    {
        
    }

    public override void EnterState()
    {
        base.EnterState();
        returned = false;
        rb.velocity = new Vector2(enemy.movementSpeed * Line.LeftOrRight(enemy.transform.position.x, enemy.startingXPos), rb.velocity.y);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        rb.velocity = new Vector2(enemy.movementSpeed * Line.LeftOrRight(enemy.transform.position.x, enemy.startingXPos), rb.velocity.y);
        if (rb.velocityX > 0 && enemy.transform.position.x > enemy.startingXPos)
        {
            returned = true;
        }
        if (rb.velocityX < 0 && enemy.transform.position.x < enemy.startingXPos)
        {
            returned = true;
        }
    }

    public override EnemyStateMachine.EEnemyState GetNextState()
    {
        if (enemy.PlayerInRange())
            return EnemyStateMachine.EEnemyState.pursuit;
        return StateKey;
    }
}
