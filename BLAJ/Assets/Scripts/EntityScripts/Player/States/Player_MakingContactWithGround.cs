using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MakingContactWithGround : PlayerState
{
    
    public Player_MakingContactWithGround(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D RB) : base(key, entity)
    {
        
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (player.Grounded()) return PlayerStateMachine.EPlayerState.transferGround; 
        
        return StateKey;
    }
}
