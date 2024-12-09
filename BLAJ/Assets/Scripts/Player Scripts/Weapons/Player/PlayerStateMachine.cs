using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine<PlayerStateMachine.EPlayerState>
{
    public enum EPlayerState{idle, walking, attack, dash, jump, block, falling}

    public void Initialize(Player player, Rigidbody2D RB, Animator Anim)
    {
        //States.Add();
        CurrentState = States[EPlayerState.idle];
    }
    

    
}
