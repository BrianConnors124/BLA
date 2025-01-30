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
        
        if (player.DashReady() && InputSystemController.instance.TryingDash() && player.hasDash)
            return PlayerStateMachine.EPlayerState.dash;

        if (player.SuperAttackReady() && InputSystemController.instance.TryingSuperAttack() && player.hasSlamAttack)
            return PlayerStateMachine.EPlayerState.slamAttack;
        
        
        if (player.AttackReady() && InputSystemController.instance.TryingAttack() && timer.TimerDone("minorCD"))
            return PlayerStateMachine.EPlayerState.attack;
        
        if (player.doubleJumps > 0 && InputSystemController.instance.TryingJump())
            return PlayerStateMachine.EPlayerState.doubleJump;

        return StateKey;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
