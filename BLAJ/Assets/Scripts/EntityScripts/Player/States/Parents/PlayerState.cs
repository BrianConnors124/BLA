using System;
using UnityEngine;

public class PlayerState : State<PlayerStateMachine.EPlayerState>
{
    public PlayerState(PlayerStateMachine.EPlayerState key, Player entity) : base(key)
    {
        player = entity;
        transform = player.transform;
        rb = player.GetComponent<Rigidbody2D>();
    }

    protected Player player;
    private Transform transform;
    private Rigidbody2D rb;

    public override void EnterState()
    {
        base.EnterState();
        Console.WriteLine(StateKey);
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
        //Debug.Log(AttackReady() + " " + attackCD + " " + Time.deltaTime);
    }

    public override void ExitState()
    {
        base.ExitState();
        //player.Anim.SetBool(StateKey.ToString(), false);
    }
}
