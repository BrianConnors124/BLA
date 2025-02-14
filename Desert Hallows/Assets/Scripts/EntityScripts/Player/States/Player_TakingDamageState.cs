using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Player_TakingDamage : PlayerState
{
    public Player_TakingDamage(PlayerStateMachine.EPlayerState key, Player entity) : base(key, entity)
    {
        
    }

    public override void EnterState()
    {
        base.EnterState();
        TakeDamage();
        
        if (player.recentKnockBack == 0) return;
        player.Move(player.recentKnockBack * player.knockBackDirection,player.recentKnockBack);
        stateTimer = 0.2f;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        player.Move(player.movementSpeed * InputSystemController.MovementInput().x, rb.velocityY);
    }
    

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        //if(StateTimerDone()) return stunnedState;
        return StateTimerDone() ? PlayerStateMachine.EPlayerState.stunned : StateKey;
    }
    
    public override void ExitState()
    {
        base.ExitState();
        player.takingDamage = false;
    }
}
