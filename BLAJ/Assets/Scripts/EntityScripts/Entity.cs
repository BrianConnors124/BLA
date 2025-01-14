using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Stats")]
    public float damage;
    public float knockBack;
    public float stun;
    
    
    [Header("info")]
    public float health;
    public float maxHealth;
    public float recentDamage;
    public float recentKnockBack;
    public int knockBackDirection;
    public float recentStun;
    public UniversalTimer timer;
    
    protected static List<GameObject> damageNumber;
    
    public bool takingDamage;
    public bool canTakeDamage;
    protected Rigidbody2D _rb;
    public SpriteRenderer sprite;
    public Animator Anim;
    public Vector2 hitBox;
    public float coyoteJump;

    protected Rigidbody2D rb => _rb;
    protected ObjectPuller pull;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        timer = GetComponent<UniversalTimer>();
        damageNumber = new List<GameObject>();
        Anim = GetComponent<Animator>();
        hitBox = GetComponent<BoxCollider2D>().size;
        hitBox *= transform.localScale;
        sprite = GetComponent<SpriteRenderer>();
        pull = new ObjectPuller();
    }

    public void ZeroVelocity() => _rb.velocity = Vector2.zero;
    public void SetVelocity(Vector2 newVelocity) => _rb.velocity = newVelocity;
    public virtual void Move(float x, float y)
    {
        _rb.velocity = new Vector2(x, y);
    }
    public Vector2 Velocity => _rb.velocity;
    
    
    public virtual void ReceiveDamage(float damage, float knockBack, float stun, int direction)
    {
        health -= damage;
        pull.PullObjectAndSetText(damageNumber, transform.position, "" + damage);
        
        if(health <= 0)Die();
        recentKnockBack = knockBack;
        recentStun = stun;
        takingDamage = true;
        knockBackDirection = direction;
    }
    
    protected virtual void Die()
    {
        Destroy(gameObject);
    }
    
    public float Direction(float a)
    {
        if (a != 0) 
            return Mathf.Abs(a) / a;
        return 0;
    }

    public int MovementDirection() => sprite.flipX ? -1 : 1;
    protected virtual void Flip()
    {
        
        if (_rb.velocityX > 0.1f) sprite.flipX = false;
        if (_rb.velocityX < -0.1f) sprite.flipX = true;
        
    }

    public int FacingDirectionInt()
    {
        return sprite.flipX ? -1 : 1;
    }
    public bool FacingDirection() => sprite.flipX;
    

    public bool Grounded() => BoxCastDrawer.BoxCastAndDraw(new Vector2(transform.position.x, transform.position.y - hitBox.y * 0.55f), new Vector2(hitBox.x - 0.1f, 0.2f), 0,
        Vector2.down, 0, LayerMask.GetMask("WorldObj"));

    public bool IsTouchingGround() => coyoteJump > 0 || Grounded();
    public bool CloseToGround() => BoxCastDrawer.BoxCastAndDraw(new Vector2(transform.position.x, transform.position.y - hitBox.y * 0.81f), new Vector2(hitBox.x - 0.1f, 1f), 0,
        Vector2.down, 0, LayerMask.GetMask("WorldObj"));
}
