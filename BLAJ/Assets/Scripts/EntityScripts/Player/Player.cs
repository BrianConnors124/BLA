using System;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
{
    [Header("Controllers")]
    public PlayerInfo playerInfo;
    public PlayerStateMachine _stateMachine;
    public UniversalTimer timer;

    [Header("Input / Info")]
    //public float dashCD;
    //public float attackCD;

    public List<string> cooldownKey;
    public Dictionary<string, float> coolDowns;
    
    public Vector2 speed;
    public float dashDuration; 
    public bool canFlip = true;
    

    [Header("Stats")] 
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
        GetComponent<Animator>().runtimeAnimatorController = playerInfo.playerAnimator;
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

    public override void ReceiveDamage(float damage, float knockBack, float stun, int direction)
    {
        if (!canTakeDamage) return;
        base.ReceiveDamage(damage,knockBack,stun,direction);
    }

    protected override void Die()
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
    public float movementSpeed = 8;
    public float dashSpeed = 64;
    public float jumpHeight = 24;
    public float attackCD = .5f;
    public float dashCD = 1;
    public float dashDuration = 0.08f;
    public float doubleJumps = 1;
    public float coyoteJump = 0.2f;
    public float gravityScale = 10;
    public float baseDamage = 10;
    public float baseKnockBack = 12;
    public float stun = 0.5f;
    public float health = 100;
    public AnimatorController playerAnimator;
}
