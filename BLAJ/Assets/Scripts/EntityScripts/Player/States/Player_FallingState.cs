using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_FallingState : PlayerState
{
    private Rigidbody2D rb;

    public Player_FallingState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D RB) : base(key, entity)
    {
        rb = RB;
    }

    public override void EnterState()
    {
        base.EnterState();
        player.Move(player.movementSpeed * InputSystemController.MovementInput().x / 1.3f, rb.velocityY);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        player.Move(player.movementSpeed * InputSystemController.MovementInput().x / 1.3f, rb.velocityY);
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (player.IsTouchingGround() && InputSystemController.MovementInput().magnitude > 0)
            return PlayerStateMachine.EPlayerState.walking;
        if (player.DashReady() && InputSystemController.HandleDash() || InputSystemController.instance.queued == InputSystemController.Equeue.dash) 
            return PlayerStateMachine.EPlayerState.dash;
        if (player.AttackReady() && InputSystemController.HandleAttack() || InputSystemController.instance.queued == InputSystemController.Equeue.attack)
            return PlayerStateMachine.EPlayerState.attack;
        if (player.doubleJumps > 0 && InputSystemController.instance.HandleJump() || InputSystemController.instance.queued == InputSystemController.Equeue.jump) 
            return PlayerStateMachine.EPlayerState.doubleJump;
        if (player.CloseToGround() && Mathf.Abs(rb.velocityY) > 50)
            return PlayerStateMachine.EPlayerState.contactWithGround;
        if (player.IsTouchingGround())
            return PlayerStateMachine.EPlayerState.idle;

        return StateKey;
    }
}
