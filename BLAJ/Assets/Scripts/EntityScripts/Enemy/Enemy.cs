using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Entity
{
    public float startingXPos;
    public EnemyStateMachine _stateMachine;
    public GameObject player;
    public EnemyInfo info;
    public Vector2 origin;
    
    
    public float movementSpeed;
    public float jumpHeight;
    
    public bool Returned;

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
    }

    private void Update()
    {
        Flip();
        Returned = _rb.transform.position.x < origin.x + .3f && _rb.transform.position.x > origin.x - .3f;
    }

    public void ReceiveDamage(float damage, float knockBack)
    {
        
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
                transform.position.y), new Vector2(transform.localScale.x * .5f, transform.localScale.y * .94f), 0,
            Vector2.right, 0, LayerMask.GetMask("WorldObj"), 0.001f); 
        return a; 
    }

#endregion
    
#region Range
private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player")) player = other.gameObject;
        
}

private void OnTriggerExit2D(Collider2D other)
{
    if (other.CompareTag("Player")) player = null;
}

public bool PlayerInRange() => player != null;
#endregion

#region Misc

private int PlayerDirection()
{
    if (!PlayerInRange()) return 0;
    
    if (transform.position.x < player.transform.position.x)
    { 
        return 1;
    }
    if (transform.position.x > player.transform.position.x)
    { 
        return -1;
    }

    return 0;

}

public int MovementDirection()
{
    if (GetComponent<SpriteRenderer>().flipX)
    {
        return -1;
    }
    return 1;
}

public override void Flip()
{
    base.Flip();
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
