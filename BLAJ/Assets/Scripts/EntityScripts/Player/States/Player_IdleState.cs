using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_IdleState : PlayerState
{
    
    
    public Player_IdleState(PlayerStateMachine.EPlayerState key, Player entity) : base(key, entity)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        player.ZeroVelocity();
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (player.takingDamage) 
            return PlayerStateMachine.EPlayerState.takingDamage;
        
        if (InputSystemController.MovementInput().magnitude > 0) 
            return PlayerStateMachine.EPlayerState.walking;

        if (player.SuperAttackReady() && InputSystemController.instance.TryingSuperAttack())
            return PlayerStateMachine.EPlayerState.dashAttack;
        
        
        if ((InputSystemController.instance.TryingAttack()) && player.AttackReady() && timer.TimerDone("minorCD"))
            return PlayerStateMachine.EPlayerState.attack;

        if (InputSystemController.instance.TryingJump())
            return PlayerStateMachine.EPlayerState.jump;

        if (!player.IsTouchingGround()) return PlayerStateMachine.EPlayerState.falling;
         
        return StateKey;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
