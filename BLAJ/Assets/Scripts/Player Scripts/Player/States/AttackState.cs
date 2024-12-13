using System;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class AttackState : PlayerState
{
    private Rigidbody2D rb;
    private Animation _animator;
    
    public AttackState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D RB) : base(key, entity)
    {
        rb = RB;
    }
    public override void EnterState()
    {
        base.EnterState();
        player.StartAttackCD();
        Debug.Log(_animator);
    }

    public override void UpdateState()
    {
        rb.velocity = new Vector2(player.movementSpeed * player.Direction(InputSystemController.MovementInput().x), rb.velocityY);
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if(player.IsTouchingGround() && animEnded){
            if (InputSystemController.MovementInput().magnitude == 0)
            {
                return PlayerStateMachine.EPlayerState.idle;
            }
        }
         
        return StateKey;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
