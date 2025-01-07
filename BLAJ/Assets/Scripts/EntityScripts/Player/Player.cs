using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
{
    [Header("Controllers")]
    public PlayerStateMachine _stateMachine;
    public PlayerInfo playerInfo;
    
    [Header("Input / Info")]
    public float dashCD;
    public float attackCD = -1;
    public float dashDuration;
    public float recentDamage;
    public float recentKnockBack;
    public float recentStun;
    public bool takingDamage;
    public bool canTakeDamage;
    public Vector2 enemyLocation; 
    public float KnockBackDirection => transform.position.x - enemyLocation.x;
    

    [Header("Stats")] 
    public float health;
    public float doubleJumps;
    public float dashSpeed;
    public float movementSpeed;
    public float jumpHeight;
    public float playerDamage;
    public float playerKnockBack;
    public float playerStun;
    
    
    
    protected override void Awake()
    {
        base.Awake();
        _stateMachine = GetComponent<PlayerStateMachine>();
        _stateMachine.Initialize(this, _rb);
        SetPresets();
        _rb.gravityScale = playerInfo.gravityScale;
    }

    private void SetPresets()
    {
        doubleJumps = playerInfo.doubleJumps + 1;
        dashCD = playerInfo.dashCD;
        movementSpeed = playerInfo.movementSpeed;
        dashSpeed = playerInfo.dashSpeed;
        dashDuration = playerInfo.dashDuration;
        jumpHeight = playerInfo.jumpHeight;
        playerDamage = playerInfo.baseDamage;
        playerKnockBack = playerInfo.baseKnockBack;
        playerStun = playerInfo.stun;
        health = playerInfo.health;
    }

    public void ReceiveDamage(float damage, float knockBack, float stun, Vector2 location)
    {
        if (!canTakeDamage) return;
        health -= damage;
        if(health <= 0)Die();
        recentKnockBack = knockBack;
        recentStun = stun;
        takingDamage = true;
        enemyLocation = location;
        print("DamageReceived");
    }

    private void Die()
    {
        print("Player Has Died");
        health = playerInfo.health;
    }

    #region Cooldowns
    protected void FixedUpdate()
    {
        if (Grounded())
        {
            coyoteJump = playerInfo.coyoteJump;
            doubleJumps = playerInfo.doubleJumps + 1;
        }
        else
        {
            coyoteJump -= Time.deltaTime;
        }
            
        if(!AttackReady())
            attackCD -= Time.deltaTime;
        if(!DashReady())
            dashCD -= Time.deltaTime;
        
        Flip();
    }
    public void StartAttackCD() => attackCD = playerInfo.attackCD;
    

    public bool AttackReady() => attackCD <= 0;
    
    public void StartDashCD() => dashCD = playerInfo.dashCD;
    
    public bool DashReady() => dashCD <= 0;
    
    #endregion

    #region Misc

    public override void Flip()
    {
        if (InputSystemController.MovementInput().x > 0) sprite.flipX = false;
        if (InputSystemController.MovementInput().x < 0) sprite.flipX = true;
    }

    public void Move(float x, float y)
    {
        _rb.velocity = new Vector2(x, y);
    }

    #endregion
}
[CreateAssetMenu(menuName = "Players/NewPlayer", fileName = "NewPlayer")]
public class PlayerInfo : ScriptableObject
{
    public float movementSpeed;
    public float dashSpeed;
    public float jumpHeight;
    public float attackCD;
    public float dashCD;
    public float dashDuration;
    public float doubleJumps;
    public float coyoteJump;
    public float gravityScale;
    public float baseDamage;
    public float baseKnockBack;
    public float stun;
    public float health;
}
