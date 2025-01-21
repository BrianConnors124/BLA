using System;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Player_AttackState : PlayerState
{
    
    public Player_AttackState(PlayerStateMachine.EPlayerState key, Player entity) : base(key, entity)
    {
        
    }
    public override void EnterState()
    {
        animEnded = false;
        attackInt++;
        var a = StateKey.ToString() + attackInt;
        player.Anim.Play(a);
        timer.SetActionTimer("canFlip", 0.1f, () => player.canFlip = false);
        timer.SetActionTimer(attackKey, player.coolDowns[player.cooldownKey[0]], () => attackInt = 0);
    }

    public override void UpdateState()
    {
        var a = StateKey.ToString() + attackInt;
        player.Anim.Play(a);
        if(player.Grounded())player.Move(player.movementSpeed * InputSystemController.MovementInput().x / 5, rb.velocityY);
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
        if(attackInt == 3) player.StartAttackCD();
        player.canFlip = true;
        timer.SetTimer("minorCD", player.coolDowns[player.cooldownKey[0]]/2f);
    }
}
