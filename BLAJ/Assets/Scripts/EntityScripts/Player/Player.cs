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
    
    
    
    protected override void Awake()
    {
        base.Awake();
        _stateMachine = GetComponent<PlayerStateMachine>();
        _stateMachine.Initialize(this, _rb);
        SetPresets();
    }

    private void SetPresets()
    {
        dashCD = playerInfo.dashCD;
        movementSpeed = playerInfo.movementSpeed;
        dashSpeed = playerInfo.dashSpeed;
        dashDuration = playerInfo.dashDuration;
        jumpHeight = playerInfo.jumpHeight;
    }

    #region Cooldowns
    private void Update()
    {
        if(!AttackReady())
            attackCD -= Time.deltaTime;
        if(!DashReady())
            dashCD -= Time.deltaTime;
    }
    public void StartAttackCD()
    {
        attackCD = playerInfo.attackCD;
    }

    public bool AttackReady() => attackCD <= 0;
    
    public void StartDashCD()
    {
        dashCD = playerInfo.dashCD;
    }
    public bool DashReady() => dashCD <= 0;
    
    #endregion
    public override bool Flip(Rigidbody2D rb, bool currentFlip)
    {
        if (InputSystemController.MovementInput().x > 0)
            return false;
        if (InputSystemController.MovementInput().x < 0)
            return true;
        return currentFlip;
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
}
