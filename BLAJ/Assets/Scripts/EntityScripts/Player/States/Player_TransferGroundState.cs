using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_TransferGroundState : PlayerState
{
    
    
    public Player_TransferGroundState(PlayerStateMachine.EPlayerState key, Player entity) : base(key, entity)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (InputSystemController.MovementInput().magnitude > 0) return PlayerStateMachine.EPlayerState.walking;
        
        
        if (player.AttackReady() && (InputSystemController.instance.HandleAttack() || InputSystemController.instance.queued == InputSystemController.Equeue.attack))
            return PlayerStateMachine.EPlayerState.attack;

        if (InputSystemController.instance.HandleJump() || InputSystemController.instance.queued == InputSystemController.Equeue.jump)
            return PlayerStateMachine.EPlayerState.jump;
         
        if (InputSystemController.MovementInput().magnitude == 0) return PlayerStateMachine.EPlayerState.idle;
        
        return StateKey;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
