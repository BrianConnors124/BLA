using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class Enemy : Entity
{
    [Header("Info")]
    public float startingXPos;
    public EnemyStateMachine _stateMachine;
    public GameObject player;
    public EnemyInfo info;
    public Vector2 origin;
    private BoxCollider2D hitbox;

    [Header("Input")] 
    public float movementSpeed;
    public float jumpHeight;
    public float reach;
    public float primaryCD;
    
    [Header("Output")]
    public bool playerInPursuitRange;
    public bool playerInMeleeRange;
    public bool SimilarX => Math.Abs(transform.position.x - player.transform.position.x) < transform.localScale.x * hitbox.size.x * 1.4f;
    public bool returned => Math.Abs(transform.position.x - origin.x) < 0.3f;
    public bool canAttack = true;
    
    public bool longRangeAttackReady = true;
    public bool playerInLongRange;
    public bool hasALongRangeAttack;
    

    protected override void Awake()
    {
        GetComponent<Animator>().runtimeAnimatorController = info.anim;
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
        reach = info.baseReach / transform.localScale.x;
        damage = info.damage;
        stun = info.stun;
        knockBack = info.knockBack;
        primaryCD = info.primaryCD;
        health = info.health;
    }

    private void Update()
    {
        var position = transform.position;
        
        if(!takingDamage) Flip();

        #region Detection

        playerInMeleeRange = BoxCastDrawer.BoxCastAndDraw(new Vector2(position.x + .4f * MovementDirection(), position.y),
            new Vector2(transform.localScale.x * hitbox.size.x * 1.4f, transform.localScale.y * 1.1f), 0, Vector2.right, 0,
            LayerMask.GetMask("Player"));


        #endregion
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
    
        RaycastHit2D a = BoxCastDrawer.BoxCastAndDraw(
        new Vector2(
            transform.position.x + (GetComponent<BoxCollider2D>().size.x * transform.localScale.x * MovementDirection()),
            transform.position.y), new Vector2(transform.localScale.x * .5f, transform.localScale.y * .94f), 0,
        Vector2.right, 0, LayerMask.GetMask("WorldObj") + LayerMask.GetMask("World"), 0.001f);
        return a; 
    }

    public RaycastHit2D ObjectTooHigh() => Line.CreateAndDraw(
        new Vector2(
            transform.position.x +
            (GetComponent<BoxCollider2D>().size.x * transform.localScale.x * MovementDirection()),
            transform.position.y + transform.localScale.y / 5), new Vector2(MovementDirection(), 0),
        transform.localScale.x * .35f, LayerMask.GetMask("WorldObj") + LayerMask.GetMask("World"), Color.gray);
    
    public RaycastHit2D ObjectForwardTooClose(){
    
        RaycastHit2D a = BoxCastDrawer.BoxCastAndDraw(
            new Vector2(
                transform.position.x + (GetComponent<BoxCollider2D>().size.x * transform.localScale.x * MovementDirection()),
                transform.position.y), new Vector2(transform.localScale.x * .1f, transform.localScale.y * .94f), 0,
            Vector2.right, 0, LayerMask.GetMask("WorldObj") + LayerMask.GetMask("World"), 0.001f);
        return a; 
    }
    public RaycastHit2D DetectsObjectBackwards(){
        RaycastHit2D a = BoxCastDrawer.BoxCastAndDraw(
            new Vector2(
                transform.position.x + (GetComponent<BoxCollider2D>().size.x * transform.localScale.x * -MovementDirection()),
                transform.position.y), new Vector2(transform.localScale.x, transform.localScale.y * .94f), 0,
            Vector2.right, 0, LayerMask.GetMask("WorldObj"), 0.001f); 
        return a; 
    }
    #endregion
    
    #region Misc
          
    public int PlayerDirection()
    {
        if (player.transform.position.x - transform.position.x < 0)
        { 
            sprite.flipX = true; 
            return -1;
        }
        
        sprite.flipX = false;
        return 1;
    }
          
    public RaycastHit2D ThereIsAFloor() => Line.CreateAndDraw(new Vector2(transform.position.x + reach * MovementDirection(), transform.position.y), Vector2.down, transform.localScale.y * 1.4f, LayerMask.GetMask("WorldObj") + LayerMask.GetMask("World"), Color.red);
          
    #endregion


    

}
