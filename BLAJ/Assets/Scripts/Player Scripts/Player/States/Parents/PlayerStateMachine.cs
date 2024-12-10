using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine<PlayerStateMachine.EPlayerState>
{
    public enum EPlayerState{idle, walking, attack, dash, jump, block, falling}

    public void Initialize(Player player, Rigidbody2D RB, Animator Anim)
    {
        //States.Add();
        States.Add(EPlayerState.idle, new IdleState(EPlayerState.idle, player));
        States.Add(EPlayerState.walking, new WalkingState(EPlayerState.walking, player, GetComponent<Rigidbody2D>()));
        States.Add(EPlayerState.jump, new JumpState(EPlayerState.jump, player, GetComponent<Rigidbody2D>()));
        States.Add(EPlayerState.dash, new DashState(EPlayerState.dash, player, GetComponent<Rigidbody2D>()));
        States.Add(EPlayerState.attack, new DashState(EPlayerState.attack, player, GetComponent<Rigidbody2D>()));
        CurrentState = States[EPlayerState.idle];
    }
    

    
}