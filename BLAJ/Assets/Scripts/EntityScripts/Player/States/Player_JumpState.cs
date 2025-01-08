using System.Collections.Generic;
using UnityEngine;

public class Player_JumpState : PlayerState
{
    
    
    public Player_JumpState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D RB) : base(key, entity)
    {
        
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
        if (player.takingDamage) return PlayerStateMachine.EPlayerState.takingDamage;
        
        if ((InputSystemController.instance.HandleAttack() || InputSystemController.instance.queued == InputSystemController.Equeue.attack) && player.AttackReady())
            return PlayerStateMachine.EPlayerState.attack;
        
        if (rb.velocityY < 0) return PlayerStateMachine.EPlayerState.falling;
        
        if ((InputSystemController.instance.HandleDash() || InputSystemController.instance.queued == InputSystemController.Equeue.dash) && player.DashReady())
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