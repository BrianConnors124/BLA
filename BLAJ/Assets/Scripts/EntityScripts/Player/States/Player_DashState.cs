using System;
using System.Collections.Generic;
using UnityEngine;

public class Player_DashState : PlayerState
{
    
    
    public Player_DashState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D RB) : base(key, entity)
    {
        
    }
    public override void EnterState()
    {
        player.GetComponent<BoxCollider2D>().excludeLayers += LayerMask.GetMask("WorldObj");
        player.GetComponent<BoxCollider2D>().includeLayers -= LayerMask.GetMask("WorldObj");
        base.EnterState();
        
        timer.SetTimer("dashTimer", player.dashDuration);
        player.canTakeDamage = false;
        player.StartDashCD();

        player.Move(InputSystemController.MovementInput().normalized.x * player.dashSpeed, InputSystemController.MovementInput().normalized.y * player.dashSpeed/1.5f);
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (timer.TimerDone("dashTimer"))
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
        player.GetComponent<BoxCollider2D>().excludeLayers -= LayerMask.GetMask("WorldObj");
        player.GetComponent<BoxCollider2D>().includeLayers += LayerMask.GetMask("WorldObj");
        player.canTakeDamage = true;
        rb.gravityScale = player.playerInfo.gravityScale;
        rb.velocity = new Vector2(player.movementSpeed * player.Direction(InputSystemController.MovementInput().x), rb.velocityY);
    }
}