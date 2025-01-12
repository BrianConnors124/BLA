using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Player_WalkingState : PlayerState
{
    
    public Player_WalkingState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D RB) : base(key, entity)
    {
        
    }

    private bool canIdle;
    public override void EnterState()
    {
        base.EnterState();
        canIdle = false;
        player.coyoteJump = player.playerInfo.coyoteJump;
        player.doubleJumps = player.playerInfo.doubleJumps + 1;
        player.Move(Speed.Calculator(player.Velocity.x, Speed.Calculator(0.15f, 0.05f, 0.28f) * InputSystemController.MovementInput().x, player.movementSpeed * Mathf.Abs(InputSystemController.MovementInput().x)), player.Velocity.y);
        player.Anim.speed = Mathf.Abs(player.Velocity.x) / player.movementSpeed;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        
        if(player.Velocity.x == 0 && !timer.TimerActive(idleWaitTime)) timer.SetActionTimer(idleWaitTime, .1f, () => canIdle = true);
        if(player.Velocity.x != 0) timer.RemoveActionTimer(idleWaitTime);
        
        player.Move(Speed.Calculator(player.Velocity.x, Speed.Calculator(0.15f, 0.05f, 0.28f) * InputSystemController.MovementInput().x, player.movementSpeed * Mathf.Abs(InputSystemController.MovementInput().x)), player.Velocity.y);
        player.Anim.speed = Mathf.Abs(player.Velocity.x) / player.movementSpeed;
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (player.takingDamage) return PlayerStateMachine.EPlayerState.takingDamage;
        
        if (InputSystemController.instance.TryingJump())
            return PlayerStateMachine.EPlayerState.jump;
        
        if (InputSystemController.MovementInput().magnitude == 0 && canIdle) return PlayerStateMachine.EPlayerState.idle;
        
        if (InputSystemController.instance.TryingAttack() && player.AttackReady())
            return PlayerStateMachine.EPlayerState.attack;
        
        
        if ((InputSystemController.instance.TryingDash()) && player.DashReady())
            return PlayerStateMachine.EPlayerState.dash;
        
        if (!player.IsTouchingGround()) return PlayerStateMachine.EPlayerState.falling;
         
        return StateKey;
    }

    public override void ExitState()
    {
        player.Anim.speed = 1;
        base.ExitState();
    }
}
