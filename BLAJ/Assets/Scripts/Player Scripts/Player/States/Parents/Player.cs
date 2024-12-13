using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
{
    public PlayerStateMachine _stateMachine;
    public PlayerInfo playerInfo;
    public UniversalTimer attack;
    public UniversalTimer dash;
    
    private float dashCD;
    private float attackCD;
    public float dashDuration;
    public float dashSpeed;
    public float movementSpeed;
    public float jumpHeight;
    
    
    
    protected override void Awake()
    {
        base.Awake();
        _stateMachine = GetComponent<PlayerStateMachine>();
        _stateMachine.Initialize(this, _rb);
        SetTimers();
        SetPresets();
    }

    private void SetTimers()
    {
        attack = new UniversalTimer();
        dash = new UniversalTimer();
    }
    private void SetPresets()
    {
        attackCD = playerInfo.attackCD;
        dashCD = playerInfo.dashCD;
        movementSpeed = playerInfo.movementSpeed;
        dashSpeed = playerInfo.dashSpeed;
        dashDuration = playerInfo.dashDuration;
        jumpHeight = playerInfo.jumpHeight;
    }
    public void StartDashCD()
    {
        StartCoroutine(dash.Timer(dashCD));
    }
    public void StartAttackCD()
    {
        StartCoroutine(attack.Timer(attackCD));
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
