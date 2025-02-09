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
        player.canFlip = false;
        //Debug.Log("Taking Damage");
        if (player.recentKnockBack == 0) return;
        player.Move(player.recentKnockBack * player.knockBackDirection,player.recentKnockBack);
        stateTimer = 0.2f;
        if (player.recentStun > 0) stateTimer = player.recentStun;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(player.IsTouchingGround()) player.ZeroVelocity();
    }
    

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        //if(StateTimerDone()) return stunnedState;
        return StateTimerDone() ? PlayerStateMachine.EPlayerState.idle : StateKey;
    }
    
    public override void ExitState()
    {
        base.ExitState();
        player.takingDamage = false;
        player.canFlip = true;
    }
}
