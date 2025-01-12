using System;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Player_AttackState : PlayerState
{
    
    public Player_AttackState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D RB) : base(key, entity)
    {
        
    }
    public override void EnterState()
    {
        animEnded = false;
        var a = StateKey.ToString() + attackInt;
        player.Anim.Play(a);
        player.canFlip = false;
        timer.SetActionTimer(attackKey, player.coolDowns[player.cooldownKey[0]], () => attackInt = 1);
        if(attackInt == 3)player.StartAttackCD();
        player.Move(player.movementSpeed * InputSystemController.MovementInput().x / 1.3f, rb.velocityY);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        player.Move(player.movementSpeed * InputSystemController.MovementInput().x / 1.3f, rb.velocityY);
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (animEnded)
        {
            if (player.Grounded())
            {
                return PlayerStateMachine.EPlayerState.walking;
            }

            return PlayerStateMachine.EPlayerState.falling;
        }
        
         
        return StateKey;
    }

    public override void ExitState()
    {
        base.ExitState();
        attackInt++;
        player.canFlip = true;
    }
}
