using System.Collections.Generic;
using UnityEngine;

public class Player_DoubleJumpState : PlayerState
{
    private Rigidbody2D rb;
    
    public Player_DoubleJumpState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D RB) : base(key, entity)
    {
        rb = RB;
    }

    public override void EnterState()
    {
        base.EnterState();
        player.doubleJumps--;
        rb.velocity = new Vector2(rb.velocityX, 0);
    }
    

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        return PlayerStateMachine.EPlayerState.jump;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}