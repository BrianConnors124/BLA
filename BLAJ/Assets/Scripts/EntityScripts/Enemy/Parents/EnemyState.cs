using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : State<EnemyStateMachine.EEnemyState>
{
    public EnemyState(EnemyStateMachine.EEnemyState key, Enemy entity) : base(key)
    {
        
    }

    public override void EnterState()
    {
        base.EnterState();
        
    }
    public override EnemyStateMachine.EEnemyState GetNextState()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        
    }
}
