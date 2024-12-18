using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_TransferState : EnemyState
{
    public Enemy_TransferState(EnemyStateMachine.EEnemyState key, Enemy entity) : base(key, entity)
    {
        
    }

    public override EnemyStateMachine.EEnemyState GetNextState()
    {
        
        return base.GetNextState();
    }
}
