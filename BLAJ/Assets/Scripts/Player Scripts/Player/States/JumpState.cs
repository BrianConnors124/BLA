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
        rb.velocity = new Vector2(rb.velocityX, player.jumpHeight);
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

        if (InputSystemController.HandleAttack() || InputSystemController.instance.queued == InputSystemController.Equeue.attack)
        {
            InputSystemController.instance.queued = InputSystemController.Equeue.attack; 
            return PlayerStateMachine.EPlayerState.attack;
        }
        
        if (player.IsTouchingGround() && InputSystemController.MovementInput().magnitude == 0)
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