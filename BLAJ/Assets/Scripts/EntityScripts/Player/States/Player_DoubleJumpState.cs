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
        player.Move(player.movementSpeed * InputSystemController.MovementInput().x, player.jumpHeight);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        player.Move(player.movementSpeed * InputSystemController.MovementInput().x, rb.velocityY);
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (player.DashReady() && InputSystemController.HandleDash() ||
            InputSystemController.instance.queued == InputSystemController.Equeue.dash)
            return PlayerStateMachine.EPlayerState.dash;
        
        if (rb.velocityY < -0.5f) return PlayerStateMachine.EPlayerState.falling;
        
        if (player.doubleJumps > 0 && InputSystemController.instance.HandleJump() || InputSystemController.instance.queued == InputSystemController.Equeue.jump) 
            return PlayerStateMachine.EPlayerState.jump;
        
        return StateKey;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
