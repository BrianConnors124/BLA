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
        player.timer.RemoveTimer("Jump");
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
        if (player.takingDamage) return PlayerStateMachine.EPlayerState.takingDamage;

        if (player.SuperAttackReady() && InputSystemController.instance.TryingSuperAttack() && player.unlockables[2])
            return PlayerStateMachine.EPlayerState.slamAttack;
        
        if ((InputSystemController.instance.TryingAttack()) && player.AttackReady() && timer.TimerDone("minorCD"))
            return PlayerStateMachine.EPlayerState.attack;
        
        if ((InputSystemController.instance.TryingDash()) && player.DashReady() && player.unlockables[0])
            return PlayerStateMachine.EPlayerState.dash;
        
        if (player.doubleJumps > 0 && InputSystemController.instance.TryingJump())
            return PlayerStateMachine.EPlayerState.doubleJump;
        
        if (rb.velocityY <= 0) return PlayerStateMachine.EPlayerState.falling;
        
        
        return StateKey;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}