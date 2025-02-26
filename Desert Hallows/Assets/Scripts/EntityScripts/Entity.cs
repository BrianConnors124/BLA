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

    [Header("DamageOBJ")] 
    public GameObject controller;

    public ObjectLists objPuller;
    public bool dead;
    public Action onDeath;
    public Action onTakeDamage;
    
    
    
    
    [Header("info")]
    public float health;
    public float maxHealth;
    public bool canBeStunned;
    public float recentKnockBack;
    public int knockBackDirection;
    public float recentStun;
    public UniversalTimer timer;
    public bool takingDamage;
    public bool canTakeDamage = true;
    protected Rigidbody2D _rb;
    public SpriteRenderer sprite;
    public Animator Anim;
    public Vector2 hitBox;
    public float coyoteJump;
    public GameObject player;
    public GameObject sheild;
    public Vector2 Location => _rb.position;
    

    
    public QuestPhases quest;

    protected virtual void Awake()
    {
        controller = GameObject.Find("Main Camera");
        _rb = GetComponent<Rigidbody2D>();
        timer = GetComponent<UniversalTimer>();
        Anim = GetComponent<Animator>();
        hitBox = GetComponent<BoxCollider2D>().size;
        hitBox *= transform.localScale;
        sprite = GetComponent<SpriteRenderer>();
        objPuller = controller.GetComponent<ObjectLists>();
        quest = GameObject.Find("QuestIsh").GetComponent<QuestPhases>();
    }

    public void ZeroVelocity() => _rb.velocity = Vector2.zero;
    public virtual void Move(float x, float y)
    {
        _rb.velocity = new Vector2(x * Time.timeScale, y * Time.timeScale);
    }
    public Vector2 Velocity => _rb.velocity;
    
    
    public virtual void ReceiveDamage(float damage, float knockBack, float stun, int direction)
    {
        if (canTakeDamage && !sheild)
        {
            health -= damage;
        
            ObjectPuller.PullObjectAndSetTextAndColor(controller.GetComponent<ObjectLists>().damageNumbers, transform.position, "" + (int) damage, Color.red);
        
            if(health <= 0)Die();

            if (canBeStunned)
            {
                recentKnockBack = knockBack;
                recentStun = stun;
                knockBackDirection = direction; 
            }
            takingDamage = true;
        }

        if (sheild)
        {
            sheild.GetComponent<SheildScript>().ReceiveDamage(damage);
        }
        onTakeDamage?.Invoke();
    }
    
    protected virtual void Die()
    {
        quest.RemoveObjective(gameObject);
        dead = true;
        GetComponent<BoxCollider2D>().enabled = false;
        _rb.constraints = RigidbodyConstraints2D.FreezePosition;
        onDeath?.Invoke();
    }

    public void DeathAnimFinished()
    {
        Destroy(gameObject);
    }

    public int MovementDirection() => transform.localScale.x > 0 ? 1 : -1;
    protected virtual void Flip()
    {
        if(_rb.velocityX > 0.01f)transform.localScale = new Vector2(Math.Abs(transform.localScale.x), transform.localScale.y);
        if(_rb.velocityX < -0.01f)transform.localScale = new Vector2(Math.Abs(transform.localScale.x) * -1, transform.localScale.y);
    }
    

    public bool Grounded() => BoxCastDrawer.BoxCastAndDraw(new Vector2(transform.position.x, transform.position.y - hitBox.y * 0.55f), new Vector2(hitBox.x - 0.05f, 0.2f), 0,
        Vector2.down, 0, LayerMask.GetMask("WorldObj") + LayerMask.GetMask("World"));

    public bool IsTouchingGround() => coyoteJump > 0 || Grounded();
    public bool CloseToGround() => BoxCastDrawer.BoxCastAndDraw(new Vector2(transform.position.x, transform.position.y - hitBox.y * 0.81f), new Vector2(hitBox.x - 0.1f, 1f), 0,
        Vector2.down, 0, LayerMask.GetMask("WorldObj") + LayerMask.GetMask("World"));
}
