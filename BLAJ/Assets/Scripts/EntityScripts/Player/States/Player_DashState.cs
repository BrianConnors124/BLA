using System;
using System.Collections.Generic;
using UnityEngine;

public class Player_DashState : PlayerState
{
    private Rigidbody2D rb;
    
    public Player_DashState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D RB) : base(key, entity)
    {
        Console.WriteLine("Yo");
        rb = RB;
    }
    public override void EnterState()
    {
        base.EnterState();
        stateTimer = player.dashDuration;
        player.StartDashCD();
        rb.velocity = new Vector2(player.dashSpeed * player.Direction(InputSystemController.MovementInput().x), 0);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        Debug.Log(stateTimer + " " + StateTimerDone());
        rb.velocity = new Vector2(player.dashSpeed * player.Direction(InputSystemController.MovementInput().x), 0);
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (StateTimerDone())
        {
            if (player.IsTouchingGround() && InputSystemController.MovementInput().magnitude > 0)
            {
                return PlayerStateMachine.EPlayerState.walking;
            }
            
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
            
            if (InputSystemController.MovementInput().magnitude == 0)
            {
                return PlayerStateMachine.EPlayerState.idle;
            }

            return PlayerStateMachine.EPlayerState.falling;
        }
         
        return StateKey;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}