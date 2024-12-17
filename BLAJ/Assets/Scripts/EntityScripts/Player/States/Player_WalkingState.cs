using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Player_WalkingState : PlayerState
{
    private Rigidbody2D rb;
    
    public Player_WalkingState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D RB) : base(key, entity)
    {
        rb = RB;
    }
    public override void EnterState()
    {
        base.EnterState();
        player.coyoteJump = player.playerInfo.coyoteJump;
        player.doubleJumps = player.playerInfo.doubleJumps + 1;
        player.Move(player.movementSpeed * InputSystemController.MovementInput().x, rb.velocityY);
        player.Anim.speed = Mathf.Abs(InputSystemController.MovementInput().x);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        player.Move(player.movementSpeed * InputSystemController.MovementInput().x, rb.velocityY);
        player.Anim.speed = Mathf.Abs(InputSystemController.MovementInput().x);
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (player.IsTouchingGround() && InputSystemController.instance.HandleJump() || InputSystemController.instance.queued == InputSystemController.Equeue.jump) 
            return PlayerStateMachine.EPlayerState.jump;
        

        if (player.AttackReady() && InputSystemController.HandleAttack() || InputSystemController.instance.queued == InputSystemController.Equeue.attack) 
            return PlayerStateMachine.EPlayerState.attack;
        
        if (player.DashReady() && InputSystemController.HandleDash() || InputSystemController.instance.queued == InputSystemController.Equeue.dash) 
            return PlayerStateMachine.EPlayerState.dash;
        
        if (InputSystemController.MovementInput().magnitude == 0) 
            return PlayerStateMachine.EPlayerState.idle;
        if (rb.velocityY < 0)
            return PlayerStateMachine.EPlayerState.falling;
        
         
        return StateKey;
    }

    public override void ExitState()
    {
        player.Anim.speed = 1;
        base.ExitState();
    }
}
