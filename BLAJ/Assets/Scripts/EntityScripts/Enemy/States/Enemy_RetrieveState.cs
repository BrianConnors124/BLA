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
        if (enemy.DetectsObjectForward() && Timer.TimerDone(jumpKey)) return EnemyStateMachine.EEnemyState.jump;
        if (enemy.Returned || enemy.PlayerInRange()) return EnemyStateMachine.EEnemyState.transferGround;
        
        return StateKey;
    }

    public override void ExitState()
    {
        base.ExitState();
        enemy.ZeroVelocity();
    }
}
