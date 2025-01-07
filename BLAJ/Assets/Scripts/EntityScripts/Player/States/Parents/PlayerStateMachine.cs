using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine<PlayerStateMachine.EPlayerState>
{
    private Rigidbody2D rb;
    public enum EPlayerState{idle, walking, attack, dash, jump, takingDamage, falling, doubleJump, contactWithGround, transferGround}

    public void Initialize(Player player, Rigidbody2D RB)
    {
        //States.Add();
        rb = GetComponent<Rigidbody2D>();
        States.Add(EPlayerState.idle, new Player_IdleState(EPlayerState.idle, player));
        States.Add(EPlayerState.walking, new Player_WalkingState(EPlayerState.walking, player, rb));
        States.Add(EPlayerState.jump, new Player_JumpState(EPlayerState.jump, player, rb));
        States.Add(EPlayerState.dash, new Player_DashState(EPlayerState.dash, player, rb));
        States.Add(EPlayerState.attack, new Player_AttackState(EPlayerState.attack, player, rb));
        States.Add(EPlayerState.falling, new Player_FallingState(EPlayerState.falling, player, rb));
        States.Add(EPlayerState.doubleJump, new Player_DoubleJumpState(EPlayerState.doubleJump, player, rb));
        States.Add(EPlayerState.contactWithGround, new Player_MakingContactWithGround(EPlayerState.contactWithGround, player, rb));
        States.Add(EPlayerState.transferGround, new Player_TransferGroundState(EPlayerState.transferGround, player));
        States.Add(EPlayerState.takingDamage, new Player_TakingDamage(EPlayerState.takingDamage, player));
        CurrentState = States[EPlayerState.idle];
    }
    

    
}
