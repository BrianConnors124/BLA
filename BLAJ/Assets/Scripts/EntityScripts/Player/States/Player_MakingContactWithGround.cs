using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MakingContactWithGround : PlayerState
{
    private Rigidbody2D rb;
    public Player_MakingContactWithGround(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D RB) : base(key, entity)
    {
        rb = RB;
    }

    public override void EnterState()
    {
        base.EnterState();
        player.Move(player.movementSpeed * InputSystemController.MovementInput().x, rb.velocityY);
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (player.IsTouchingGround()) return PlayerStateMachine.EPlayerState.idle;
        if (player.DashReady() && InputSystemController.HandleDash() || InputSystemController.instance.queued == InputSystemController.Equeue.dash) return PlayerStateMachine.EPlayerState.dash;
        
        return base.GetNextState();
    }
}
