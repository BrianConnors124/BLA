using System.Collections.Generic;
using UnityEngine;

public class Player_JumpState : PlayerState
{
    private Rigidbody2D rb;
    
    public Player_JumpState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D RB) : base(key, entity)
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
        if (animEnded && rb.velocityY <= 0)
        {
            return PlayerStateMachine.EPlayerState.walking;
        } 
         
        return StateKey;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}