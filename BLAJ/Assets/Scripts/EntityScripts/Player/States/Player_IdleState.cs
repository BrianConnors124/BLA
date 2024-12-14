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
        if (player.IsTouchingGround() && InputSystemController.instance.HandleJump() || InputSystemController.instance.queued == InputSystemController.Equeue.jump)
        {
            InputSystemController.instance.queued = InputSystemController.Equeue.jump; 
            return PlayerStateMachine.EPlayerState.jump;
        }

        if (player.AttackReady() && InputSystemController.HandleAttack() || InputSystemController.instance.queued == InputSystemController.Equeue.attack)
        {
            InputSystemController.instance.queued = InputSystemController.Equeue.attack; 
            return PlayerStateMachine.EPlayerState.attack;
        }
        if (InputSystemController.MovementInput().magnitude > 0)
        {
            return PlayerStateMachine.EPlayerState.walking;
        }
         
        return StateKey;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
