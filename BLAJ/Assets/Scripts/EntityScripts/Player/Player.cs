using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
{
    [Header("Controllers")]
    public PlayerStateMachine _stateMachine;
    public PlayerInfo playerInfo;
    public UniversalTimer timer;

    [Header("Input / Info")]
    //public float dashCD;
    //public float attackCD;

    public List<string> cooldownKey;
    private Dictionary<string, float> coolDowns;
    
    public Vector2 speed;
    public float dashDuration;
    public float recentDamage;
    public float recentKnockBack;
    public float recentStun;
    public bool takingDamage;
    public bool canTakeDamage;
    public Vector2 enemyLocation; 
    public bool canFlip = true;
    public float KnockBackDirection => transform.position.x - enemyLocation.x;
    

    [Header("Stats")] 
    public float health;
    public float maxHealth;
    public float playerDamage;
    public float playerKnockBack;
    public float playerStun;
    
    public float doubleJumps;
    public float dashSpeed;
    public float movementSpeed;
    public float jumpHeight;


    public bool doneLoading;
    
    
    protected override void Awake()
    {
        base.Awake();
        _stateMachine = GetComponent<PlayerStateMachine>();
        _stateMachine.Initialize(this, _rb);
        _rb.gravityScale = playerInfo.gravityScale;
        timer = GetComponent<UniversalTimer>();
        coolDowns = new Dictionary<string, float>();
        cooldownKey = new List<string>();
        cooldownKey.Add("attackCD");
        cooldownKey.Add("dashCD");
        doneLoading = true;
        SetPresets();
    }

    private void SetPresets()
    {
        doubleJumps = playerInfo.doubleJumps + 1;
        coolDowns.Add(cooldownKey[0], playerInfo.attackCD);
        coolDowns.Add(cooldownKey[1], playerInfo.dashCD);
        movementSpeed = playerInfo.movementSpeed;
        dashSpeed = playerInfo.dashSpeed;
        dashDuration = playerInfo.dashDuration;
        jumpHeight = playerInfo.jumpHeight;
        playerDamage = playerInfo.baseDamage;
        playerKnockBack = playerInfo.baseKnockBack;
        playerStun = playerInfo.stun;
        health = playerInfo.health;
        maxHealth = playerInfo.health;
        
    }

    public Dictionary<string, float> GetCoolDowns() => coolDowns;

    public UniversalTimer GetUniversalTimer() => timer;

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
        speed = Velocity;
        if (Grounded())
        {
            coyoteJump = playerInfo.coyoteJump;
            doubleJumps = playerInfo.doubleJumps + 1;
        }
        else
        {
            coyoteJump -= Time.deltaTime;
        }
        
        if(canFlip) Flip();
    }
    public void StartAttackCD() => timer.SetTimer(cooldownKey[0], coolDowns[cooldownKey[0]]);
    
    public void StartDashCD() => timer.SetTimer(cooldownKey[1], coolDowns[cooldownKey[1]]);
    public bool AttackReady() => !timer.TimerActive(cooldownKey[0]);
    
    
    public bool DashReady() => !timer.TimerActive(cooldownKey[1]);
    
    #endregion

    #region Misc

    protected override void Flip()
    {
        if (rb.velocityX > 0.1f) sprite.flipX = false;
        if (rb.velocityX < -0.1f) sprite.flipX = true;
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
