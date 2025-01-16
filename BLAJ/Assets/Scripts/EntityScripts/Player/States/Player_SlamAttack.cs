using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_SlamAttack : PlayerState
{
    public Player_SlamAttack(PlayerStateMachine.EPlayerState key, Player entity) : base(key, entity)
    {
        
    }

    private bool attacked;
    private float maxVelo;
    public override void EnterState()
    {
        base.EnterState();
        attacked = false;
        rb.gravityScale *= 3;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        RaycastHit2D[] bcd;
        //Debug.Log(player.CloseToGround());
        if (-player.Velocity.y > maxVelo) maxVelo = -player.Velocity.y;
        if (player.CloseToGround() && !attacked)
        {
            bcd = BoxCastDrawer.BoxCastAllAndDraw(new Vector2(rb.position.x, rb.position.y - .5f), new Vector2(player.hitBox.x * 10, player.hitBox.y * 2), 0, Vector2.down, 0, LayerMask.GetMask("Enemy"), 5f);
            foreach (var a in bcd)
            {
                a.collider.gameObject.GetComponent<Enemy>().ReceiveDamage((int)maxVelo * player.damage * .33f, player.knockBack * 1.5f, player.stun, -100);
                //Debug.Log(a.collider.name);
            }
            attacked = true;
        }
        
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (player.IsTouchingGround() && attacked) return PlayerStateMachine.EPlayerState.idle;
        return StateKey;
    }

    public override void ExitState()
    {
        base.ExitState();
        player.StartSuperAttackCD();
        rb.gravityScale = player.playerInfo.gravityScale;
        maxVelo = 0;
    }
}
