using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
{
    public PlayerStateMachine _stateMachine;
    public PlayerInfo playerInfo;
    
    public float dashCD;
    public float attackCD = -1;
    public float dashDuration;
    public float dashSpeed;
    public float movementSpeed;
    public float jumpHeight;
    public float doubleJumps;
    public InputSystemController ISC;
    public bool currentFlip;
    
    
    
    protected override void Awake()
    {
        base.Awake();
        _stateMachine = GetComponent<PlayerStateMachine>();
        _stateMachine.Initialize(this, _rb);
        ISC = GetComponent<InputSystemController>();
        SetPresets();
    }

    private void SetPresets()
    {
        doubleJumps = playerInfo.doubleJumps + 1;
        dashCD = playerInfo.dashCD;
        movementSpeed = playerInfo.movementSpeed;
        dashSpeed = playerInfo.dashSpeed;
        dashDuration = playerInfo.dashDuration;
        jumpHeight = playerInfo.jumpHeight;
    }

    #region Cooldowns
    protected void FixedUpdate()
    {
        if (!Grounded()) 
            coyoteJump -= Time.deltaTime;
        if(!AttackReady())
            attackCD -= Time.deltaTime;
        if(!DashReady())
            dashCD -= Time.deltaTime;
        if (Grounded())
        {
            coyoteJump = playerInfo.coyoteJump;
            doubleJumps = playerInfo.doubleJumps;
        }
        
        Flip();
    }
    public void StartAttackCD() => attackCD = playerInfo.attackCD;
    

    public bool AttackReady() => attackCD <= 0;
    
    public void StartDashCD() => dashCD = playerInfo.dashCD;
    
    public bool DashReady() => dashCD <= 0;
    
    #endregion
    public override void Flip()
    {
        if (InputSystemController.MovementInput().x > 0) sprite.flipX = false;
        if (InputSystemController.MovementInput().x < 0) sprite.flipX = true;
    }

    public void Move(float x, float y)
    {
        _rb.velocity = new Vector2(x, y);
    }
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
}
