using System;
using UnityEngine;

public class PlayerState : State<PlayerStateMachine.EPlayerState>
{

    public PlayerState(PlayerStateMachine.EPlayerState key, Player entity) : base(key)
    {
        player = entity;
    }

    protected Player player;

    public override void EnterState()
    {
        base.EnterState();
        Console.WriteLine(StateKey);
        //player.Anim.SetBool(StateKey.ToString(), true);
    }

    public override void ExitState()
    {
        base.ExitState();
        //player.Anim.SetBool(StateKey.ToString(), false);
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        throw new System.NotImplementedException();
    }
}
