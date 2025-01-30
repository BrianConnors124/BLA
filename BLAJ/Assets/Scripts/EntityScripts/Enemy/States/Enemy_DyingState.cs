using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDyingState : EnemyState
{
    public EnemyDyingState(EnemyStateMachine.EEnemyState key, Enemy entity) : base(key, entity)
    {
        
    }

    public override EnemyStateMachine.EEnemyState GetNextState()
    {
        return StateKey;
    }
}
