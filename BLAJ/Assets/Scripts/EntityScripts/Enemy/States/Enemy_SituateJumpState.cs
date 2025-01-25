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
        stateTimer = 1;
        rb.velocity = new Vector2(enemy.movementSpeed * -Line.LeftOrRight(enemy.transform.position.x, enemy.DetectsObjectForward().collider.transform.position.x), rb.velocity.y);
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override EnemyStateMachine.EEnemyState GetNextState()
    {
        if (enemy.takingDamage) return EnemyStateMachine.EEnemyState.takingDamage;
        if (!enemy.DetectsObjectBackwards() && playerLost) return EnemyStateMachine.EEnemyState.retrieve;
        if (!enemy.DetectsObjectBackwards()) return enemy.GetComponent<EnemyStateMachine>().LastState.StateKey;
        if (StateTimerDone()) return EnemyStateMachine.EEnemyState.retrieve;
        
        
        return StateKey;
    }
}
