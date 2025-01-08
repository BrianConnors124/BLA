using System;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Player_AttackState : PlayerState
{
    //private RaycastHit2D[] bcd;
    private Transform _transform;
    private Vector2 hitBox;
    
    public Player_AttackState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D RB) : base(key, entity)
    {
        _transform = entity.transform;
        hitBox = entity.hitBox;
    }
    public override void EnterState()
    {
        base.EnterState();
        player.canFlip = false;
        player.canTakeDamage = false;
        stateTimer = 0.005f;
        player.Move(player.movementSpeed * InputSystemController.MovementInput().x / 1.3f, rb.velocityY);
        // bcd = BoxCastDrawer.BoxCastAllAndDraw(new Vector2(_transform.position.x + hitBox.x * 1.5f * player.FacingDirection(), _transform.position.y),
        //     new Vector2(_transform.localScale.x, _transform.localScale.y) * 0.5f, 0, Vector2.right, 0,
        //     LayerMask.GetMask("Enemy"));
        
        var bcd = BoxCastDrawer.BoxCastAllAndDraw(new Vector2(player.transform.position.x + player.FacingDirection(), player.transform.position.y),new Vector2(player.transform.localScale.x/2,player.transform.localScale.y), 0, new Vector2(player.FacingDirection(), 0),0, LayerMask.GetMask("Enemy"), 0.3f);
        
        foreach(var enemies in bcd) enemies.collider.gameObject.GetComponent<Enemy>().ReceiveDamage(player.playerDamage,player.playerKnockBack, player.playerStun);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        player.Move(player.movementSpeed * InputSystemController.MovementInput().x / 1.3f, rb.velocityY);
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (animEnded)
        {
            if (player.Grounded())
            {
                return PlayerStateMachine.EPlayerState.transferGround;
            }

            return PlayerStateMachine.EPlayerState.falling;
        }
        
         
        return StateKey;
    }

    public override void ExitState()
    {
        base.ExitState();
        player.canFlip = true;
        player.canTakeDamage = true;
        
    }
}
