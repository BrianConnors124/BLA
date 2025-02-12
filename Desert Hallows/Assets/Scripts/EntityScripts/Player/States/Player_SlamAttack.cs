using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_SlamAttack : PlayerState
{
    public Player_SlamAttack(PlayerStateMachine.EPlayerState key, Player entity) : base(key, entity)
    {
        
    }
    
    private float maxVelo;
    private bool once;
    public override void EnterState()
    {
        base.EnterState();
        once = true;
        player.canTakeDamage = false;
        rb.gravityScale *= 3;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (-player.Velocity.y > maxVelo) maxVelo = -player.Velocity.y;

        if (player.CloseToGround() && once)
        {
            DoSlamAttack(maxVelo);
            once = false;
        }
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (player.IsTouchingGround() && !once) return PlayerStateMachine.EPlayerState.idle;
        return StateKey;
    }

    public override void ExitState()
    {
        base.ExitState();
        player.StartSuperAttackCD();
        rb.gravityScale = player.playerInfo.gravityScale;
        player.timer.SetActionTimer("I-Frames", .5f, () => player.canTakeDamage = true);
        maxVelo = 0;
    }
}
