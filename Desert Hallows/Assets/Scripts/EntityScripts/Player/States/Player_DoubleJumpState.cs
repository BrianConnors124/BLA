using System.Collections.Generic;
using UnityEngine;

public class Player_DoubleJumpState : PlayerState
{
    
    
    public Player_DoubleJumpState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D RB) : base(key, entity)
    {
       
    }

    public override void EnterState()
    {
        base.EnterState();
        player.doubleJumps--;
    }
    

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        return PlayerStateMachine.EPlayerState.jump;
    }

    public override void ExitState()
    {
        base.ExitState();
        quest.Completed(1);
    }
}
