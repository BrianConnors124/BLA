using System;
using Unity.VisualScripting;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected Rigidbody2D _rb;
    protected SpriteRenderer sprite;
    protected Animator _anim;
    public Vector2 hitBox;
    public float coyoteJump;

    public Animator Anim => _anim;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponentInChildren<Animator>();
        hitBox = GetComponent<BoxCollider2D>().size;
        hitBox *= transform.localScale;
        sprite = GetComponent<SpriteRenderer>();
    }

    public void SetAnimState(string state ,bool value)
    {
        _anim.SetBool(state, value);
    }

    public void ZeroVelocity() => _rb.velocity = Vector2.zero;
    public void SetVelocity(Vector2 newVelocity) => _rb.velocity = newVelocity;
    public Vector2 Velocity => _rb.velocity;
    public float Direction(float a)
    {
        if (a != 0) 
            return Mathf.Abs(a) / a;
        return 0;
    }
    public virtual void Flip()
    {
        
        if (_rb.velocityX > 0.1f) sprite.flipX = false;
        if (_rb.velocityX < -0.1f) sprite.flipX = false;
        
    }

    public bool Grounded() => BoxCastDrawer.BoxCastAndDraw(new Vector2(transform.position.x, transform.position.y - hitBox.y * 0.55f), new Vector2(hitBox.x - 0.1f, 0.2f), 0,
        Vector2.down, 0, LayerMask.GetMask("WorldObj"));

    public bool IsTouchingGround() => coyoteJump > 0 || Grounded();
    public bool CloseToGround() => BoxCastDrawer.BoxCastAndDraw(new Vector2(transform.position.x, transform.position.y - hitBox.y * 0.81f), new Vector2(hitBox.x - 0.1f, 1f), 0,
        Vector2.down, 0, LayerMask.GetMask("WorldObj"));
}
