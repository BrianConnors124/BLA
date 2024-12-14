using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine<EnemyStateMachine.EEnemyState>
{
    public enum EEnemyState{idle, walking, attack, jump, falling}

    public void Initialize(Enemy enemy, Rigidbody2D RB)
    {
        
        CurrentState = States[EEnemyState.idle];
    }
}
