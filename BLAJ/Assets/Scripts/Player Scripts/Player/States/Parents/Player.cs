using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
{
    public PlayerStateMachine _stateMachine;
    
    
    
    protected override void Awake()
    {
        base.Awake();
        _stateMachine = GetComponent<PlayerStateMachine>();
        _stateMachine.Initialize(this, _rb, _anim);
    }
}
[CreateAssetMenu(menuName = "Weapons/NewWeapon", fileName = "NewWeapon")]
public class PlayerInfo : ScriptableObject
{
    public float movementSpeed;
    public UniversalTimer jumpCD;
}
