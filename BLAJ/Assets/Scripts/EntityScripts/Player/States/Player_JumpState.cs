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
        
        if ((InputSystemController.instance.TryingAttack()) && player.AttackReady())
            return PlayerStateMachine.EPlayerState.attack;
        
        if ((InputSystemController.instance.TryingDash()) && player.DashReady())
            return PlayerStateMachine.EPlayerState.dash;
        
        
        if (rb.velocityY < 0) return PlayerStateMachine.EPlayerState.falling;
        
        if (player.Grounded()) return PlayerStateMachine.EPlayerState.walking;
        
        return StateKey;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}