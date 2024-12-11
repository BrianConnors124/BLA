using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
{
    public PlayerStateMachine _stateMachine;
    public PlayerInfo playerInfo;
    public UniversalTimer jump;
    public UniversalTimer dash;
    
    private float jumpCD;
    private float dashCD;
    public float dashSpeed;
    public float movementSpeed;
    
    
    
    protected override void Awake()
    {
        base.Awake();
        _stateMachine = GetComponent<PlayerStateMachine>();
        _stateMachine.Initialize(this, _rb, _anim);
        SetTimers();
        SetPresets();
    }

    private void SetTimers()
    {
        jump = new UniversalTimer();
        dash = new UniversalTimer();
    }
    private void SetPresets()
    {
        jumpCD = playerInfo.jumpCD;
        dashCD = playerInfo.dashCD;
        movementSpeed = playerInfo.movementSpeed;
        dashSpeed = playerInfo.dashSpeed;
    }

    public void StartJumpCD()
    {
        StartCoroutine(jump.Timer(jumpCD));
    }
    public void StartDashCD()
    {
        StartCoroutine(dash.Timer(dashCD));
    }
}
[CreateAssetMenu(menuName = "Players/NewPlayer", fileName = "NewPlayer")]
public class PlayerInfo : ScriptableObject
{
    public float movementSpeed;
    public float dashSpeed;
    public float jumpCD;
    public float dashCD;
}
