using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerState
{
    public IdleState(PlayerStateMachine.EPlayerState key, Player entity) : base(key, entity)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        player.SetVelocity(new Vector2(0,0));
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (InputSystemController.HandleJump())
            return PlayerStateMachine.EPlayerState.jump;
        if (InputSystemController.HandleAttack())
            return PlayerStateMachine.EPlayerState.attack;
        if (InputSystemController.MovementInput().magnitude > 0)
            return PlayerStateMachine.EPlayerState.walking;
        return StateKey;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
