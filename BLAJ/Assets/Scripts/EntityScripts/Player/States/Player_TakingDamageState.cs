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
        player.canTakeDamage = false;
        Debug.Log("Taking Damage");
        if (player.recentKnockBack == 0) return;
        player.SetVelocity(new Vector2(player.recentKnockBack * player.KnockBackDirection, player.recentKnockBack));
        stateTimer = 0.2f;
        if (player.recentStun > 0) stateTimer = player.recentStun;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(player.IsTouchingGround() && player.recentStun - stateTimer >= .2f) player.ZeroVelocity();
    }
    

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        //if(StateTimerDone()) return stunnedState;
        return player.Velocity.y <= 0 && StateTimerDone() ? PlayerStateMachine.EPlayerState.falling : StateKey;
    }
    
    public override void ExitState()
    {
        base.ExitState();
        player.takingDamage = false;
        player.canTakeDamage = true;
    }
}
