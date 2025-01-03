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
    public GameObject player;
    public EnemyInfo info;
    public Vector2 origin;

    [Header("Input")] 
    public float movementSpeed;
    public float jumpHeight;
    public float reach;
    public float damage;
    public float stun;
    public float knockback;
    public float primaryCD;
    public float health;
    
    [Header("Output")]
    public bool returned;
    public bool playerInPursuitRange;
    public bool playerInAttackRange;
    public bool canAttack = true;
    public bool takingDamage;
    public float recentKnockBack;
    public float recentStun;

    protected override void Awake()
    {
        base.Awake();
        startingXPos = transform.position.x;
        _stateMachine = GetComponent<EnemyStateMachine>();
        _stateMachine.Initialize(this, _rb);
        SetPresets();
        origin = _rb.transform.position;
    }

    private void SetPresets()
    {
        movementSpeed = info.movementSpeed;
        jumpHeight = info.jumpHeight;
        reach = info.baseReach / transform.localScale.x;
        damage = info.damage;
        stun = info.stun;
        knockback = info.knockBack;
        primaryCD = info.primaryCD;
        health = info.health;
    }

    private void Update()
    {
        var position = transform.position;
        
        if(!takingDamage) Flip();
        returned = position.x < origin.x + .3f && position.x > origin.x - .3f;

        #region Detection

        var player = Physics2D.CircleCast(position, 8.85f, Vector2.left, 0, LayerMask.GetMask("Player"));
        playerInPursuitRange = player;
        playerInAttackRange = Physics2D.CircleCast(position, reach, Vector2.left, 0, LayerMask.GetMask("Player"));
        this.player = player.collider.GameObject();


        #endregion
    }

    public void ReceiveDamage(float damage, float knockBack, float stun)
    {
        health -= damage;
        if(health <= 0)Die();
        recentKnockBack = knockBack;
        recentStun = stun;
        takingDamage = true;
    }

    private void Die()
    {
     Destroy(gameObject);   
    }






#region Sight

    public RaycastHit2D PlayerOutOfSight() => Line.CreateAndDraw(transform.position, player.transform.position - transform.position, Line.Length(transform.position,player.transform.position),LayerMask.GetMask("WorldObj"), Color.black);
    public RaycastHit2D DetectsObjectForward(){
    
        RaycastHit2D a = BoxCastDrawer.BoxCastAndDraw(
        new Vector2(
            transform.position.x + (GetComponent<BoxCollider2D>().size.x * transform.localScale.x * MovementDirection()),
            transform.position.y), new Vector2(transform.localScale.x * .5f, transform.localScale.y * .94f), 0,
        Vector2.right, 0, LayerMask.GetMask("WorldObj"), 0.001f);
        return a; 
    }

    public RaycastHit2D ObjectTooHigh() => Line.CreateAndDraw(
        new Vector2(
            transform.position.x +
            (GetComponent<BoxCollider2D>().size.x * transform.localScale.x * MovementDirection()),
            transform.position.y + transform.localScale.y / 5), new Vector2(MovementDirection(), 0),
        transform.localScale.x * .5f);
    
    public RaycastHit2D ObjectForwardTooClose(){
    
        RaycastHit2D a = BoxCastDrawer.BoxCastAndDraw(
            new Vector2(
                transform.position.x + (GetComponent<BoxCollider2D>().size.x * transform.localScale.x * MovementDirection()),
                transform.position.y), new Vector2(transform.localScale.x * .1f, transform.localScale.y * .94f), 0,
            Vector2.right, 0, LayerMask.GetMask("WorldObj"), 0.001f);
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
        GetComponent<SpriteRenderer>().flipX = true;
        return -1;
    }
    GetComponent<SpriteRenderer>().flipX = false;
    return 1;
}
public int MovementDirection()
{
    if (GetComponent<SpriteRenderer>().flipX)
    {
        return -1;
    }
    return 1;
}



#endregion
}

[CreateAssetMenu(menuName = "Enemies/New Enemy", fileName = "New Enemy")]
public class EnemyInfo : ScriptableObject
{
    public string name;
    public string description;
    public float health;
    public float jumpHeight;
    public float movementSpeed;
    public float damage;
    public float baseReach;
    public float stun;
    public float knockBack;
    public float primaryCD;
    public bool bossType;
}
