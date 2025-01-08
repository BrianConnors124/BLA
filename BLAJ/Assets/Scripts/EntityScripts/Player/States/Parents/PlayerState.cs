using System;
using UnityEngine;

public class PlayerState : State<PlayerStateMachine.EPlayerState>
{
    public PlayerState(PlayerStateMachine.EPlayerState key, Player entity) : base(key)
    {
        player = entity;
        rb = player.GetComponent<Rigidbody2D>();
        timer = entity.GetComponent<UniversalTimer>();
    }

    protected Player player;
    protected Rigidbody2D rb;
    protected UniversalTimer timer;

    protected string idleWaitTime = "idleWaitTime";
    

    public override void EnterState()
    {
        base.EnterState();
        //Debug.Log(StateKey);
        player.Anim.Play(StateKey.ToString());
    }
    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        player.Anim.Play(StateKey.ToString());
    }

    public override void ExitState()
    {
        base.ExitState();
        //player.Anim.SetBool(StateKey.ToString(), false);
    }
}
