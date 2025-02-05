using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_DashState : PlayerState
{
    
    
    public Player_DashState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D RB) : base(key, entity)
    {
        
    }
    public override void EnterState()
    {
        TurnOnPhase();
        base.EnterState();
        DashSound();
        timer.SetTimer("dashTimer", player.dashDuration);
        player.canTakeDamage = false;
        player.StartDashCD();
        
        player.Move(InputSystemController.MovementInput().x * player.dashSpeed, InputSystemController.MovementInput().y * player.dashSpeed/1.7f);
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
        TurnOffPhase();
        player.canTakeDamage = true;
        rb.gravityScale = player.playerInfo.gravityScale;
    }

    private void TurnOnPhase()
    {
        player.GetComponent<BoxCollider2D>().excludeLayers += LayerMask.GetMask("WorldObj");
        player.GetComponent<BoxCollider2D>().includeLayers -= LayerMask.GetMask("WorldObj");
    }

    private void TurnOffPhase()
    {
        player.GetComponent<BoxCollider2D>().excludeLayers -= LayerMask.GetMask("WorldObj");
        player.GetComponent<BoxCollider2D>().includeLayers += LayerMask.GetMask("WorldObj");
    }
    
}