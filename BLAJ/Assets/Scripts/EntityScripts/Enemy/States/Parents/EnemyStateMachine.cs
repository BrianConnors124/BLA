using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStateMachine : StateMachine<EnemyStateMachine.EEnemyState>
{
    public enum EEnemyState{retrieve, pursuit, attack, jump, falling, idle, situateJump, takingDamage, stunned, longRange}

    public void Initialize(Enemy enemy, Rigidbody2D rb)
    {
        States.Add(EEnemyState.retrieve, new Enemy_RetrieveState(EEnemyState.retrieve, enemy));
        States.Add(EEnemyState.pursuit, new Enemy_PursuitState(EEnemyState.pursuit, enemy));
        States.Add(EEnemyState.jump, new Enemy_JumpState(EEnemyState.jump, enemy));
        States.Add(EEnemyState.idle, new Enemy_IdleState(EEnemyState.idle,enemy));
        States.Add(EEnemyState.falling, new Enemy_FallingState(EEnemyState.falling, enemy));
        States.Add(EEnemyState.situateJump, new Enemy_SituateJumpState(EEnemyState.situateJump, enemy));
        States.Add(EEnemyState.attack, new Enemy_AttackState(EEnemyState.attack, enemy));
        States.Add(EEnemyState.takingDamage, new Enemy_TakingDamage(EEnemyState.takingDamage, enemy));
        States.Add(EEnemyState.stunned, new Enemy_StunnedState(EEnemyState.stunned, enemy));
        States.Add(EEnemyState.longRange, new Enemy_LongRangeAttackState(EEnemyState.longRange, enemy));
        CurrentState = States[EEnemyState.idle];
    }
}
