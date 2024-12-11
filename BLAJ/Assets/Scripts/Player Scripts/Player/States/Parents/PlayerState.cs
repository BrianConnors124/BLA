using System;
using UnityEngine;

public class PlayerState : State<PlayerStateMachine.EPlayerState>
{

    public PlayerState(PlayerStateMachine.EPlayerState key, Player entity) : base(key)
    {
        player = entity;
        transform = player.transform;
    }

    protected Player player;
    private Transform transform;

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
    }

    public override void ExitState()
    {
        base.ExitState();
        //player.Anim.SetBool(StateKey.ToString(), false);
    }
    
    //private bool IsTouchingGround() => BoxCastDrawer.BoxCastAndDraw(player.transform.position - new Vector3(0, player.transform.localScale.y, 0), new Vector2())
}
