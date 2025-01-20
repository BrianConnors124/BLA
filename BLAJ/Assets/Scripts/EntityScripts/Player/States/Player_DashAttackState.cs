using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_DashAttackState : PlayerState
{
    public Player_DashAttackState(PlayerStateMachine.EPlayerState key, Player entity) : base(key, entity)
    {
        
    }

    private Vector2 startingPos, endingPos;
    private bool once;
    public override void EnterState()
    {
        base.EnterState();
        timer.SetTimer("attackDashTimer", player.dashDuration);
        startingPos = rb.position;
        once = true;
        player.Move(player.dashSpeed * 1.2f * player.MovementDirection(), 0);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (timer.TimerDone("attackDashTimer") && once)
        {
            endingPos = rb.position;
            DoDashAttack(startingPos, endingPos);
            once = false;
        }
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (timer.TimerDone("attackDashTimer") && !once)
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
        player.StartSuperAttackCD();
        
    }
    
}
