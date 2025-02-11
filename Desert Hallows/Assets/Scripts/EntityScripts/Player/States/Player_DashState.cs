using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_DashState : PlayerState
{
    
    
    public Player_DashState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D RB) : base(key, entity)
    {
        
    }

    private Vector2 movementInputDirection;
    public override void EnterState()
    {
        TurnOnPhase();
        base.EnterState();
        DashSound();
        timer.SetTimer("dashTimer", player.dashDuration);
        player.canTakeDamage = false;
        player.StartDashCD();
        movementInputDirection = FindAngularVector(InputSystemController.MovementInput());
        player.Move(movementInputDirection.x * player.dashSpeed * player.MovementDirection(), movementInputDirection.y * player.dashSpeed);
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
        player.Move(20 * movementInputDirection.x, 20 * movementInputDirection.y);
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

    private Vector2 FindAngularVector(Vector2 input)
    {
        if (Mathf.Rad2Deg * Math.Atan2(Math.Abs(input.y), Math.Abs(input.x)) + 22.5 >= 45)
        {
            if (Mathf.Rad2Deg * Math.Atan2(Math.Abs(input.y), Math.Abs(input.x)) + 22.5 >= 90)
            {
                return new Vector2(0, 1 * Math.Abs(input.y)/ input.y);
            }
            
            return new Vector2((float)Math.Sqrt(2)/2, (float) Math.Sqrt(2)/2 * Math.Abs(input.y)/ input.y);
            
        }

        return new Vector2(1, 0);
    }
    
}