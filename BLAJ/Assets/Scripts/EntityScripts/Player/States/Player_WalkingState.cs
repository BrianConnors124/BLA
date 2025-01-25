using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Player_WalkingState : PlayerState
{
    
    public Player_WalkingState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D RB) : base(key, entity)
    {
        
    }
    
    public override void EnterState()
    {
        base.EnterState();
        player.coyoteJump = player.playerInfo.coyoteJump;
        player.doubleJumps = player.playerInfo.doubleJumps + 1;
        if(player.Grounded())player.Move(player.movementSpeed * InputSystemController.MovementInput().x, rb.velocityY);
        player.Anim.speed = Mathf.Abs(player.Velocity.x) / player.movementSpeed;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(player.Grounded())player.Move(player.movementSpeed * InputSystemController.MovementInput().x, rb.velocityY);
        player.Anim.speed = Mathf.Abs(player.Velocity.x) / player.movementSpeed;
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (player.takingDamage) 
            return PlayerStateMachine.EPlayerState.takingDamage;
        
        if (player.SuperAttackReady() && InputSystemController.instance.TryingSuperAttack())
            return PlayerStateMachine.EPlayerState.dashAttack;
        
        if (!player.IsTouchingGround()) 
            return PlayerStateMachine.EPlayerState.falling;
        
        if (InputSystemController.instance.TryingJump())
            return PlayerStateMachine.EPlayerState.jump;
        
        if (InputSystemController.instance.TryingAttack() && player.AttackReady() && timer.TimerDone("minorCD"))
            return PlayerStateMachine.EPlayerState.attack;
        
        
        if ((InputSystemController.instance.TryingDash()) && player.DashReady())
            return PlayerStateMachine.EPlayerState.dash;
        
        if (InputSystemController.MovementInput().magnitude == 0) return PlayerStateMachine.EPlayerState.idle;
        
         
        return StateKey;
    }

    public override void ExitState()
    {
        player.Anim.speed = 1;
        base.ExitState();
    }
}
