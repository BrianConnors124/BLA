using System;
using System.Collections.Generic;
using UnityEngine;

public class DashState : PlayerState
{
    private Rigidbody2D rb;
    
    public DashState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D RB) : base(key, entity)
    {
        Console.WriteLine("Yo");
        rb = RB;
    }
    public override void EnterState()
    {
        base.EnterState();
        player.SetVelocity(new Vector2(15,rb.velocityY));
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (InputSystemController.instance.HandleJump() || InputSystemController.instance.queued == InputSystemController.Equeue.jump)
        {
            InputSystemController.instance.queued = InputSystemController.Equeue.jump; 
            return PlayerStateMachine.EPlayerState.jump;
        }

        if (InputSystemController.HandleAttack() || InputSystemController.instance.queued == InputSystemController.Equeue.attack)
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