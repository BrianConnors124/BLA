using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDyingState : EnemyState
{
    public EnemyDyingState(EnemyStateMachine.EEnemyState key, Enemy entity) : base(key, entity)
    {
        
    }

    public override void EnterState()
    {
        base.EnterState();
        enemy.ZeroVelocity();
    }

    public override EnemyStateMachine.EEnemyState GetNextState()
    {
        if (animEnded)
        {
            enemy.DestroyGameObject();
        }
        return StateKey;
    }
}
