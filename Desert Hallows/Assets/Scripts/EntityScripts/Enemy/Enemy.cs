using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Entity
{
    [Header("Info")]
    public float startingXPos;
    public EnemyStateMachine _stateMachine;
    public EnemyInfo info;
    public Vector2 origin;
    private BoxCollider2D hitbox;
    public bool canJump;

    [Header("Input")] 
    public float movementSpeed;
    public float jumpHeight;
    public float reach;
    public float aoe;
    public float primaryCD;
    
    [Header("Output")]
    public bool playerInPursuitRange;
    public bool playerInMeleeRange => BoxCastDrawer.BoxCastAndDraw(transform.position, new Vector2(.01f, 
            transform.localScale.y * 1.1f), 0, new Vector2(MovementDirection(), 0), reach * .75f,
        LayerMask.GetMask("Player"));
    public bool SimilarX => Math.Abs(transform.position.x - player.transform.position.x) < hitbox.size.x + reach * .8f;
    public bool returned => Math.Abs(transform.position.x - origin.x) < 0.3f;
    public bool canAttack = true;
    
    public bool longRangeAttackReady = true;
    public bool playerInLongRange;
    public bool hasAMeleAttack;
    public bool hasALongRangeAttack;
    

    protected override void Awake()
    {
        base.Awake();
        startingXPos = transform.position.x;
        _stateMachine = GetComponent<EnemyStateMachine>();
        _stateMachine.Initialize(this, _rb);
        SetPresets();
        origin = _rb.transform.position;
        hitbox = GetComponent<BoxCollider2D>();
    }

    private void SetPresets()
    {
        movementSpeed = info.movementSpeed;
        jumpHeight = info.jumpHeight;
        reach = info.baseReach;
        damage = info.damage;
        stun = info.stun;
        knockBack = info.knockBack;
        primaryCD = info.primaryCD;
        health = info.health;
        aoe = info.aoe;
    }

    private void Update()
    { 
        if(!takingDamage) Flip();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            playerInPursuitRange = true;
            timer.SetActionTimer("LongRangeAttack", 1, () => playerInLongRange = true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInPursuitRange = false;
            timer.RemoveTimer("LongRangeAttack");
            playerInLongRange = false;
        }
    }


    #region Sight

    public RaycastHit2D PlayerOutOfSight()
    {
        return player != null? Line.CreateAndDraw(transform.position, player.transform.position - transform.position, Line.Length(transform.position,player.transform.position),LayerMask.GetMask("WorldObj") + LayerMask.GetMask("World"), Color.black) : new RaycastHit2D();
    }
    public RaycastHit2D DetectsObjectForward(){
    
        RaycastHit2D a = BoxCastDrawer.BoxCastAndDraw(transform.position, new Vector2(.1f, .94f * hitBox.y), 0,
        new Vector2(MovementDirection(), 0), reach * 1.5f, LayerMask.GetMask("WorldObj") + LayerMask.GetMask("World"));
        return a; 
    }
    
    public RaycastHit2D DetectsObjectBackwards(){
        RaycastHit2D a = BoxCastDrawer.BoxCastAndDraw(
            transform.position, new Vector2(.1f, .94f * hitBox.y), 0,
            new Vector2(-MovementDirection(), 0), reach * 1.5f, LayerMask.GetMask("WorldObj")); 
        return a; 
    }

    public RaycastHit2D ObjectTooHigh() => Line.CreateAndDraw(
        new Vector2(transform.position.x, 
            transform.position.y + hitBox.y * .1f), new Vector2(MovementDirection(), 0),
        reach * 1.5f, LayerMask.GetMask("WorldObj") + LayerMask.GetMask("World"), Color.red);
    
    public RaycastHit2D ObjectForwardTooClose(){
    
        RaycastHit2D a = BoxCastDrawer.BoxCastAndDraw(
            transform.position, new Vector2(.1f, hitBox.y * .94f), 0,
            new Vector2(MovementDirection(), 0), reach, LayerMask.GetMask("WorldObj") + LayerMask.GetMask("World"));
        return a; 
    }
    #endregion
    
    #region Misc
          
    public int PlayerDirection()
    {
        if (player.transform.position.x - transform.position.x < 0)
        {
            transform.localScale = new Vector2(Math.Abs(transform.localScale.x) * -1, transform.localScale.y);
            return -1;
        }
        transform.localScale = new Vector2(Math.Abs(transform.localScale.x), transform.localScale.y);
        return 1;
    }
          
    public RaycastHit2D ThereIsAFloor() => Line.CreateAndDraw(new Vector2(transform.position.x + reach * MovementDirection() * .95f, transform.position.y), Vector2.down, transform.localScale.y,LayerMask.GetMask("World"), Color.yellow);
          
    #endregion


    

}
