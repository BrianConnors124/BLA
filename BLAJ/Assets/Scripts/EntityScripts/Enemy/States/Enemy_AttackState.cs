using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy_AttackState : EnemyState
{
    public Enemy_AttackState(EnemyStateMachine.EEnemyState key, Enemy entity) : base(key, entity)
    {
        
    }
    
    public override void EnterState()
    { 
        enemy.ZeroVelocity();
        base.EnterState();
        enemy.canAttack = false;
    }
    
    
    public override void UpdateState() 
    { 
        base.UpdateState();
    }
    
    
    
    public override EnemyStateMachine.EEnemyState GetNextState()
    {
        if (enemy.takingDamage) return EnemyStateMachine.EEnemyState.takingDamage;
        return animEnded ? EnemyStateMachine.EEnemyState.idle : StateKey;
    }

    public override void ExitState()
    {
        base.ExitState();
        Timer.SetActionTimer("AttackCoolDown", enemy.primaryCD, () => enemy.canAttack = true);
    }
}

