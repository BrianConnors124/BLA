using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine<PlayerStateMachine.EPlayerState>
{
    private Rigidbody2D rb;
    public enum EPlayerState{idle, walking, attack, dash, jump, takingDamage, falling, doubleJump, slamAttack, dashAttack, stunned}

    public void Initialize(Player player, Rigidbody2D RB)
    {
        //States.Add();
        rb = GetComponent<Rigidbody2D>();
        States.Add(EPlayerState.idle, new Player_IdleState(EPlayerState.idle, player));
        States.Add(EPlayerState.walking, new Player_WalkingState(EPlayerState.walking, player, rb));
        States.Add(EPlayerState.jump, new Player_JumpState(EPlayerState.jump, player, rb));
        States.Add(EPlayerState.dash, new Player_DashState(EPlayerState.dash, player, rb));
        States.Add(EPlayerState.attack, new Player_AttackState(EPlayerState.attack, player));
        States.Add(EPlayerState.falling, new Player_FallingState(EPlayerState.falling, player, rb));
        States.Add(EPlayerState.doubleJump, new Player_DoubleJumpState(EPlayerState.doubleJump, player, rb));
        States.Add(EPlayerState.takingDamage, new Player_TakingDamage(EPlayerState.takingDamage, player));
        States.Add(EPlayerState.slamAttack, new Player_SlamAttack(EPlayerState.slamAttack, player));
        States.Add(EPlayerState.dashAttack, new Player_DashAttackState(EPlayerState.dashAttack, player));
        States.Add(EPlayerState.stunned, new Player_StunnedState(EPlayerState.stunned, player));
        CurrentState = States[EPlayerState.idle];
    }
    

    
}
