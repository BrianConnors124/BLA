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
        stateTimer = 0.2f;
        player.Move(player.movementSpeed * InputSystemController.MovementInput().x, player.jumpHeight);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        player.Move(player.movementSpeed * InputSystemController.MovementInput().x, rb.velocityY);
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        
        
        return StateKey;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
