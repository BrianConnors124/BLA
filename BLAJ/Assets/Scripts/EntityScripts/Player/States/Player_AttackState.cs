using System;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Player_AttackState : PlayerState
{
    //private RaycastHit2D[] bcd;
    private Transform _transform;
    private Vector2 hitBox;
    private RaycastHit2D[] bcd;
    
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
        player.StartAttackCD();
        player.Move(player.movementSpeed * InputSystemController.MovementInput().x / 1.3f, rb.velocityY);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (startAttack)
        {
            bcd = BoxCastDrawer.BoxCastAllAndDraw(new Vector2(player.transform.position.x + player.FacingDirection(), player.transform.position.y),new Vector2(player.transform.localScale.x/1.3f,player.transform.localScale.y), 0, new Vector2(player.FacingDirection(), 0),0, LayerMask.GetMask("Enemy"), 0.3f);
            foreach (var enemies in bcd)
            {
                enemies.collider.gameObject.GetComponent<Enemy>().ReceiveDamage(player.playerDamage,player.playerKnockBack, player.playerStun);
                Debug.Log(enemies);
            }
            startAttack = false;
        }
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
