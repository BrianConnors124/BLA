using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_FallingState : PlayerState
{

    public Player_FallingState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D RB) : base(key, entity)
    {
        
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
        if (player.takingDamage) return PlayerStateMachine.EPlayerState.takingDamage;
        
        if (player.Grounded()) return PlayerStateMachine.EPlayerState.walking;
        
        if (player.DashReady() && (InputSystemController.instance.TryingDash()))
            return PlayerStateMachine.EPlayerState.dash;

        if (player.SuperAttackReady() && InputSystemController.instance.TryingSuperAttack())
            return PlayerStateMachine.EPlayerState.slamAttack;
        
        if (player.AttackReady() && (InputSystemController.instance.TryingAttack()))
            return PlayerStateMachine.EPlayerState.attack;
        
        if (player.doubleJumps > 0 && InputSystemController.instance.HandleJump())
            return PlayerStateMachine.EPlayerState.doubleJump;

        if (rb.velocityY < -70 && player.CloseToGround()) return PlayerStateMachine.EPlayerState.contactWithGround;

        return StateKey;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
