using System.Collections.Generic;
using UnityEngine;

public class Player_DoubleJumpState : PlayerState
{
    private Rigidbody2D rb;
    
    public Player_DoubleJumpState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D RB) : base(key, entity)
    {
        rb = RB;
    }

    public override void EnterState()
    {
        base.EnterState();
        player.doubleJumps--;
        rb.velocity = new Vector2(rb.velocityX, player.jumpHeight);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        rb.velocity = new Vector2(player.movementSpeed * player.Direction(InputSystemController.MovementInput().x), rb.velocityY);
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (rb.velocityY < 0.5f) return PlayerStateMachine.EPlayerState.falling;

        if (player.DashReady() && InputSystemController.HandleDash() ||
            InputSystemController.instance.queued == InputSystemController.Equeue.dash)
            return PlayerStateMachine.EPlayerState.dash;
         
        return StateKey;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
