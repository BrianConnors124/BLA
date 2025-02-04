using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [Header("Controllers")]
    public PlayerInfo playerInfo;
    public PlayerStateMachine _stateMachine;
    public InputSystemController playerController;

    [Header("Input / Info")]
    

    public List<string> cooldownKey;
    public Dictionary<string, float> coolDowns;
    
    public Vector2 speed;
    public float dashDuration; 
    public bool canFlip = true;
    

    [Header("Stats")] 
    
    
    public float doubleJumps;
    public float dashSpeed;
    public float movementSpeed;
    public float jumpHeight;


    public bool doneLoading;
    //public bool hasDash, hasDashAttack, hasSlamAttack;
    public bool[] unlockables;

    [Header("Sound Effects")] 
    public string[] soundKey;
    public AudioClip[] sound;
    public Dictionary<string, AudioClip> soundEffect;
    public AudioSource playerAudio;
    public float volume;
    
    
    protected override void Awake()
    {
        base.Awake();
        _stateMachine = GetComponent<PlayerStateMachine>();
        _stateMachine.Initialize(this, _rb);
        _rb.gravityScale = playerInfo.gravityScale;
        coolDowns = new Dictionary<string, float>();
        cooldownKey = new List<string>();
        cooldownKey.Add("attackCD");
        cooldownKey.Add("dashCD");
        cooldownKey.Add("superAttackCD");
        SetPresets();
        playerController = GetComponent<InputSystemController>();
        doneLoading = true;
        InitializeSound();
    }
    private void InitializeSound()
    {
        volume = playerAudio.volume;
        soundEffect = new Dictionary<string, AudioClip>();
        for (var i = 0; i < sound.Length; i++)
        {
            soundEffect.Add(soundKey[i], sound[i]);
        }
    }

    private void SetPresets()
    {
        //0 => dash, 1 => dashAttack, 2 => slamAttack//
        unlockables = new bool[3];
        doubleJumps = playerInfo.doubleJumps + 1;
        coolDowns.Add(cooldownKey[0], playerInfo.attackCD);
        coolDowns.Add(cooldownKey[1], playerInfo.dashCD);
        coolDowns.Add(cooldownKey[2], playerInfo.superAttackCD);
        movementSpeed = playerInfo.movementSpeed;
        dashSpeed = playerInfo.dashSpeed;
        dashDuration = playerInfo.dashDuration;
        jumpHeight = playerInfo.jumpHeight;
        damage = playerInfo.baseDamage;
        knockBack = playerInfo.baseKnockBack;
        stun = playerInfo.stun;
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
    public bool AttackReady() => !timer.TimerActive(cooldownKey[0]);
    
    public void StartDashCD() => timer.SetTimer(cooldownKey[1], coolDowns[cooldownKey[1]]);
    public bool DashReady() => !timer.TimerActive(cooldownKey[1]);
    
    public void StartSuperAttackCD() => timer.SetTimer(cooldownKey[2], coolDowns[cooldownKey[2]]);
    public bool SuperAttackReady() => !timer.TimerActive(cooldownKey[2]);
    
    #endregion

    #region Misc

    protected override void Flip()
    {
        if (InputSystemController.MovementInput().x > 0.1f) sprite.flipX = false;
        if (InputSystemController.MovementInput().x < -0.1f) sprite.flipX = true;
    }

    #endregion
}
