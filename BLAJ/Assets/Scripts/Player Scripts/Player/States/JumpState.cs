using System.Collections.Generic;
using UnityEngine;

public class JumpState : PlayerState
{
    private Rigidbody2D rb;
    
    public JumpState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D RB) : base(key, entity)
    {
        rb = RB;
    }
    public override void EnterState()
    {
        base.EnterState();
        player.StartJumpCD();
        player.SetVelocity(new Vector2(rb.velocityX,24));
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (InputSystemController.MovementInput().magnitude > 0)
        {
            return PlayerStateMachine.EPlayerState.walking;
        }

        if (InputSystemController.HandleAttack() || InputSystemController.instance.queued == InputSystemController.Equeue.attack)
        {
            InputSystemController.instance.queued = InputSystemController.Equeue.attack; 
            return PlayerStateMachine.EPlayerState.attack;
        }
        
        if (InputSystemController.MovementInput().magnitude == 0 && player.IsTouchingGround())
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