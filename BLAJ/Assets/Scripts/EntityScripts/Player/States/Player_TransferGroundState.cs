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
        if (player.takingDamage) return PlayerStateMachine.EPlayerState.takingDamage;
        
        if (InputSystemController.MovementInput().magnitude > 0) return PlayerStateMachine.EPlayerState.walking;
        
        
        if ((InputSystemController.instance.TryingAttack()) && player.AttackReady())
            return PlayerStateMachine.EPlayerState.attack;

        if (InputSystemController.instance.TryingJump())
            return PlayerStateMachine.EPlayerState.jump;
         
        if (InputSystemController.MovementInput().magnitude == 0) return PlayerStateMachine.EPlayerState.idle;
        
        return StateKey;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
