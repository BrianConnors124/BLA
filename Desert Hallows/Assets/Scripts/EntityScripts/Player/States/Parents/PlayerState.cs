using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerState : State<PlayerStateMachine.EPlayerState>
{
    
    public PlayerState(PlayerStateMachine.EPlayerState key, Player entity) : base(key)
    {
        player = entity;
        rb = player.GetComponent<Rigidbody2D>();
        timer = entity.GetComponent<UniversalTimer>();
        playerController = entity.GetComponent<InputSystemController>();
        quest = player.GetComponent<MiniQuestCompletion>();
    }

    [Header("AttackInfo")] 
    protected int attackInt = 0;
    protected string attackKey = "attackPhase";

    protected Player player;
    protected Rigidbody2D rb;
    protected UniversalTimer timer;
    protected InputSystemController playerController;

    protected string idleWaitTime = "idleWaitTime";

    protected MiniQuestCompletion quest;
    

    public override void EnterState()
    {
        base.EnterState();
        //Debug.Log(StateKey);
        player.Anim.Play(StateKey.ToString());
    }
    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (player.takingDamage) return PlayerStateMachine.EPlayerState.takingDamage;
        throw new System.NotImplementedException();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(!animEnded) player.Anim.Play(StateKey.ToString());
    }

    public override void DoAttack()
    {
        
        player.playerAudio.clip = player.soundEffect["Slash"];
        player.playerAudio.Play();
        var ecd = BoxCastDrawer.BoxCastAllAndDraw(new Vector2(player.transform.position.x + player.MovementDirection()/1.4f, player.transform.position.y),new Vector2(player.transform.localScale.x * player.MovementDirection(),player.transform.localScale.y), 0, new Vector2(1, 0),0, LayerMask.GetMask("Enemy"), 0.3f);
        var bcd = BoxCastDrawer.BoxCastAllAndDraw(new Vector2(player.transform.position.x + player.MovementDirection()/1.4f, player.transform.position.y),new Vector2(player.transform.localScale.x * player.MovementDirection(),player.transform.localScale.y), 0, new Vector2(1, 0),0, LayerMask.GetMask("Boss"), 0.3f);
        foreach (var enemies in ecd)
        {
            enemies.collider.gameObject.GetComponent<Enemy>().ReceiveDamage(player.damage, player.knockBack/1.4f, player.stun, player.MovementDirection());
            //Debug.Log(enemies);
        }
        
        foreach (var boss in bcd)
        {
            boss.collider.gameObject.GetComponent<BossController>().ReceiveDamage(player.damage);
            //Debug.Log(enemies);
        }
        
    }

    protected void DoDashAttack(Vector2 startingPos, Vector2 endingPos)
    {
        float changeInDistance = endingPos.x - startingPos.x;
        
        var bcd = BoxCastDrawer.BoxCastAllAndDraw(startingPos ,new Vector2(0.1f ,player.transform.localScale.y), 0, new Vector2(player.MovementDirection(), 0),changeInDistance * player.MovementDirection(), LayerMask.GetMask("Enemy"), 5f);
        
        foreach (var enemies in bcd)
        {
            enemies.collider.gameObject.GetComponent<Enemy>().ReceiveDamage(player.damage * 2, player.knockBack * 1.5f, player.stun, -200);
        }
    }

    protected void DoSlamAttack(float maxVelo)
    {
        var bcd = BoxCastDrawer.BoxCastAllAndDraw(new Vector2(rb.position.x, rb.position.y - .5f), new Vector2(player.hitBox.x * 10, player.hitBox.y * 2), 0, Vector2.down, 0, LayerMask.GetMask("Enemy"), 5f);
        foreach (var a in bcd)
        {
            a.collider.gameObject.GetComponent<Enemy>().ReceiveDamage((int)maxVelo * player.damage * .05f, player.knockBack * 1.5f, player.stun, -100);
        }
    }

    public override void WalkingSound()
    {
        player.playerAudio.clip = player.soundEffect["Walking"];
        
        player.playerAudio.Play();
    }

    public override void TakeDamage()
    {
        player.playerAudio.volume = player.volume;
        player.playerAudio.clip = player.soundEffect["TakeDamage"];
        player.playerAudio.Play();
    }
    
    public override void DashSound()
    {
        
        player.playerAudio.clip = player.soundEffect["Dash"];
        player.playerAudio.Play();
    }
    

    public override void ExitState()
    {
        base.ExitState();
        //player.Anim.SetBool(StateKey.ToString(), false);
    }
}
