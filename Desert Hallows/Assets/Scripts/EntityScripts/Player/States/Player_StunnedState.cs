using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_StunnedState : PlayerState
{
    public Player_StunnedState(PlayerStateMachine.EPlayerState key, Player entity) : base(key, entity)
    {
        
    }

    public override void EnterState()
    {
        base.EnterState();
        player.ZeroVelocity();
        player.canFlip = false;
        stateTimer = player.recentStun;
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }
    

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        //if(StateTimerDone()) return stunnedState;
        return StateTimerDone() ? PlayerStateMachine.EPlayerState.idle : StateKey;
    }
    
    public override void ExitState()
    {
        base.ExitState();
        player.canFlip = true;
    }
}
