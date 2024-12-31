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
        player.SetVelocity(new Vector2(0,0));
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (InputSystemController.MovementInput().magnitude > 0) return PlayerStateMachine.EPlayerState.walking;
        
        
        if ((InputSystemController.instance.HandleAttack() || InputSystemController.instance.queued == InputSystemController.Equeue.attack) && player.AttackReady())
            return PlayerStateMachine.EPlayerState.attack;

        if (InputSystemController.instance.HandleJump() || InputSystemController.instance.queued == InputSystemController.Equeue.jump)
            return PlayerStateMachine.EPlayerState.jump;

        if (!player.IsTouchingGround()) return PlayerStateMachine.EPlayerState.falling;
         
        return StateKey;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
