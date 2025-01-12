using System;
using UnityEngine;

public class PlayerState : State<PlayerStateMachine.EPlayerState>
{
    public PlayerState(PlayerStateMachine.EPlayerState key, Player entity) : base(key)
    {
        player = entity;
        rb = player.GetComponent<Rigidbody2D>();
        timer = entity.GetComponent<UniversalTimer>();
        playerController = entity.GetComponent<InputSystemController>();
    }

    protected Player player;
    protected Rigidbody2D rb;
    protected UniversalTimer timer;
    protected InputSystemController playerController;

    protected string idleWaitTime = "idleWaitTime";
    

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log(StateKey);
        player.Anim.Play(StateKey.ToString());
    }
    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        playerController.GetInput();
        if(!animEnded) player.Anim.Play(StateKey.ToString());
    }

    public override void DoAttack()
    {
        var bcd = BoxCastDrawer.BoxCastAllAndDraw(new Vector2(player.transform.position.x + player.FacingDirection(), player.transform.position.y),new Vector2(player.transform.localScale.x/1.3f,player.transform.localScale.y), 0, new Vector2(player.FacingDirection(), 0),0, LayerMask.GetMask("Enemy"), 0.3f);
        foreach (var enemies in bcd)
        {
            enemies.collider.gameObject.GetComponent<Enemy>().ReceiveDamage(player.playerDamage,player.playerKnockBack, player.playerStun);
            Debug.Log(enemies);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
        //player.Anim.SetBool(StateKey.ToString(), false);
    }
}
