using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_RetreatState : EnemyState
{
    public Enemy_RetreatState(EnemyStateMachine.EEnemyState key, Enemy entity) : base(key, entity)
    {
        
    }

    public override void EnterState()
    {
        base.EnterState();
        
        rb.velocity = new Vector2(enemy.movementSpeed * -Line.LeftOrRight(enemy.transform.position.x, enemy.player.transform.position.x), rb.velocity.y);
        if(!Timer.TimerActive("retreat"))Timer.SetTimer("retreat", .8f);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        rb.velocity = new Vector2(enemy.movementSpeed * -Line.LeftOrRight(enemy.transform.position.x, enemy.player.transform.position.x), rb.velocity.y);
        
    }

    public override EnemyStateMachine.EEnemyState GetNextState()
    {
        if (enemy.takingDamage) return EnemyStateMachine.EEnemyState.takingDamage;
        
        if (rb.velocity.y < -0.1f) return EnemyStateMachine.EEnemyState.falling;
        if (enemy.DetectsObjectForward()) return EnemyStateMachine.EEnemyState.pursuit;
        if (!enemy.ThereIsAFloor()) return EnemyStateMachine.EEnemyState.pursuit;
        if (Timer.TimerDone("retreat")) return EnemyStateMachine.EEnemyState.pursuit;
        
        

        return StateKey;
    }

    public override void ExitState()
    {
        base.ExitState();
        Timer.RemoveTimer("retreat");
        enemy.ZeroVelocity();
    }
}
