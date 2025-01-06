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
        base.EnterState();
        enemy.canAttack = false;
        enemy.ZeroVelocity();
        var a = BoxCastDrawer.BoxCastAndDraw(new Vector2(enemy.transform.position.x +( enemy.reach * enemy.MovementDirection()), enemy.transform.position.y),new Vector2(enemy.transform.localScale.x/2,enemy.transform.localScale.y), 0, new Vector2(enemy.MovementDirection(), 0),0, LayerMask.GetMask("Player"), 0.3f);
        if(a) a.collider.GetComponent<Player>().DamageDelt(enemy.damage, enemy.knockback,enemy.stun);
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

