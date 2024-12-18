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
        player.doubleJumps--;
        stateTimer = 0.4f;
        player.Move(player.movementSpeed * InputSystemController.MovementInput().x, player.jumpHeight);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        player.Move(player.movementSpeed * InputSystemController.MovementInput().x, rb.velocityY);
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (player.AttackReady() && (InputSystemController.instance.HandleAttack() || InputSystemController.instance.queued == InputSystemController.Equeue.attack))
            return PlayerStateMachine.EPlayerState.attack;
        
        if (rb.velocityY < 0) return PlayerStateMachine.EPlayerState.falling;
        
        if (player.DashReady() && (InputSystemController.instance.HandleDash() || InputSystemController.instance.queued == InputSystemController.Equeue.dash))
            return PlayerStateMachine.EPlayerState.dash;
        
        if (player.doubleJumps > 0 && InputSystemController.instance.HandleJump())
            return PlayerStateMachine.EPlayerState.doubleJump;
        
        if (player.Grounded()) return PlayerStateMachine.EPlayerState.transferGround;
        
        return StateKey;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}