using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_RetrieveState : EnemyState
{
    public Enemy_RetrieveState(EnemyStateMachine.EEnemyState key, Enemy entity) : base(key, entity)
    {
        
    }

    private bool pathObst;
    public override void EnterState()
    {
        base.EnterState();
        rb.velocity = new Vector2(enemy.movementSpeed * Line.LeftOrRight(enemy.transform.position.x, enemy.startingXPos), rb.velocity.y);
        stateTimer = 1;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(!pathObst) rb.velocity = new Vector2(enemy.movementSpeed * Line.LeftOrRight(enemy.transform.position.x, enemy.startingXPos), rb.velocity.y);
        if (!enemy.ThereIsAFloor() || enemy.ObjectTooHigh())
        {
            pathObst = true;
            enemy.Move(enemy.movementSpeed * -Line.LeftOrRight(enemy.transform.position.x, enemy.startingXPos), rb.velocity.y);
            if(!Timer.TimerActive("FindingNewOrigin"))Timer.SetActionTimer("FindingNewOrigin", Random.Range(.3f,1), ()=> enemy.origin = enemy.transform.position);
        }

        if (pathObst && enemy.DetectsObjectForward())
        {
            enemy.origin = enemy.transform.position;
            Timer.RemoveTimer("FindingNewOrigin");
        }
    }

    public override EnemyStateMachine.EEnemyState GetNextState()
    {
        
        if (enemy.takingDamage) return EnemyStateMachine.EEnemyState.takingDamage;
        if (enemy.DetectsObjectForward() && Timer.TimerDone(jumpKey) && !enemy.ObjectTooHigh() && enemy.canJump) return EnemyStateMachine.EEnemyState.jump;
        if (enemy.returned || (pathObst && Timer.TimerDone("FindingNewOrigin"))) return EnemyStateMachine.EEnemyState.idle;
        if (rb.velocity.y < -0.1f) return EnemyStateMachine.EEnemyState.falling;
        
        if (!enemy.PlayerOutOfSight() && enemy.playerInPursuitRange) return EnemyStateMachine.EEnemyState.pursuit;
        
        

        return StateKey;
    }

    public override void ExitState()
    {
        base.ExitState();
        enemy.ZeroVelocity();
        pathObst = false;
    }
}
