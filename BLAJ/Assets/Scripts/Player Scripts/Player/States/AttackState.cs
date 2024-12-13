using System;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : PlayerState
{
    private Rigidbody2D rb;
    
    public AttackState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D RB) : base(key, entity)
    {
        rb = RB;
    }
    public override void EnterState()
    {
        base.EnterState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        rb.velocity = new Vector2(player.movementSpeed * player.Direction(InputSystemController.MovementInput().x), rb.velocityY);
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
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
        if (player.IsTouchingGround() && player.dash.TimerDone && InputSystemController.HandleDash() || InputSystemController.instance.queued == InputSystemController.Equeue.dash)
        {
            InputSystemController.instance.queued = InputSystemController.Equeue.dash;
            return PlayerStateMachine.EPlayerState.dash;
        }
            
        if (InputSystemController.MovementInput().magnitude == 0)
        {
            return PlayerStateMachine.EPlayerState.idle;
        }
         
        return StateKey;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
