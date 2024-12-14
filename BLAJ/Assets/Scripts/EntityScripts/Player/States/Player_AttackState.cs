using System;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Player_AttackState : PlayerState
{
    private Rigidbody2D rb;
    private RaycastHit2D[] bcd;
    private Transform _transform;
    private Vector2 hitBox;
    
    public Player_AttackState(PlayerStateMachine.EPlayerState key, Player entity, Rigidbody2D RB) : base(key, entity)
    {
        rb = RB;
        _transform = entity.transform;
        hitBox = entity.hitBox;
    }
    public override void EnterState()
    {
        base.EnterState();
        bcd = BoxCastDrawer.BoxCastAllAndDraw(new Vector2(_transform.position.x + hitBox.x * 1.5f, _transform.position.y),
            new Vector2(_transform.localScale.x, _transform.localScale.y) * 0.5f, 0, Vector2.right, 0,
            LayerMask.GetMask("Enemy"));
    }

    public override void UpdateState()
    {
        rb.velocity = new Vector2(player.movementSpeed * player.Direction(InputSystemController.MovementInput().x), rb.velocityY);
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if(animEnded){
            return PlayerStateMachine.EPlayerState.idle;
        }
         
        return StateKey;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
