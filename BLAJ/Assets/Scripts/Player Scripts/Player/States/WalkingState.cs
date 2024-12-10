using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingState : PlayerState
{
    private Rigidbody2D rb;
    
    public WalkingState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D RB) : base(key, entity)
    {
        rb = RB;
    }
    public override void EnterState()
    {
        base.EnterState();
        player.SetVelocity(new Vector2(InputSystemController.MovementInput().x * 100,rb.velocityY));
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (InputSystemController.instance.HandleJump() || InputSystemController.instance.queued == InputSystemController.Equeue.jump)
        {
            InputSystemController.instance.queued = InputSystemController.Equeue.jump; 
            return PlayerStateMachine.EPlayerState.jump;
        }

        if (InputSystemController.HandleAttack() || InputSystemController.instance.queued == InputSystemController.Equeue.attack)
        {
            InputSystemController.instance.queued = InputSystemController.Equeue.attack; 
            return PlayerStateMachine.EPlayerState.attack;
        }
        if (InputSystemController.HandleDash() || InputSystemController.instance.queued == InputSystemController.Equeue.dash)
        {
            InputSystemController.instance.queued = InputSystemController.Equeue.dash;
            return PlayerStateMachine.EPlayerState.dash;
        }
        if (InputSystemController.MovementInput().magnitude.Equals(0))
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
