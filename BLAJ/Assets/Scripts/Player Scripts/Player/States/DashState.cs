using System;
using System.Collections.Generic;
using UnityEngine;

public class DashState : PlayerState
{
    private Rigidbody2D rb;
    
    public DashState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D RB) : base(key, entity)
    {
        Console.WriteLine("Yo");
        rb = RB;
    }
    public override void EnterState()
    {
        base.EnterState();
        player.StartDashCD();
        stateTimer = player.dashDuration;
        rb.velocity = new Vector2(player.dashSpeed * player.Direction(InputSystemController.MovementInput().x), rb.velocityY);
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (TimerDone())
        {
            if (player.IsTouchingGround() && InputSystemController.MovementInput().magnitude > 0)
            {
                return PlayerStateMachine.EPlayerState.walking;
            }
            
            if (player.IsTouchingGround() && InputSystemController.instance.HandleJump() || InputSystemController.instance.queued == InputSystemController.Equeue.jump)
            {
                InputSystemController.instance.queued = InputSystemController.Equeue.jump; 
                return PlayerStateMachine.EPlayerState.jump;
            }

            if (InputSystemController.HandleAttack() || InputSystemController.instance.queued == InputSystemController.Equeue.attack)
            {
                InputSystemController.instance.queued = InputSystemController.Equeue.attack; 
                return PlayerStateMachine.EPlayerState.attack;
            }
            
            if (InputSystemController.MovementInput().magnitude == 0)
            {
                return PlayerStateMachine.EPlayerState.idle;
            }  
        }
         
        return StateKey;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}