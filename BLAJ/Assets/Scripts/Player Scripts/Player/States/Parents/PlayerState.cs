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
    private bool currentFlip;

    public override void EnterState()
    {
        base.EnterState();
        Console.WriteLine(StateKey);
        player.Anim.Play(StateKey.ToString());
        player.GetComponent<SpriteRenderer>().flipX = player.Flip(rb, currentFlip);
    }
    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        player.GetComponent<SpriteRenderer>().flipX = player.Flip(rb, currentFlip);
        player.Anim.Play(StateKey.ToString());
    }

    public override void ExitState()
    {
        base.ExitState();
        //player.Anim.SetBool(StateKey.ToString(), false);
    }
    
    //private bool IsTouchingGround() => BoxCastDrawer.BoxCastAndDraw(player.transform.position - new Vector3(0, player.transform.localScale.y, 0), new Vector2())
}
