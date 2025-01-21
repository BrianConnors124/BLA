using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.PlayerLoop;
using Debug = System.Diagnostics.Debug;


public class InputSystemController : MonoBehaviour
{

    public static InputSystemController instance;
    private bool queueActive;
    private bool buttonPressed;
    private UniversalTimer _queueTimer;
    [SerializeField] private int currentPause;

    [Header("ActionKeys")] 
    private string jumpKey = "Jump";
    private string attackKey = "Attack";
    private string superAttackKey = "SuperAttack";
    private string dashKey = "Dash";
    
    [SerializeField] private InputActionReference Walk;
    public Action pauseGame;
    
    

    private void Awake()
    {
        instance = this;
        _queueTimer = GetComponent<UniversalTimer>();
        
    }

    public static Vector2 MovementInput() => instance.Walk.action.ReadValue<Vector2>();
    
    public void HandleJump(InputAction.CallbackContext context)
    {
        
        if(context.performed)_queueTimer.SetTimer(jumpKey, 0.2f);
        
    }  
    public bool TryingJump() => _queueTimer.TimerActive(jumpKey);
    
    
    public void HandleDash(InputAction.CallbackContext context)
    {
        
        if(context.performed)_queueTimer.SetTimer(dashKey, 0.2f);
        
    } 
    public bool TryingDash() => _queueTimer.TimerActive(dashKey);
    
    public void HandleAttack(InputAction.CallbackContext context)
    {
        
        if(context.performed)_queueTimer.SetTimer(attackKey, 0.2f);
        
    } 
    
    public bool TryingAttack() => _queueTimer.TimerActive(attackKey);
    
    public void HandleSuperAttack(InputAction.CallbackContext context)
    {
        
        if(context.performed)_queueTimer.SetTimer(superAttackKey, 0.2f);
        
    } 
    
    public bool TryingSuperAttack() => _queueTimer.TimerActive(superAttackKey);
    
    public void PauseGame(InputAction.CallbackContext context)
    {
        
        if (context.performed)
        {
            pauseGame.Invoke();
        }
        
    } 
    
}
