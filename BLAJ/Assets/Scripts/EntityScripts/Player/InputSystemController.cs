using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using Debug = System.Diagnostics.Debug;


public class InputSystemController : MonoBehaviour
{

    public static InputSystemController instance;
    private bool queueActive;
    private bool buttonPressed;


    private UniversalTimer _queueTimer;

    [Header("ActionKeys")] 
    private string jumpKey = "Jump";
    private string attackKey = "Attack";
    private string superAttackKey = "SuperAttack";
    private string dashKey = "Dash";
    
    [SerializeField] private InputActionReference Walk;
    [SerializeField] private InputActionReference Jump;
    [SerializeField] private InputActionReference Dash;
    [SerializeField] private InputActionReference Attack;
    [SerializeField] private InputActionReference SuperAttack;
    
    

    private void Awake()
    {
        instance = this;
        _queueTimer = GetComponent<UniversalTimer>();
    }
    
    public static Vector2 MovementInput() => instance.Walk.action.ReadValue<Vector2>();
    
    public bool HandleJump()
    {
        var a = instance.Jump.action.triggered;
        if (a && !_queueTimer.TimerActive(jumpKey))_queueTimer.SetTimer(jumpKey, 0.2f);
        return a;
    } 
    public bool TryingJump() => HandleJump() || _queueTimer.TimerActive(jumpKey);
    
    
    private bool HandleDash()
    {
        var a = instance.Dash.action.triggered;
        if (a && !_queueTimer.TimerActive(dashKey))_queueTimer.SetTimer(dashKey, 0.2f);
        return a;
    } 
    public bool TryingDash() => HandleDash() || _queueTimer.TimerActive(dashKey);
    
    
    private bool HandleAttack()
    {
        var a = instance.Attack.action.inProgress;
        if (a && !_queueTimer.TimerActive(attackKey))_queueTimer.SetTimer(attackKey, 0.2f);
        return a;
    } 
    public bool TryingAttack() => HandleAttack() || _queueTimer.TimerActive(attackKey);
    
    private bool HandleSuperAttack()
    {
        var a = instance.SuperAttack.action.inProgress;
        if (a && !_queueTimer.TimerActive(superAttackKey))_queueTimer.SetTimer(superAttackKey, 0.2f);
        return a;
    } 
    
    public bool TryingSuperAttack() => HandleSuperAttack() || _queueTimer.TimerActive(superAttackKey);

    public void GetInput()
    {
        HandleJump();
        HandleAttack();
        HandleDash();
    }
    
}
