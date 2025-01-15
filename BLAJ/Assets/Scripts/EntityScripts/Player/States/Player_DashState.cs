using System;
using System.Collections.Generic;
using UnityEngine;

public class Player_DashState : PlayerState
{
    
    
    public Player_DashState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D RB) : base(key, entity)
    {
        Console.WriteLine("Yo");
    }
    public override void EnterState()
    {
        base.EnterState();
        stateTimer = player.dashDuration;
        player.canTakeDamage = false;
        player.StartDashCD();
        rb.gravityScale = 0;
        rb.velocity = new Vector2(player.dashSpeed * player.FacingDirectionInt(), 0);
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
                return PlayerStateMachine.EPlayerState.walking;
            }
            
            return PlayerStateMachine.EPlayerState.falling; 
        }
         
        return StateKey;
    }

    public override void ExitState()
    {
        base.ExitState();
        player.canTakeDamage = true;
        rb.gravityScale = player.playerInfo.gravityScale;
        rb.velocity = new Vector2(player.movementSpeed * player.Direction(InputSystemController.MovementInput().x), rb.velocityY);
    }
}