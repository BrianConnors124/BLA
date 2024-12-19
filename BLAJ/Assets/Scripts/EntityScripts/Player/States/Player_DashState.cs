using System;
using System.Collections.Generic;
using UnityEngine;

public class Player_DashState : PlayerState
{
    private Rigidbody2D rb;
    
    public Player_DashState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D RB) : base(key, entity)
    {
        Console.WriteLine("Yo");
        rb = RB;
    }
    public override void EnterState()
    {
        base.EnterState();
        stateTimer = player.dashDuration;
        player.StartDashCD();
        rb.gravityScale = 0;
        rb.velocity = new Vector2(player.dashSpeed * player.Direction(InputSystemController.MovementInput().x), 0);
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (StateTimerDone())
        {
            if (player.Grounded())
            {
                return PlayerStateMachine.EPlayerState.transferGround;
            }
            
            return PlayerStateMachine.EPlayerState.falling; 
        }
         
        return StateKey;
    }

    public override void ExitState()
    {
        base.ExitState();
        rb.gravityScale = player.playerInfo.gravityScale;
        rb.velocity = new Vector2(player.movementSpeed * player.Direction(InputSystemController.MovementInput().x), rb.velocityY);
    }
}